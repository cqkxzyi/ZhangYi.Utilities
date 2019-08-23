CREATE PROCEDURE YourProcedure    
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY---------------------��ʼ��׽�쳣
       BEGIN TRAN------------------��ʼ����
        
		--�������

       COMMIT TRAN -------�ύ����
    END TRY-----------������׽�쳣
    BEGIN CATCH------------���쳣������
        IF @@TRANCOUNT > 0---------------�ж���û������
        BEGIN
            ROLLBACK TRAN----------�ع�����
        END 
        EXEC YourLogErrorProcedure-----------ִ�д洢���̽�������Ϣ��¼�ڱ���
    END CATCH--------�����쳣����
END

 

---------------------------------------------��¼��������Ϣ�Ĵ洢����--------------------------------------------

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
        PRINT 'ϵͳ�����˴��� ';
        EXECUTE PrintErrorProcedure;-----------------��ӡ������Ϣ�Ĵ洢����
        RETURN -1;
    END CATCH
END;

 

CREATE PROCEDURE PrintErrorProcedure
AS
BEGIN
    SET NOCOUNT ON;

    PRINT '������룺 ' + CONVERT(varchar(50), ERROR_NUMBER()) +     --�������
          ', ���ؼ��� ' + CONVERT(varchar(5), ERROR_SEVERITY()) +  --�������ؼ��𣬼���С��10 try catch ���񲻵�
          ', ״̬�룺 ' + CONVERT(varchar(5), ERROR_STATE()) +      --����״̬��
          ', �����������ƣ� ' + ISNULL(ERROR_PROCEDURE(), '-') +    --���ִ���Ĵ洢���̻򴥷��������ơ�
          ', �кţ�' + CONVERT(varchar(5), ERROR_LINE());		    --����������к�
    PRINT ERROR_MESSAGE();										    --����ľ�����Ϣ
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




--=========================================��ʽ��=======================================
   -----��������ִ�в������
   --SET xact_abort on
   --BEGIN tran
   --BEGIN try  
   -- --�����ȷ
   -- SELECT GETDATE();

   --END try
   --BEGIN catch
   --  SELECT Error_number() as ErrorNumber,  --�������
   --       Error_severity() as ErrorSeverity,  --�������ؼ��𣬼���С��10 try catch ���񲻵�
   --       Error_state() as ErrorState ,  --����״̬��
   --       Error_Procedure() as ErrorProcedure , --���ִ���Ĵ洢���̻򴥷��������ơ�
   --       Error_line() as ErrorLine,  --����������к�
   --       Error_message() as ErrorMessage  --����ľ�����Ϣ
   --   IF(@@trancount>0) --ȫ�ֱ���@@trancount����������ֵ+1���������ж����п�������
   --    ROLLBACK tran  ---���ڳ�������ع�����ʼ����һ�����Ҳû�в���ɹ���
   --END catch
   --IF(@@trancount>0)
   --COMMIT tran  --�ύ����

