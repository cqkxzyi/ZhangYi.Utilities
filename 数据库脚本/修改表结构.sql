
--建立人：  高升
--建立日期：2007/06/16
--修改日期：
--功能目的：主要就是修改列的属性，不过是可以执行以后修改N多DB的，算不算远程修改列？
--注意：    就我可以用，因为用的临时表太多了，汗！
CREATE PROCEDURE dbo.ModifyColumn
  @dbName sysname = '',        --要修改的数据库名，不写就是所以的数据库
  @tableName sysname = '',    --要修改的表
  @columnName sysname = '',    --要修改的列
  @type varchar(2),            --M修改A新增
  @dataType nvarchar(128) = '',    --数据类型
  @constraint varchar(100) = '',    --约束名，新增的时候写
  @is_exec bit = 0,            --是打印是生成的代码还是直接执行
  @custom varchar(max) = ''
WITH ENCRYPTION
AS

IF (SUSER_SNAME() != 'GaoS')    --我的登陆名，sa都不能执行我的东西
  RAISERROR('The user does not have permission to perform this action.',16,1)

DECLARE @exec varchar(max)    --存放执行的语句
DECLARE @name sysname        --当前要执行的数据库名
DECLARE @server_id varchar(4)    --那个Server
DECLARE @exec1 varchar(100)    --临时执行的语句
DECLARE @exec2 varchar(100)    --
DECLARE @exec3 varchar(300)    --
DECLARE @constraintName varchar(100)    --默认约束的约束名
DECLARE @definition nvarchar(50)        --默认值
DECLARE @sno smallInt        --用于循环

IF (@is_exec <> 1) SET @is_exec = 0    --该值只能是0或1

IF exists (SELECT * FROM tempdb.sys.objects WHERE name = '##myDB')
BEGIN
  PRINT ('There is already an object named ''ModifyColumn'' in the database.')
  RETURN;
END
--这个表放DB的名字，以及DB所在的SERVER
CREATE TABLE ##myDB(sno smallInt identity(1,1),name sysname,server_id tinyInt)

IF (@dbName = '')    --查询所有要执行的DB,除去系统和临时DB
BEGIN
  INSERT ##myDB select name,1 FROM master.sys.databases
   WHERE database_id > 4 and name not like '%temp%' ORDER BY name

  INSERT ##myDB select name,2 FROM SHDB2.master.sys.databases
   WHERE database_id > 4 and name not like '%temp%' ORDER BY name

  INSERT ##myDB select name,3 FROM TWDB.master.sys.databases
   WHERE database_id > 4 and name not like '%temp%' ORDER BY name

  INSERT ##myDB select name,4 FROM GZDB.master.sys.databases
   WHERE database_id > 4 and name not like '%temp%' ORDER BY name
END
ELSE    --如果指定了要修改的数据库名
BEGIN
  DECLARE @getDBName varchar(500)
  SET @getDBName = 'INSERT ##myDB select name,1 FROM master.sys.databases
        WHERE name in ('''+@dbName+''') ORDER BY name'
  EXEC (@getDBName)
  SET @getDBName = 'INSERT ##myDB select name,2 FROM SHDB2.master.sys.databases
        WHERE name in ('''+@dbName+''') ORDER BY name'
  EXEC (@getDBName)
  SET @getDBName = 'INSERT ##myDB select name,3 FROM TWDB.master.sys.databases
        WHERE name in ('''+@dbName+''') ORDER BY name'
  EXEC (@getDBName)
  SET @getDBName = 'INSERT ##myDB select name,4 FROM GZDB.master.sys.databases
        WHERE name in ('''+@dbName+''') ORDER BY name'
  EXEC (@getDBName)
END

