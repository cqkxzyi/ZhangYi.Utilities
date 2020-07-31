--****************************************函数********************************************
--金钱类型转换成字符串，小数不精确时：
CONVERT(varchar(20),convert(decimal(18,4),列名))
--字符串截取
SUBSTRING('12345678',1,10);
--替换
REPLACE('http://image.lyzb.cn/Product/123','http://image.lyzb.cn','')
--替换前后字符
declare @temp nvarchar(100)=',1,2,3,4,5,,';
IF(CHARINDEX(',',@temp)=1)
	SET @temp=SUBSTRING(@temp,2,LEN(@temp)-1);
while CHARINDEX(',',@temp,LEN(@temp))=LEN(@temp)
begin 
SET @temp=SUBSTRING(@temp,1,LEN(@temp)-1);
end
PRINT @temp

--判断数据是否存在
if exists(SELECT Id_bigint FROM N_SYS_Evaluation)

--判断临时表是否存在
if object_id(N'tempdb..#temp_table',N'U') is not null 
 begin
    drop table [dbo].#temp_table 
 end
 
--判断临时表是否存在
if exists (select * from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#temp_table') and type='U')
   drop table #temp_table


--**************************************数据库、表、设置********************************************
--插入数据时，关闭主键自增，再打开。
SET IDENTITY_INSERT [表名] ON
 --INSERT语句
SET IDENTITY_INSERT [表名] OFF

--新增字段
ALTER TABLE [szy_order] ADD  [MembershipId_uniqueidentifier] UNIQUEIDENTIFIER
--修改字段
ALTER TABLE [szy_order] ALTER COLUMN  order_sn varchar(123);--新增字段
--删除字段
ALTER TABLE [szy_freight_regions] drop column Destinationy;


--复制表结构 可以选择是否复制数据（缺陷：主键等信息无法复制）
--如果目的表已经存在:
insert into tablename  select * from OldTable where 1=2
--如果目的表不存在:
select * into NewTable from OldTable  ( where 1=2)
--创建一张临时表
SELECT * INTO #tmp FROM OldTable


--Oracle创建表 
create table NewTable as select * from OldTable where 1=2



--**********删除主键再新增主键*************
--删除原主键
Declare @Tb varchar(100);
set @Tb='T_Module';
Declare @Pk varChar(100);
Select @Pk=Name from sysobjects where Parent_Obj=OBJECT_ID(@Tb) and xtype='PK';
if @Pk is not null
begin
     exec('Alter table ' + @Tb+ ' Drop '+ @Pk)  --删除原主键
 end
 
--添加新主键
Declare @Ta varchar(100);
set @Ta='T_Module';
 exec ('alter table ' +@Ta+ ' add PRIMARY KEY nonclustered
(
	 id ASC   
)')  --a为字段名
---------End





--*************************************创建约束***************
主键:
1、constraint 名称 primary key （列名，...）  
2、列名 primary key  同时需要多列主键的时候 不能用该方法
3、add constraint 名称 primary key (列名,...)
外部键：
1、constraint 名称 foreign key (本身列名) references 外部表名 （列名） on update   no action    不级联更新      on delete cascade  级联删除
2、列名 foreign key references 外部表名 (列名)
3、add constraint 名称 foreign key (本身列名) references 外部表名 （列名）
唯一约束：
1、add constraint 名称 unique (列名)
check约束
1、add constraint 名称 check(列名 条件)
默认约束
1、列名 default  '0'
2、add constraint 名称 default '0' for 列名
在创建约束时 忽略已经存在的坏数据
1、alter talbe 表名 with nocheck ADD constraint 名称 check (列名 条件)
临时禁用现有约束
1、alter table 表名 nocheck constraint 名称    
启用现有约束
1、alter table 表名 check constraint 名称
验证约束在当前表中的数据是否完全符合
1、EXEC sp_helpconstraint 表名
-------------End

--***************游标示例*****************
-- 声明游标
DECLARE C_Cursor CURSOR  FOR
    SELECT  Id_bigint,CurrentStateId_int FROM [JSH].[dbo].[N_Shopping] WHERE CreateTime_datetime<=@beginDate ORDER BY CreateTime_datetime desc
OPEN C_Cursor; 
--取一条记录
FETCH NEXT FROM C_Cursor INTO @老订单id,@CurrentStateId_int;
WHILE @@FETCH_STATUS=0
BEGIN
    --填写业务代码

    -- 取下一条记录
    FETCH NEXT FROM C_Cursor INTO @老订单id,@CurrentStateId_int;
END
-- 关闭游标
CLOSE C_Cursor;
-- 释放游标
DEALLOCATE C_Cursor;  PRINT '执行完毕！';
-------------End

--*************序列*****************
CREATE SEQUENCE TestSeq
    AS decimal(8,0)   
    START WITH 1  
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 99999999  
    CYCLE  
    CACHE 3 
;

SELECT * FROM sys.sequences WHERE name = 'TestSeq' ;
SELECT NEXT VALUE FOR TestSeq;  
DROP sequence TestSeq;
-------------End

--*****************************其他*****************************
set nocount on   --是否启用受影响的行数
select @@ROWCOUNT--返回上句影响的行数
SET ANSI_NULLS OFF       --用<>、= 比较null是 始终返回false,所以通常都设置成Off；
SET QUOTED_IDENTIFIER ON --标识符是否可以通过双引号分开，on、off可以随意定义；


--/查询当前 server 的实例名称
 select convert(varchar(30),@@SERVICENAME) as shili
--/查询时 区分大小写
SELECT * FROM T_Cg_Base_Flbm_bm WHERE 规格型号 COLLATE SQL_Latin1_General_CP1_CS_AS LIKE '%A%' 
--/延迟后执行
WAITFOR DELAY '000:00:10'   /*延迟时间10秒*/ 
waitfor time '15:43:50'



--查询游标使用情况，是否释放
SELECT  session_id ,
        cursor_id ,
        name ,
        creation_time ,
        is_open
FROM    sys.dm_exec_cursors(0)
WHERE   is_open = 1;



