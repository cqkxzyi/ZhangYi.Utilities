 
--插入
CREATE PROCEDURE [dbo].[InsertMessage]
@strTable varchar(50),--表名
@strValues varchar(5000)--要插入的数据（用英文逗号分隔）,如果是字符串类型，需加单引号
as
declare @sqlString varchar(5000);
set @sqlString = 'insert into '+@strTable+' values ('+@strValues+')';
exec(@sqlString);

--删除(delete from)
CREATE PROCEDURE [dbo].[DeleteMessage]
@strtable varchar(50),--要删除信息的表名
@strwhere varchar(300)--要删除信息的条件，不用加where，直接：列名=值；如果值是字符串，需加单引号
as
declare @sqlString varchar(500);
declare @whereString varchar(300);
if @strwhere is null or rtrim(@strwhere)=''
    set @whereString='';
else
    set @whereString=' where '+@strwhere;
set @sql= 'delete from '+@strtable+@whereString;
exec(@sql);

--查询
CREATE PROCEDURE [dbo].[SelelctMessage]
@strTable varchar(50),--要查询的表
@strColum varchar(500),--要查询的字段（*表示全部字段）
@strWhere varchar(500)--查询的条件,不用加where，直接 列名=值。如果值是字符串，需加单引号
as
declare @sqlString varchar(400);
declare @whereString varchar(500);
if @strwhere is null or rtrim(@strwhere)=''
    set @whereString='';
else
    set @whereString=' where '+@strwhere;
set @sqlString='select '+@strcolum+' from '+@strtable+@whereString;
exec(@sqlString);

--修改
CREATE PROCEDURE [dbo].[UpdateMessage]
@strTable varchar(50),--要修改的表
@strColumn varchar(500),--要修改的列名（如果有多个，用英文逗号分隔）
@strValue varchar(500),--新的值（用英文逗号分隔，如果是字符串，需加单引号）
@strWhere varchar(500)--where条件，不加wehere，直接 列名=值，如果值是字符串，需加单引号
as
--变量
declare @sqlString nvarchar(4000);--完整的update语句
declare @whereString varchar(500);--where条件
declare @tempString varchar(50);--update语句中间的赋值语句
declare @curr_Column int;--列名字符串的当前位置
declare @curr_Value int;--值字符串的当前位置
declare @prev int;--光标位置
--变量赋初值
set @sqlString = 'update '+@strTable+' set ';
set @whereString = ' where '+@strWhere;
set @tempString='';
set @curr_Column=1;
set @prev=1;
--开始循环处理
while  @prev < len(@strColumn)
begin
    set @curr_Column=charindex(',',@strColumn,@prev);
    set @curr_Value= charindex(',',@strValue,@prev);
    if @curr_Column>@prev
        set @tempString = substring(@strColumn,@prev,@curr_Column-@prev)+'='+substring(@strValue,@prev,@curr_Value-@prev)+',' +@tempString;
    else--最后一个
        begin
            set @tempString =@tempString + substring(@strColumn,@prev,len(@strColumn)-@prev+1)+'='+substring(@strValue,@prev,len(@strValue)-@prev+1);
            break;
        end
    set @prev=@curr_Column+1;
end
set @sqlString = @sqlString+@tempString+@whereString;
exec(@sqlString);