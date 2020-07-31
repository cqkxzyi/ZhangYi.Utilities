--/统计表
select * from information_schema.tables  
--/查询表信息
select * from syscolumns where  id =(select id from sysobjects where xtype='U' and name='表名称')
--获取所有存储过程
select * from sysobjects where type='p' 



--查询表内容 
SELECT 
表名=case when a.colorder=1 then d.name else '' end, 
表说明=case when a.colorder=1 then isnull(f.value,'') else '' end, 
字段序号=a.colorder, 
字段名=a.name, 
标识=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end, 
主键=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in ( 
SELECT name FROM sysindexes WHERE indid in( 
SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid 
))) then '√' else '' end, 
类型=b.name, 
占用字节数=a.length, 
长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), 
小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), 
允许空=case when a.isnullable=1 then '√'else '' end, 
默认值=isnull(e.text,''), 
字段说明=isnull(g.[value],'') 
FROM syscolumns a 
left join systypes b on a.xtype=b.xusertype 
inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
left join syscomments e on a.cdefault=e.id 
left join sysproperties g on a.id=g.id and a.colid=g.smallid 
left join sysproperties f on d.id=f.id and f.smallid=0 
--where d.name='要查询的表' --如果只查询指定表,加上此条件 
order by a.id,a.colorder 


 --================================================================================ 
