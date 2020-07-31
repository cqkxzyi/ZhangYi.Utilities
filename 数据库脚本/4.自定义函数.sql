------------------------------循环创建变量-------------------------
DECLARE @img1 VARCHAR(255)='';
DECLARE @img2 VARCHAR(255)='';
DECLARE @图片集合 NVARCHAR(max)='1,2';
DECLARE @next int;
DECLARE @s varchar(100);
set @next=1;
while (@next<=4 and @next<=dbo.Get_StrArrayLength(@图片集合,','))
begin
	DECLARE @确定变量名 NVARCHAR(50)='';
	DECLARE @确定变量值 NVARCHAR(255)='';
	DECLARE @output NVARCHAR(50)='';
	DECLARE @sql NVARCHAR(500)='';
				   
	SET @确定变量名='@img'+CAST(@next as CHAR(1));
	SET @确定变量值= dbo.Get_StrArrayStrOfIndex(@图片集合,',',@next) ----输出数组中的值

	SET @sql=N'set '+@确定变量名+' = '''+ @确定变量值+'''';

	EXEC sp_executesql @sql,N'@img1 NVARCHAR(255) output,@img2 NVARCHAR(255) output',@img1 OUTPUT,@img2 OUTPUT
	SET @next=@next+1
END
PRINT @img1; PRINT @img2
PRINT '执行完毕'
------------------------------循环创建变量-------------------------End