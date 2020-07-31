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
-------------



--**********try catch的用法**********
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
	PRINT '执行完毕！';
	
END TRY
BEGIN CATCH------------有异常被捕获
        PRINT '错误代码： ' + CONVERT(varchar(50), ERROR_NUMBER()) +      --错误代码
        ', 严重级别： ' + CONVERT(varchar(5), ERROR_SEVERITY()) +	--错误严重级别，级别小于10 try catch 捕获不到
        ', 状态码： ' + CONVERT(varchar(5), ERROR_STATE()) +      --错误状态码
        ', 触发器的名称： ' + ISNULL(ERROR_PROCEDURE(), '-') +    --出现错误的存储过程或触发器的名称。
        ', 行号：' + CONVERT(varchar(5), ERROR_LINE());			 --发生错误的行号
		           
        PRINT ERROR_MESSAGE();   --错误的具体信息

END CATCH--结束异常处理

END
-------------End 






CREATE PROCEDURE YourProcedure    
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY---------------------开始捕捉异常
       BEGIN TRAN------------------开始事务
        
		--您的语句

       COMMIT TRAN -------提交事务
    END TRY-----------结束捕捉异常
    BEGIN CATCH------------有异常被捕获
        IF @@TRANCOUNT > 0---------------判断有没有事务
        BEGIN
            ROLLBACK TRAN----------回滚事务
        END 
        EXEC YourLogErrorProcedure-----------执行存储过程将错误信息记录在表当中
    END CATCH--------结束异常处理
END

 

---------------------------------------------记录操作错信息的存储过程--------------------------------------------

CREATE PROCEDURE YourLogErrorProcedure
    @ErrorLogID [int] = 0 OUTPUT -- contains the ErrorLogID of the row inserted
AS                               -- by uspLogError in the ErrorLog table
BEGIN
    SET NOCOUNT ON;

    -- Output parameter value of 0 indicates that error 
    -- information was not logged
    SET @ErrorLogID = 0;

    BEGIN TRY
        -- Return if there is no error information to log
        IF ERROR_NUMBER() IS NULL
            RETURN;

        -- Return if inside an uncommittable transaction.
        -- Data insertion/modification is not allowed when 
        -- a transaction is in an uncommittable state.
        IF XACT_STATE() = -1
        BEGIN
            PRINT 'Cannot log error since the current transaction is in an uncommittable state. ' 
                + 'Rollback the transaction before executing uspLogError in order to successfully log error information.';
            RETURN;
        END

        INSERT [dbo].[OperateErrorLog] 
            (
            [OperateName], 
            [ErrorNumber], 
            [ErrorSeverity], 
            [ErrorState], 
            [ErrorProcedure], 
            [ErrorLine], 
            [ErrorMessage]
            ) 
        VALUES 
            (
            CONVERT(sysname, CURRENT_USER), 
            ERROR_NUMBER(),
            ERROR_SEVERITY(),
            ERROR_STATE(),
            ERROR_PROCEDURE(),
            ERROR_LINE(),
            ERROR_MESSAGE()
            );
        SET @ErrorLogID = @@IDENTITY;
    END TRY
    BEGIN CATCH
        PRINT '系统发生了错误： ';
        EXECUTE PrintErrorProcedure;-----------------打印错误信息的存储过程
        RETURN -1;
    END CATCH
END;

 

CREATE PROCEDURE PrintErrorProcedure
AS
BEGIN
    SET NOCOUNT ON;

    PRINT '错误代码： ' + CONVERT(varchar(50), ERROR_NUMBER()) +     --错误代码
          ', 严重级别： ' + CONVERT(varchar(5), ERROR_SEVERITY()) +  --错误严重级别，级别小于10 try catch 捕获不到
          ', 状态码： ' + CONVERT(varchar(5), ERROR_STATE()) +      --错误状态码
          ', 触发器的名称： ' + ISNULL(ERROR_PROCEDURE(), '-') +    --出现错误的存储过程或触发器的名称。
          ', 行号：' + CONVERT(varchar(5), ERROR_LINE());		    --发生错误的行号
    PRINT ERROR_MESSAGE();										    --错误的具体信息
END;

CREATE TABLE [dbo].[ErrorLog](
    [ErrorLogID] [int] IDENTITY(1,1) NOT NULL,
    [ErrorTime] [datetime] NOT NULL CONSTRAINT [DF_ErrorLog_ErrorTime]  DEFAULT (getdate()),
    [UserName] [sysname] COLLATE Chinese_PRC_CI_AS NOT NULL,
    [ErrorNumber] [int] NOT NULL,
    [ErrorSeverity] [int] NULL,
    [ErrorState] [int] NULL,
    [ErrorProcedure] [nvarchar](126) COLLATE Chinese_PRC_CI_AS NULL,
    [ErrorLine] [int] NULL,
    [ErrorMessage] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_ErrorLog_ErrorLogID] PRIMARY KEY CLUSTERED 
(
    [ErrorLogID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]




--=========================================方式二=======================================
   -----开启事务，执行插入语句
   --SET xact_abort on
   --BEGIN tran
   --BEGIN try  
   -- --语句正确
   -- SELECT GETDATE();

   --END try
   --BEGIN catch
   --  SELECT Error_number() as ErrorNumber,  --错误代码
   --       Error_severity() as ErrorSeverity,  --错误严重级别，级别小于10 try catch 捕获不到
   --       Error_state() as ErrorState ,  --错误状态码
   --       Error_Procedure() as ErrorProcedure , --出现错误的存储过程或触发器的名称。
   --       Error_line() as ErrorLine,  --发生错误的行号
   --       Error_message() as ErrorMessage  --错误的具体信息
   --   IF(@@trancount>0) --全局变量@@trancount，事务开启此值+1，他用来判断是有开启事务
   --    ROLLBACK tran  ---由于出错，这里回滚到开始，第一条语句也没有插入成功。
   --END catch
   --IF(@@trancount>0)
   --COMMIT tran  --提交事务