--SQL交*表实例 
--实现交*表，数据库基于SQL SERVER 2000。 
--建表： 
--在查询分析器里运行： 
CREATE TABLE [Test] ( 
[id] [int] IDENTITY (1, 1) NOT NULL , 
[name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[subject] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[Source] [numeric](18, 0) NULL 
) ON [PRIMARY] 
GO 
INSERT INTO [test] ([name],[subject],[Source]) values (N'张三',N'语文',60) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'李四',N'数学',70) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'王五',N'英语',80) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'王五',N'数学',75) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'王五',N'语文',57) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'李四',N'语文',80) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'张三',N'英语',100) 
Go 
--交*表语句的实现： 
--用于：交*表的列数是确定的 
select name,sum(case subject when '数学' then source else 0 end) as '数学', 
sum(case subject when '英语' then source else 0 end) as '英语', 
sum(case subject when '语文' then source else 0 end) as '语文' 
from test 
group by name 
--用于：交*表的列数是不确定的 
declare @sql varchar(8000) 
set @sql = 'select name,' 
select @sql = @sql + 'sum(case subject when '''+subject+''' 
then source else 0 end) as '''+subject+''',' 
from (select distinct subject from test) as a 
select @sql = left(@sql,len(@sql)-1) + ' from test group by name' 
exec(@sql) 
go 


--******************生成交*表的简单通用存储过程 

/*--生成交*表的简单通用存储过程 
根据指定的表名,纵横字段,统计字段,自动生成交*表 
并可根据需要生成纵横两个方向的合计 
注意,横向字段数目如果大于纵向字段数目,将自动交换纵横字段 
如果不要此功能,则去掉交换处理部分 
--*/ 
create proc p_qry 
@TableName sysname, --表名 
@纵轴 sysname, --交*表最左面的列 
@横轴 sysname, --交*表最上面的列 
@表体内容 sysname, --交*表的数数据字段 
@是否加横向合计 bit,--为1时在交*表横向最右边加横向合计 
@是否家纵向合计 bit --为1时在交*表纵向最下边加纵向合计 
as 
declare @s nvarchar(4000),@sql varchar(8000) 
--判断横向字段是否大于纵向字段数目,如果是,则交换纵横字段 
set @s='declare @a sysname 
if(select case when count(distinct ['+@纵轴+'])<count(distinct ['+@横轴+']) then 1 else 0 end 
from ['+@TableName+'])=1 
select @a=@纵轴,@纵轴=@横轴,@横轴=@a' 
exec sp_executesql @s 
,N'@纵轴 sysname out,@横轴 sysname out' 
,@纵轴 out,@横轴 out 
--生成交*表处理语句 
set @s=' 
set @s='''' 
select @s=@s+'',[''+cast(['+@横轴+'] as varchar)+'']=sum(case ['+@横轴 
+'] when ''''''+cast(['+@横轴+'] as varchar)+'''''' then ['+@表体内容+'] else 0 end)'' 
from ['+@TableName+'] 
group by ['+@横轴+']' 
exec sp_executesql @s 
,N'@s varchar(8000) out' 
,@sql out 
--是否生成合计字段的处理 
declare @sum1 varchar(200),@sum2 varchar(200),@sum3 varchar(200) 
select @sum1=case @是否加横向合计 
when 1 then ',[合计]=sum(['+@表体内容+'])' 
else '' end 
,@sum2=case @是否家纵向合计 
when 1 then '['+@纵轴+']=case grouping([' 
+@纵轴+']) when 1 then ''合计'' else cast([' 
+@纵轴+'] as varchar) end' 
else '['+@纵轴+']' end 
,@sum3=case @是否家纵向合计 
when 1 then ' with rollup' 
else '' end 
--生成交*表 
exec('select 
from ['+@TableName+'] 
group by ['+@纵轴+']'+@sum3) 

--测试用例：
exec p_qry 'syscolumns','id','colid','colid',1,1 
--删除存储过程
--if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[p_qry]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) 
--drop procedure [dbo].[p_qry]  

-----------------End




--**查询某个表的外键约束列表**
select fk.name,fk.object_id,OBJECT_NAME(fk.parent_object_id) as referenceTableName
	from sys.foreign_keys as fk
	join sys.objects as o on fk.referenced_object_id=o.object_id
where o.name='iFramework_Department'

--**查询数据库字典--表结构**
SELECT   --a.id,
      CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名,
      CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明,
      a.colorder AS 字段序号, a.name AS 字段名, CASE WHEN COLUMNPROPERTY(a.id,
      a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,
      CASE WHEN EXISTS
          (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,
      b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')
      AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,
      CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '')
      AS 默认值, ISNULL(g.[value], '') AS 字段说明, d.crdate AS 创建时间,
      CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间
FROM dbo.syscolumns a LEFT OUTER JOIN
      dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
      dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND
      d.status >= 0 LEFT OUTER JOIN
      dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN
      sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND
      g.name = 'MS_Description' LEFT OUTER JOIN
      sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND
      f.name = 'MS_Description'
--where d.name = 'ERP_Supplier'
ORDER BY d.name, a.colorder

--**数据库字典--索引**
SELECT TOP 100 PERCENT --a.id,
      CASE WHEN b.keyno = 1 THEN c.name ELSE '' END AS 表名,
      CASE WHEN b.keyno = 1 THEN a.name ELSE '' END AS 索引名称, d.name AS 列名,
      b.keyno AS 索引顺序, CASE indexkey_property(c.id, b.indid, b.keyno, 'isdescending')
      WHEN 1 THEN '降序' WHEN 0 THEN '升序' END AS 排序, CASE WHEN p.id IS NULL
      THEN '' ELSE '√' END AS 主键, CASE INDEXPROPERTY(c.id, a.name, 'IsClustered')
      WHEN 1 THEN '√' WHEN 0 THEN '' END AS 聚集, CASE INDEXPROPERTY(c.id,
      a.name, 'IsUnique') WHEN 1 THEN '√' WHEN 0 THEN '' END AS 唯一,
      CASE WHEN e.id IS NULL THEN '' ELSE '√' END AS 唯一约束,
      a.OrigFillFactor AS 填充因子, c.crdate AS 创建时间, c.refdate AS 更改时间
FROM dbo.sysindexes a INNER JOIN
      dbo.sysindexkeys b ON a.id = b.id AND a.indid = b.indid INNER JOIN
      dbo.syscolumns d ON b.id = d.id AND b.colid = d.colid INNER JOIN
      dbo.sysobjects c ON a.id = c.id AND c.xtype = 'U' LEFT OUTER JOIN
      dbo.sysobjects e ON e.name = a.name AND e.xtype = 'UQ' LEFT OUTER JOIN
      dbo.sysobjects p ON p.name = a.name AND p.xtype = 'PK'
WHERE (OBJECTPROPERTY(a.id, N'IsUserTable') = 1) AND (OBJECTPROPERTY(a.id,
      N'IsMSShipped') = 0) AND (INDEXPROPERTY(a.id, a.name, 'IsAutoStatistics') = 0)
ORDER BY c.name, a.name, b.keyno


--**查询--主键.外键.约束.视图.函数.存储过程.触发器**
SELECT DISTINCT
      TOP 100 PERCENT o.xtype,
      CASE o.xtype WHEN 'X' THEN '扩展存储过程' WHEN 'TR' THEN '触发器' WHEN 'PK' THEN
       '主键' WHEN 'F' THEN '外键' WHEN 'C' THEN '约束' WHEN 'V' THEN '视图' WHEN 'FN'
       THEN '函数-标量' WHEN 'IF' THEN '函数-内嵌' WHEN 'TF' THEN '函数-表值' ELSE '存储过程'
       END AS 类型, o.name AS 对象名, o.crdate AS 创建时间, o.refdate AS 更改时间,
      c.text AS 声明语句
FROM dbo.sysobjects o LEFT OUTER JOIN
      dbo.syscomments c ON o.id = c.id
WHERE (o.xtype IN ('X', 'TR', 'C', 'V', 'F', 'IF', 'TF', 'FN', 'P', 'PK')) AND
      (OBJECTPROPERTY(o.id, N'IsMSShipped') = 0)
ORDER BY CASE o.xtype WHEN 'X' THEN '扩展存储过程' WHEN 'TR' THEN '触发器' WHEN
       'PK' THEN '主键' WHEN 'F' THEN '外键' WHEN 'C' THEN '约束' WHEN 'V' THEN '视图'
       WHEN 'FN' THEN '函数-标量' WHEN 'IF' THEN '函数-内嵌' WHEN 'TF' THEN '函数-表值'
       ELSE '存储过程' END DESC