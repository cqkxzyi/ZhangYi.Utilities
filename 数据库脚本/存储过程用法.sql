
  --执行存储过程  EXEC sp_executesql @SQL
  
  --例子1---
	DECLARE @TableName VARCHAR(50),@sql NVARCHAR(MAX),@OrderID INT;  
	SET @TableName = 'aaa ';   
	SET @OrderID = 1;  
	SET @sql = N'SELECT * FROM '+QUOTENAME(@TableName) + ' WHERE id = @OID ORDER BY id DESC'  
	EXEC sp_executesql  
	@stmt = @sql,  
	@params = N'@OID AS INT', 
	@OID = @OrderID
	
  --例子2--
	DECLARE @sql AS NVARCHAR(12),@i AS INT;
	set @i=10;
	SET @sql = 'SET @p = 12';
	EXEC sp_executesql     
	@stmt = @sql,    
	@params = N'@p AS INT OUTPUT',    
	@p =@i OUTPUT
	SELECT @i
	
  --例子3--
	EXEC sp_executesql @sql,N'@img1 NVARCHAR(255) output,@img2 NVARCHAR(255) output',@img1 OUTPUT,@img2 OUTPUT
	
 --例子4--
	set @sqls=' select @a= COUNT(*) from [HAS].[dbo].[HAS_IWantAValuationApply] 
	exec sp_executesql @sqls,N'@a int output',@num output 	--定义@a为输出变量,@num 获取变量