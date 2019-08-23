select request_session_id spid, OBJECT_NAME(resource_associated_entity_id) LiOrder    
   from  sys.dm_tran_locks   where    resource_type='OBJECT'  

-- xxx �� spid �滻��ȥ�ͺ�,�Ϳ��Կ�������̨����ɵ�����.
exec sp_who2 'xxx'  
--ɱ����������
kill spid 

--------------------------------------------------
--�������
use master
Select * from sysprocesses where blocked<>0
--�ҵ�SPID  
exec sp_lock
--����SPID�ҵ�OBJID
select object_name(131)
--����OBJID�ҵ�����


---------------------------------------------------------
USE MASTER 
--GO
DECLARE @spid INT 
--��ѯ��������SPID 
SELECT   @spid=blocked 
 FROM (SELECT * FROM sysprocesses WHERE blocked > 0) a   
     WHERE NOT EXISTS(SELECT * FROM (SELECT * FROM sysprocesses WHERE blocked > 0) b   
WHERE a.blocked=@spid) 

print @spid

--������������Ĳ��� 
DBCC INPUTBUFFER   (@spid)
print @spid
--KILL���������Ľ��� 
EXEC ('KILL ' + @spid)

--------------------------������ϸ��ѯ---------------------------------
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
    EXEC sp_who active  --���ĸ������������blk 
INSERT INTO #Lock
    EXEC sp_lock  --����ס���Ǹ���Դid��objid 

DECLARE @DBName nvarchar(20);
SET @DBName='NameOfDataBase'

SELECT #Who.* FROM #Who WHERE dbname=@DBName
SELECT #Lock.* FROM #Lock
    JOIN #Who
        ON #Who.spid=#Lock.spid
            AND dbname=@DBName;

--����͵�SQL Server�����
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

--��������Դ
SELECT #Who.spid,hostname,objid,[type],mode,object_name(objid) as objName FROM #Lock
    JOIN #Who
        ON #Who.spid=#Lock.spid
            AND dbname=@DBName
    WHERE objid<>0;

DROP Table #Who;
DROP Table #Lock;