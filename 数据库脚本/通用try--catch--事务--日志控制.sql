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