SET @sno = 1
WHILE (@sno <= (SELECT COUNT(sno) FROM ##MyDB))
  BEGIN        --终于可以开始了，一个DB一个DB的循环吧，不喜欢用游标

  SELECT @name = name,@server_id = server_id FROM ##MyDB WHERE sno = @sno

IF (@server_id = 2)    --如果是本地的服务器2
BEGIN
  EXEC SHDB2.msdb.dbo.ModifyColumn --其他Server上的这个存储过程比这个简单多了
                @dbName = @name,
                @type = @type,
                @tableName = @tableName,
                @columnName = @columnName,
                @dataType = @dataType,
                @constraint = @constraint,
                @is_exec = @is_exec,
                @custom = @custom
  SET @exec = ''
END

ELSE IF (@server_id = 3)    --如果是台湾的服务器
BEGIN
  EXEC TWDB.msdb.dbo.ModifyColumn
                @dbName = @name,
                @type = @type,
                @tableName = @tableName,
                @columnName = @columnName,
                @dataType = @dataType,
                @constraint = @constraint,
                @is_exec = @is_exec,
                @custom = @custom
  SET @exec = ''
END

ELSE IF (@server_id = 4)    --如果是广州的服务器
BEGIN
  EXEC GZDB.msdb.dbo.ModifyColumn
                @dbName = @name,
                @type = @type,
                @tableName = @tableName,
                @columnName = @columnName,
                @dataType = @dataType,
                @constraint = @constraint,
                @is_exec = @is_exec,
                @custom = @custom
  SET @exec = ''
END

ELSE IF (@type = 'M')    --修改某个字段，这里才是重点
BEGIN
  DECLARE @exec4 varchar(500)    --删除索引或键
  DECLARE @exec5 varchar(500)    --添加索引或键
  DECLARE @exec6 varchar(500)    --临时用用为了得到几个参数
  DECLARE @i_name sysname        --索引名
  DECLARE @is_key bit            --是否主键
  DECLARE @i_no tinyInt             --索引的序号
  DECLARE @c_name varchar(100)    --索引所在的列名
  DECLARE @i_type varchar(60)    --是否聚集
  DECLARE @is_unique varchar(6)    --是否唯一

  SET @exec4 = ''
  SET @exec5 = ''
  SET @i_name = ''

  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##Temp_dafalit')
    DROP TABLE ##Temp_dafalit    --放默认值的
  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##temp_indexName')
    DROP TABLE ##temp_indexName    --放索引名的

  SET @constraintName = ''    --约束名
  SET @definition = ''        --默认值
  SET @exec3 = 'use ' + @name + ';' + char(13) +
        'SELECT name,definition INTO ##Temp_dafalit FROM sys.default_constraints
          WHERE object_id = (SELECT default_object_id FROM sys.columns WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND name = ''' + @columnName + ''')'
  EXEC (@exec3)
  SELECT @constraintName = name,@definition = definition FROM ##Temp_dafalit

  IF (@constraintName != '')    --如果该列有默认约束。修改前先删除，改好后还原。
  BEGIN
    SET @exec1 = 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + @constraintName + char(13)
    SET @exec2 = 'ALTER TABLE ' + @tableName + ' ADD  CONSTRAINT ' + @constraintName + ' DEFAULT ' + @definition + ' FOR ' + @columnName + char(13)
  END
  ELSE
  BEGIN
    SET @exec1 = ''
    SET @exec2 = ''
  END

  SET @exec6 =  'use ' + @name + ';' + char(13) +
    'SELECT IDENTITY(tinyInt,1,1)i_no,c.name,c.is_primary_key INTO ##temp_indexName
       FROM sys.columns a inner join sys.index_columns b 
    ON a.object_id = b.object_id AND a.column_id = b.column_id inner join sys.indexes c
    ON b.object_id = c.object_id AND b.index_id = c.index_id
    WHERE a.object_id = OBJECT_ID(''' + @tableName + ''') AND a.name = ''' + @columnName + ''''
  EXEC (@exec6)

  SET @i_no = 1 --此处的循环很无奈，因为有的列上可能有两个索引，也不知道设计有没问题
  WHILE (@i_no <= (SELECT COUNT(i_no) FROM ##temp_indexName))
  BEGIN
    SELECT @i_name = name ,@is_key = is_primary_key FROM ##temp_indexName WHERE i_no = @i_no

    IF (@i_name != '')    --如果该列有索引。修改前先删除，改好后还原。
    BEGIN
      IF (@is_key = 0)    --判断是否主键，主键的索引和非主键索引删除方法不一样
        SET @exec4 = @exec4 + 'DROP INDEX [' + @i_name + '] ON [' + @tableName + '] WITH ( ONLINE = OFF )' + char(13)
      ELSE
        SET @exec4 = @exec4 + 'ALTER TABLE [' + @tableName + '] DROP CONSTRAINT [' + @i_name + ']' + char(13)

      IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##temp_indexinfo')
        DROP TABLE ##temp_indexinfo    --存放临时的索引信息

      SET @exec6 =  'use ' + @name + ';' + char(13) +
        'SELECT * INTO ##temp_indexinfo FROM
        (SELECT a.name,b.is_descending_key,c.type_desc, c.is_unique,is_primary_key
        FROM sys.columns a inner join sys.index_columns b 
        ON a.object_id = b.object_id AND a.column_id = b.column_id inner join sys.indexes c
        ON b.object_id = c.object_id AND b.index_id = c.index_id
        WHERE a.object_id = OBJECT_ID(''' + @tableName + ''') AND c.name = ''' + @i_name + ''')a'
      EXEC (@exec6)
      SET @c_name = ''
      SELECT @i_type = type_desc, @is_unique = (CASE is_unique WHEN 1 THEN 'UNIQUE' ELSE '' END),@is_key = is_primary_key FROM ##temp_indexinfo
      SELECT @c_name = @c_name + ',[' + name + (CASE is_descending_key WHEN 1 THEN '] DESC' ELSE '] ASC' END) FROM ##temp_indexinfo

      IF (@is_key = 0)    --主键的索引和非主键索引创建方法不一样
        SET @exec5 = @exec5 + 'CREATE ' + @is_unique + ' ' + @i_type + ' INDEX [' + @i_name + '] ON [' + @tableName + '] (' + SUBSTRING(@c_name,2,len(@c_name)) + ')' + char(13)
      ELSE
        SET @exec5 = @exec5 + 'ALTER TABLE [' + @tableName + '] ADD CONSTRAINT [' + @i_name + '] PRIMARY KEY ' + @i_type + ' (' + SUBSTRING(@c_name,2,len(@c_name)) + ')' + + char(13)
    END
    ELSE
    BEGIN
      SET @exec4 = ''
      SET @exec5 = ''
    END
    SET @i_no = @i_no + 1    --下一个索引
  END    --while

  SET @exec = 
    'IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND type = ''U'')BEGIN' + char(13) +
        'IF ((SELECT is_nullable FROM sys.columns WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND name = ''' + @columnName + ''') = 0)' + char(13) +
        'ALTER TABLE ' + @tableName + ' ALTER COLUMN ' + @columnName + ' ' +@dataType + ' not null ' + @constraint + char(13) +    
        'ELSE ALTER TABLE ' + @tableName + ' ALTER COLUMN ' + @columnName + ' ' +@dataType + ' ' + @constraint + char(13) +
    'PRINT ''' + @name + ' 的 ' + @tableName + ' 表修改 ' + @columnName + ' 列成功'' END' + char(13) +
    'ELSE PRINT ''' + @name + ' 中没有 ' + @tableName + ' 表''' + char(13)

  SET @exec = 'use ' + @name + ';' + char(13) + @exec1 + @exec4 + @exec + @exec2 + @exec5

  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##Temp_dafalit')
    DROP TABLE ##Temp_dafalit
  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##temp_indexName')
    DROP TABLE ##temp_indexName
  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##temp_indexinfo')
    DROP TABLE ##temp_indexinfo
  --居然用了3个临时表，还好这个不需要考虑效率问题
END

ELSE IF (@type = 'A')    --增加某个字段
BEGIN
  SET @exec = 'use ' + @name + ';' + char(13) + 
    'IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND type = ''U'')' + char(13) +
        'BEGIN ALTER TABLE ' + @tableName + ' ADD ' + @columnName + ' ' +@dataType + ' ' + @constraint + char(13) +
        'PRINT ''' + @name + ' 的 ' + @tableName + ' 表增加 ' + @columnName + ' 列成功'' END' + char(13) +
    'ELSE PRINT ''' + @name + ' 中没有 ' + @tableName + ' 表''' + char(13)
END

ELSE IF (@type = 'D')    --为列添加默认值
BEGIN
  SET @exec = 
    'IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND type = ''U'')BEGIN' + char(13) +
        'ALTER TABLE ' + @tableName + ' ADD CONSTRAINT DF_' + @tableName + '_' + @columnName +' DEFAULT (' + @constraint + ') FOR ' + @columnName + char(13) + 
        'PRINT ''' + @name + ' 的 ' + @tableName + ' 表为 ' + @columnName + ' 列添加默认值成功'' END' + char(13) +
    'ELSE PRINT ''' + @name + ' 中没有 ' + @tableName + ' 表''' + char(13)

  IF exists (SELECT * FROM tempdb.sys.objects WHERE name = '##Temp_dafalit')
    DROP TABLE ##Temp_dafalit
  SET @constraintName = '' 
  SET @definition = ''
  SET @exec3 = 'use ' + @name + ';' + char(13) +
        'SELECT name,definition INTO ##Temp_dafalit FROM sys.default_constraints
          WHERE object_id = (SELECT default_object_id FROM sys.columns WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND name = ''' + @columnName + ''')'
  EXEC (@exec3)
  SELECT @constraintName = name,@definition = definition FROM ##Temp_dafalit

  IF (@constraintName != '')
  BEGIN
    SET @exec1 =  'use ' + @name + ';' + char(13) + 'alter table ' + @tableName + ' drop constraint ' + @constraintName
    SET @exec = @exec1 + char(13) + @exec + char(13)
  END
  ELSE
    SET @exec = 'use ' + @name + ';' + char(13) + @exec

  IF EXISTS (SELECT * FROM tempdb.sys.objects WHERE name = '##Temp_dafalit')
    DROP TABLE ##Temp_dafalit
END

ELSE IF (@type = 'Z')    --执行自定义动作
BEGIN
set @exec = 'use ' + @name + ';' + char(13) +
    'IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(''' + @tableName + ''') AND type = ''U'')BEGIN' + char(13) +
        @custom + char(13) + 'print ''在 ' + @name + ' 中执行成功'' END' + char(13) +
    'ELSE  print ''在 ' + @name + ' 中没有这个表''' + char(13)
END

BEGIN try    --开始执行
  IF (@is_exec = 1) EXEC (@exec)
  ELSE PRINT (@exec)
END try
BEGIN catch
  DECLARE @error nvarchar(500)
  SET @error = @name + ' 的 ' + @tableName + ' 中失败，原因：' + char(13) + ERROR_MESSAGE()
  RAISERROR(@error,16,1)    --抛出错误信息    
END catch

SET @sno = @sno + 1
END;    --循环结束

IF exists (SELECT * FROM tempdb.sys.objects WHERE name = '##myDB')
  DROP TABLE ##myDB
GO
