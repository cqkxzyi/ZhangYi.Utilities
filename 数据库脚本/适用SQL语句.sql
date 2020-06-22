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
--======================================================== 
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
--================================================================================ 
--SQL Server 存储过程的分页方案比拼 
 
--建立表： 
CREATE TABLE [TestTable] ( 
[ID] [int] IDENTITY (1, 1) NOT NULL , 
[FirstName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL , 
[LastName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL , 
[Country] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[Note] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY] 
GO 

--插入数据：(2万条，用更多的数据测试会明显一些) 
SET IDENTITY_INSERT TestTable ON 
declare @i int 
set @i=1 
while @i<=20000 
begin 
insert into TestTable([id], FirstName, LastName, Country,Note) values(@i, 'FirstName_XXX','LastName_XXX','Country_XXX','Note_XXX') 
set @i=@i+1 
end 
SET IDENTITY_INSERT TestTable OFF 

------------------------------------- 
--分页方案一：(利用Not In和SELECT TOP分页) 
--语句形式： 
SELECT TOP 10 * 
FROM TestTable 
WHERE (ID NOT IN 
(SELECT TOP 20 id 
FROM TestTable 
ORDER BY id)) 
ORDER BY ID 
SELECT TOP 页大小 * 
FROM TestTable 
WHERE (ID NOT IN 
(SELECT TOP 页大小*页数 id 
FROM 表 
ORDER BY id)) 
ORDER BY ID 
------------------------------------- 
--分页方案二：(利用ID大于多少和SELECT TOP分页） 
--语句形式： 
SELECT TOP 10 * 
FROM TestTable 
WHERE (ID > 
(SELECT MAX(id) 
FROM (SELECT TOP 20 id 
FROM TestTable 
ORDER BY id) AS T)) 
ORDER BY ID 
SELECT TOP 页大小 * 
FROM TestTable 
WHERE (ID > 
(SELECT MAX(id) 
FROM (SELECT TOP 页大小*页数 id 
FROM 表 
ORDER BY id) AS T)) 
ORDER BY ID 
------------------------------------- 
--分页方案三：(利用SQL的游标存储过程分页) 
create procedure XiaoZhengGe 
@sqlstr nvarchar(4000), --查询字符串 
@currentpage int, --第N页 
@pagesize int --每页行数 
as 
set nocount on 
declare @P1 int, --P1是游标的id 
@rowcount int 
exec sp_cursoropen @P1 output,@sqlstr,@scrollopt=1,@ccopt=1,@rowcount=@rowcount output 
select ceiling(1.0*@rowcount/@pagesize) as 总页数--,@rowcount as 总行数,@currentpage as 当前页 
set @currentpage=(@currentpage-1)*@pagesize+1 
exec sp_cursorfetch @P1,16,@currentpage,@pagesize 
exec sp_cursorclose @P1 
set nocount off 
--其它的方案：如果没有主键，可以用临时表，也可以用方案三做，但是效率会低。 
--建议优化的时候，加上主键和索引，查询效率会提高。 
--通过SQL 查询分析器，显示比较：我的结论是: 
--分页方案二：(利用ID大于多少和SELECT TOP分页）效率最高，需要拼接SQL语句 
--分页方案一：(利用Not In和SELECT TOP分页) 效率次之，需要拼接SQL语句 
--分页方案三：(利用SQL的游标存储过程分页) 效率最差，但是最为通用 
--在实际情况中，要具体分析。 
==================================================================================== 
--得到随机排序结果 

SELECT * 
FROM Northwind..Orders 
ORDER BY NEWID() 
SELECT TOP 10 * 
FROM Northwind..Orders 
ORDER BY NEWID() 
==================================================================================== 
select 
to_char(日期,'yyyymmdd') DATE_ID,to_char(日期,'yyyy')||'年'||to_char(日期,'mm')||'月'||to_char(日期,'dd')||'日' DATE_NAME, 
to_char(日期,'yyyymm') MONTH_ID,to_char(日期,'yyyy')||'年'||to_char(日期,'mm')||'月' MONTH_NAME, 
'Q'||to_char(日期,'q.yyyy') QUARTERID,to_char(日期,'yyyy')||'年第'||to_char(日期,'q')||'季度' QUARTERID_NAME, 
to_char(日期,'yyyy') YEAR_ID,to_char(日期,'yyyy')||'年' YEAR_NAME 
from( 
select to_date('2000-01-01','yyyy-mm-dd')+(rownum-1) 日期 from user_objects where rownum<367 and to_date('2000-01-01','yyyy-mm-dd')+(rownum-1)<to_date('2001-01-01','yyyy-mm-dd') 
); 
--得到季度和月份对应关系 
select distinct to_char(日期,'q') 季度,to_char(to_date('2001-01-01','yyyy-mm-dd')+(rownum-1),'yyyymm') 日期 from( 
select to_date('2001-01','yyyy-mm')+(rownum-1) 日期 from user_objects where rownum<367 and to_date('2001-01-01','yyyy-mm-dd')+(rownum-1)<to_date('2002-01-01','yyyy-mm-dd') 
); 
--得到一年中的天数 
select to_char(to_date('2000-01-01','yyyy-mm-dd')+(rownum-1),'yyyy-mm-dd') 日期 from user_objects where rownum<367 and to_date('2000-01-01','yyyy-mm-dd')+(rownum-1)<to_date('2001-01-01','yyyy-mm-dd'); 
==================================================================================== 
--获取一个数据库的所有存储过程,可以用 
select * from sysobjects where type='p' 
==================================================================================== 
--生成交*表的简单通用存储过程 

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[p_qry]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) 
drop procedure [dbo].[p_qry] 
GO 
/*--生成交*表的简单通用存储过程 

根据指定的表名,纵横字段,统计字段,自动生成交*表 
并可根据需要生成纵横两个方向的合计 
注意,横向字段数目如果大于纵向字段数目,将自动交换纵横字段 
如果不要此功能,则去掉交换处理部分 
--邹建 204.06--*/ 
/*--调用示例 
exec p_qry 'syscolumns','id','colid','colid',1,1 
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
 

--SQL Server 实现Indexof和LastIndexof
dECLARE @Name NVARCHAR (50)
SET @Name = 'ABCDEFG.HI.JKLMNOPQ'
DECLARE @Position INT
--1sql first indexof
SET @Position = CHARINDEX('.', @Name);
select @Position
SELECT SUBSTRING (@Name, @Position+1,LEN(@Name)-@Position)
--2sql last indexof
SET @Position =  LEN(@Name) - CHARINDEX('.', REVERSE(@Name)) + 1
SELECT SUBSTRING (@Name, @Position+1,LEN(@Name)-@Position)

