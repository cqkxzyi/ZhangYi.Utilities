--关闭数据库
alter database LiSpreadPlatform set offline
--开启数据库
alter database LiSpreadPlatform set online

--让数据库处于单用户模式并且回滚所有事物：
ALTER DATABASE DB_NAME SET SINGLE_USER WITH ROLLBACK IMMEDIATE


--修改数据库不支持中文的问题
alter database LiSpreadPlatform collate Chinese_PRC_CI_AS

----修改表，区分大小写----
ALTER TABLE [CustomerInfo]   ALTER COLUMN IDCard nvarchar(100) COLLATE Chinese_PRC_CS_AS 
----修改表，不区分大小写----
ALTER TABLE [CustomerInfo]  ALTER COLUMN IDCard nvarchar(100) COLLATE Chinese_PRC_CI_AS 


--用sql server 连接其他数据源
SELECT * FROM OPENDATASOURCE('Microsoft.Jet.OLEDB.4.0','Provider=Microsoft.Jet.OLEDB.4.0;Data Source=abc.mdb;Persist Security Info=False') table1
SELECT * FROM OPENROWSET('Microsoft.Jet.OLEDB.4.0', 'abc.mdb';'admin';'','SELECT * FROM table1')

--查询和更改当前索引
DBCC checkident ('Buy_Images',noreseed)
DBCC checkident ('Buy_Images',reseed,1)


--**********断开连接设置数据库排序规则**********
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
----------End

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

--**********断开所有数据库链接**********
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
--------------End

---**********还原失败，断开所有数据库链接**********
--断开
ALTER DATABASE BPMS SET OFFLINE WITH ROLLBACK IMMEDIATE
--恢复
ALTER  database  BPMS  set   online 
------------End




