--****************************************函数********************************************
--金钱类型转换成字符串，小数不精确时：
CONVERT(varchar(20),convert(decimal(18,4),列名))
--字符串截取
SUBSTRING('12345678',1,10);
--替换
REPLACE('http://image.lyzb.cn/Product/123','http://image.lyzb.cn','')

declare @temp nvarchar(100)=',1,2,3,4,5,,';
IF(CHARINDEX(',',@temp)=1)
	SET @temp=SUBSTRING(@temp,2,LEN(@temp)-1);
while CHARINDEX(',',@temp,LEN(@temp))=LEN(@temp)
begin 
SET @temp=SUBSTRING(@temp,1,LEN(@temp)-1);
end
PRINT @temp

--判断是否存在
if exists(SELECT Id_bigint FROM N_SYS_Evaluation)


--**************************************数据库、表、设置********************************************
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
--判断临时表是否存在
if exists (select * from tempdb.dbo.sysobjects where id = object_id(N'tempdb..#tempcitys') and type='U')
   drop table #tempcitys
--判断临时表是否存在
if object_id(N'tempdb..#temp_table',N'U') is not null 
 begin
       drop table [dbo].#temp_table 
 end

--Oracle SQL
create table NewTable as select * from OldTable where 1=2

--关闭数据库
alter database LiSpreadPlatform set offline
--开启数据库
alter database LiSpreadPlatform set online

--让数据库处于单用户模式并且回滚所有事物：
ALTER DATABASE DB_NAME SET SINGLE_USER WITH ROLLBACK IMMEDIATE

