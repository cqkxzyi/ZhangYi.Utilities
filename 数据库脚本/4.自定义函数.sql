------------------------------ѭ����������-------------------------
DECLARE @img1 VARCHAR(255)='';
DECLARE @img2 VARCHAR(255)='';
DECLARE @ͼƬ���� NVARCHAR(max)='1,2';
DECLARE @next int;
DECLARE @s varchar(100);
set @next=1;
while (@next<=4 and @next<=dbo.Get_StrArrayLength(@ͼƬ����,','))
begin
	DECLARE @ȷ�������� NVARCHAR(50)='';
	DECLARE @ȷ������ֵ NVARCHAR(255)='';
	DECLARE @output NVARCHAR(50)='';
	DECLARE @sql NVARCHAR(500)='';
				   
	SET @ȷ��������='@img'+CAST(@next as CHAR(1));
	SET @ȷ������ֵ= dbo.Get_StrArrayStrOfIndex(@ͼƬ����,',',@next) ----��������е�ֵ

	SET @sql=N'set '+@ȷ��������+' = '''+ @ȷ������ֵ+'''';

	EXEC sp_executesql @sql,N'@img1 NVARCHAR(255) output,@img2 NVARCHAR(255) output',@img1 OUTPUT,@img2 OUTPUT
	SET @next=@next+1
END
PRINT @img1; PRINT @img2
PRINT 'ִ�����'
------------------------------ѭ����������-------------------------End