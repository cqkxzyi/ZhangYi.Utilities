select request_session_id spid, OBJECT_NAME(resource_associated_entity_id) LiOrder    
   from  sys.dm_tran_locks   where    resource_type='OBJECT'  

-- xxx 是 spid 替换进去就好,就可以看到是哪台机造成的死锁.
exec sp_who2 'xxx'  
--杀死死锁进程
kill spid 

--------------------------------------------------
--死锁检测
use master
Select * from sysprocesses where blocked<>0
--找到SPID  
exec sp_lock
--根据SPID找到OBJID
select object_name(131)
--根据OBJID找到表名


---------------------------------------------------------
USE MASTER 
--GO
DECLARE @spid INT 
--查询出死锁的SPID 
SELECT   @spid=blocked 
 FROM (SELECT * FROM sysprocesses WHERE blocked > 0) a   
     WHERE NOT EXISTS(SELECT * FROM (SELECT * FROM sysprocesses WHERE blocked > 0) b   
WHERE a.blocked=@spid) 

print @spid

--输出引起死锁的操作 
DBCC INPUTBUFFER   (@spid)
print @spid
--KILL引起死锁的进程 
EXEC ('KILL ' + @spid)

--------------------------死锁详细查询---------------------------------
CREATE Table #Who(spid int,
    ecid int,
    status nvarchar(50),
    loginname nvarchar(50),
    hostname nvarchar(50),
    blk int,
    dbname nvarchar(50),
    cmd nvarchar(50),
    request_ID int);

CREATE Table #Lock(spid int,
    dpid int,
    objid int,
    indld int,
    [Type] nvarchar(20),
    Resource nvarchar(50),
    Mode nvarchar(10),
    Status nvarchar(10)
);

INSERT INTO #Who
    EXEC sp_who active  --看哪个引起的阻塞，blk 
INSERT INTO #Lock
    EXEC sp_lock  --看锁住了那个资源id，objid 

DECLARE @DBName nvarchar(20);
SET @DBName='NameOfDataBase'

SELECT #Who.* FROM #Who WHERE dbname=@DBName
SELECT #Lock.* FROM #Lock
    JOIN #Who
        ON #Who.spid=#Lock.spid
            AND dbname=@DBName;

--最后发送到SQL Server的语句
DECLARE crsr Cursor FOR
    SELECT blk FROM #Who WHERE dbname=@DBName AND blk<>0;
DECLARE @blk int;
open crsr;
FETCH NEXT FROM crsr INTO @blk;
WHILE (@@FETCH_STATUS = 0)
BEGIN;
    dbcc inputbuffer(@blk);
    FETCH NEXT FROM crsr INTO @blk;
END;
close crsr;
DEALLOCATE crsr;

--锁定的资源
SELECT #Who.spid,hostname,objid,[type],mode,object_name(objid) as objName FROM #Lock
    JOIN #Who
        ON #Who.spid=#Lock.spid
            AND dbname=@DBName
    WHERE objid<>0;

DROP Table #Who;
DROP Table #Lock;