--**********断开连接方式2**********
USE master
GO
ALTER DATABASE [GPOSDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
--查看是否还有用户连接
SELECT * FROM sys.[sysprocesses] WHERE DB_NAME([dbid])='gposdb'
GO
ALTER DATABASE [GPOSDB] SET MULTI_USER 
GO
-----End----

--修改数据库不支持中文的问题
alter database LiSpreadPlatform collate Chinese_PRC_CI_AS

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

--插入数据时，关闭主键自增，再打开。
SET IDENTITY_INSERT [Recursive] ON
 INSERT INTO [Recursive](id,text) VALUES(3,'c')
SET IDENTITY_INSERT [Recursive] OFF


--************事务********
@@Error --/错误个数
@@TRANCOUNT --/当前连接的事务个数
SET XACT_ABORT on;--/回滚所有语句
select   *   from fn_dblog(null,null)--/查看系统事务日志

commit transaction;--/提交事务
rollback transaction--/回滚事务语句
save transaction savepoint--/设置保存点
SET IMPLICIT_TRANSACTIONS ON  --/启动隐式事务模式
SET IMPLICIT_TRANSACTIONS OFF --/关闭隐式事务模式
COMMIT TRANSACTION 或者 COMMIT WORK 或者 ROLLBACK TRANSACTION 或 ROLLBACK WORK --/结束或回滚事务

--**********try catch 的用法1**********
BEGIN
 begin try
	begin transaction 
		insert into aaa values (2,2,2)
		commit --提交事务
 end try
 begin catch
	  PRINT('错误代码 = '+STR(ERROR_NUMBER()))
	  PRINT('错误严重级别 = '+STR(ERROR_SEVERITY()))
	  PRINT('错误状态代码 = '+STR(ERROR_STATE()))
	  PRINT('错误信息 = '+ERROR_MESSAGE())
	  PRINT('例程中的行号 = '+str(ERROR_LINE()))
	  PRINT('存储过程或触发器的名称='+ERROR_PROCEDURE())
	  raiserror('不符合数据格式要求',18,2)--用户自定义抛出异常
	  ROLLBACK  -- 回滚事务
  end catch
END

SET XACT_ABORT ON--可以回滚所有事务
begin transaction
	insert into aaa (id,代码) values ('1','1')
	commit transaction
SET XACT_ABORT off

SET IMPLICIT_TRANSACTIONS ON
---------------End 

--**********简单try catch 的用法**********
BEGIN
SET NOCOUNT ON;--不返回受影响的行数

BEGIN TRY

	--sql业务语句
	SELECT TOP 10 * FROM tableName;

	PRINT '执行完毕！';
END TRY
BEGIN CATCH------------有异常被捕获
        PRINT '错误代码： ' + CONVERT(varchar(50), ERROR_NUMBER()) +      --错误代码
        ', 严重级别： ' + CONVERT(varchar(5), ERROR_SEVERITY()) +	--错误严重级别，级别小于10 try catch 捕获不到
        ', 状态码： ' + CONVERT(varchar(5), ERROR_STATE()) +      --错误状态码
        ', 触发器的名称： ' + ISNULL(ERROR_PROCEDURE(), '-') +    --出现错误的存储过程或触发器的名称。
        ', 行号：' + CONVERT(varchar(5), ERROR_LINE());			 --发生错误的行号
		           
        PRINT ERROR_MESSAGE();   --错误的具体信息

END CATCH--------结束异常处理

END
-------------End 



--*******************************************创建约束********************************************
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

--**********************************************游标********************************************
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

--*****************************************序列*************************************************
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

--**********************************************其他********************************************
set nocount on   --是否启用受影响的行数
select @@ROWCOUNT--返回上句影响的行数
SET ANSI_NULLS OFF       --用<>、= 比较null是 始终返回false,所以通常都设置成Off；
SET QUOTED_IDENTIFIER ON --标识符是否可以通过双引号分开，on、off可以随意定义；

--/统计 系统列、表
select * from syscolumns where  id =(select id from sysobjects where xtype='U' and name='表名称')
select * from information_schema.tables  
--/查询当前 server 的实例名称
 select convert(varchar(30),@@SERVICENAME) as shili
 --/查询时 区分大小写
SELECT * FROM T_Cg_Base_Flbm_bm WHERE 规格型号 COLLATE SQL_Latin1_General_CP1_CS_AS LIKE '%A%' 
--/延迟后执行
WAITFOR DELAY '000:00:10'   /*延迟时间10秒*/ 
waitfor time '15:43:50'



--用sql server 连接其他数据源
SELECT * FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.4.0','Provider=Microsoft.Jet.OLEDB.4.0;Data Source=abc.mdb;Persist Security Info=False') table1
SELECT * FROM OPENROWSET('Microsoft.Jet.OLEDB.4.0', 'abc.mdb';'admin';'','SELECT * FROM table1')

--查询和更改当前索引
DBCC checkident ('Buy_Images',noreseed)
DBCC checkident ('Buy_Images',reseed,1)


--查询游标使用情况，是否释放
SELECT  session_id ,
        cursor_id ,
        name ,
        creation_time ,
        is_open
FROM    sys.dm_exec_cursors(0)
WHERE   is_open = 1;

--递归查询
WITH AppMenuList as (
	select *,CAST(SortCode AS NVARCHAR(Max)) as 排序码 from UPM_AppMenu where ParentID=0
	UNION ALL
	select temp.*, AppMenuList.排序码+'.'+CAST(temp.SortCode AS NVARCHAR(Max)) from UPM_AppMenu as temp INNER JOIN AppMenuList on temp.ParentID=AppMenuList.ID
)
select * from AppMenuList order by 排序码 ASC


--父级查所有子级
with CTEGetChild as 
( 
  select 0 AS lvl,* from [Szy_DB].[dbo].[szy_region] where parent_code='50' 
  UNION ALL 
  (SELECT lvl+1,a.* from [Szy_DB].[dbo].[szy_region] as a inner join  CTEGetChild as b on a.parent_code=b.region_code ) 
) 
SELECT * FROM CTEGetChild

--子级查所有父级
DECLARE @temp NVARCHAR(100)
SET @temp='';
with CTEGetParent  as 
( 
	SELECT lvl+1,* from [Szy_DB].[dbo].[szy_region] where region_code='50,01,39' 
	UNION ALL 
	(SELECT lvl+1,a.* from [Szy_DB].[dbo].[szy_region] as a inner join CTEGetParent  as b on a.region_code=b.parent_code ) 
)

--利用FOR XML PATH 合并列用逗号串联
 SELECT [LoanNo],
   (SELECT '['+[DocName]+']'+',' FROM [BPMS_Doc] WHERE LoanNo=A.LoanNo  FOR XML PATH('')) AS StuList
 FROM [BPMS_Doc] as A 

--------------------------------断开连接设置数据库排序规则-----------------------------------
 --设置单用户模式
 USE master
 GO
 ALTER DATABASE [数据库名称] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
 GO
 --查看是否还有用户连接
 SELECT * FROM sys.[sysprocesses] WHERE DB_NAME([dbid])='数据库名称'
 GO
 ALTER DATABASE [GPOSDB] SET MULTI_USER 
 GO 
-------------------------------断开连接设置数据库排序规则------------------------------------End

--------------------------------断开所有数据库链接-----------------------------------
use master
declare @spid int ;
declare @ddlstring nvarchar(max);
declare @dbname varchar(200);
set @dbname='数据库名';
declare tmpcur cursor 
for select distinct spid as spid from sys.sysprocesses
where dbid=db_id(@dbname) ;
OPEN tmpcur;
fetch tmpcur into @spid ;
while (@@FETCH_STATUS=0)
 begin 
   set @ddlstring=N'Kill '+CONVERT( nvarchar,@spid) ;
   execute sp_executesql @ddlstring ;
   fetch tmpcur into @spid ;
 end ;
close tmpcur ;
deallocate tmpcur ;
--------------------------------断开所有数据库链接-----------------------------------End

--------------------------------还原失败，断开所有数据库链接-----------------------------------
--断开
ALTER DATABASE BPMS SET OFFLINE WITH ROLLBACK IMMEDIATE
--恢复
ALTER  database  BPMS  set   online 
--------------------------------还原失败，断开所有数据库链接-----------------------------------End


