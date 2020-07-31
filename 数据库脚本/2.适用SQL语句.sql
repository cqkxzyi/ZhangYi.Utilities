
-----------inner join 去重---------------
select *,
(select top 1 IOUNo from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  IOUNo,
(select top 1 max(LoanDate) from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  LoanDate
from BPMS_FormalLoan as loan  


-----------distinct 去重---------------
--只能单个字段
select distinct name from tableName
--保留其他字段，同时去重
select *, count(distinct name) from 表名 group by name
-------------End



--******************删除重复数据（没有主键ID）******************
--添加唯一序列号
select identity(int,1,1) as tempkey, * into temp1  from Bpms_LoanPawn
--取出LoanNo,PawnId唯一的最大序列号
select  LoanNo,PawnId, max(tempkey) as maxkey into temp2 from temp1 group by  LoanNo,PawnId
--删除临时表多余数据
delete a from temp1 a where not exists (select 1 from temp2 b where a.tempkey=b.maxkey)
--删除临时表序列号
alter table temp1 drop column tempkey
--删除原表数据
truncate table Bpms_LoanPawn
--插回唯一数据
insert Bpms_LoanPawn select * from temp1
--删除临时表
drop table temp1, temp2
--------------End



--SQL Server 实现Indexof和LastIndexof
dECLARE @Name NVARCHAR (50)
SET @Name = 'ABCDEFG.HI.JKLMNOPQ'
DECLARE @Position INT
--1sql first indexof
SET @Position = CHARINDEX('.', @Name);
select @Position
SELECT SUBSTRING (@Name, @Position+1,LEN(@Name)-@Position)
--2sql last indexof
SET @Position =  LEN(@Name) - CHARINDEX('.', REVERSE(@Name)) + 1
select @Position;
SELECT SUBSTRING (@Name, @Position+1,LEN(@Name)-@Position)


--得到随机排序结果 
SELECT TOP 10 *  FROM 表名  ORDER BY NEWID() 




--递归查询
WITH AppMenuList as (
	select *,CAST(SortCode AS NVARCHAR(Max)) as 排序码 from UPM_AppMenu where ParentID=0
	UNION ALL
	select temp.*, AppMenuList.排序码+'.'+CAST(temp.SortCode AS NVARCHAR(Max)) from UPM_AppMenu as temp INNER JOIN AppMenuList on temp.ParentID=AppMenuList.ID
)
select * from AppMenuList order by 排序码 ASC


--父级查所有子级
with CTEGetChild as 
( 
  select 0 AS lvl,* from [Szy_DB].[dbo].[szy_region] where parent_code='50' 
  UNION ALL 
  (SELECT lvl+1,a.* from [Szy_DB].[dbo].[szy_region] as a inner join  CTEGetChild as b on a.parent_code=b.region_code ) 
) 
SELECT * FROM CTEGetChild

--子级查所有父级
DECLARE @temp NVARCHAR(100)
SET @temp='';
with CTEGetParent  as 
( 
	SELECT lvl+1,* from [Szy_DB].[dbo].[szy_region] where region_code='50,01,39' 
	UNION ALL 
	(SELECT lvl+1,a.* from [Szy_DB].[dbo].[szy_region] as a inner join CTEGetParent  as b on a.region_code=b.parent_code ) 
)

--利用FOR XML PATH 合并列用逗号串联
 SELECT [LoanNo],
   (SELECT '['+[DocName]+']'+',' FROM [BPMS_Doc] WHERE LoanNo=A.LoanNo  FOR XML PATH('')) AS StuList
 FROM [BPMS_Doc] as A 










--================================================================================ 
--SQL Server 存储过程的分页方案比拼 
 
--建立表： 
CREATE TABLE [TestTable] ( 
[ID] [int] IDENTITY (1, 1) NOT NULL , 
[FirstName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL , 
[LastName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL , 
[Country] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[Note] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY] 
GO 

--插入数据：(2万条，用更多的数据测试会明显一些) 
SET IDENTITY_INSERT TestTable ON 
declare @i int 
set @i=1 
while @i<=20000 
begin 
insert into TestTable([id], FirstName, LastName, Country,Note) values(@i, 'FirstName_XXX','LastName_XXX','Country_XXX','Note_XXX') 
set @i=@i+1 
end 
SET IDENTITY_INSERT TestTable OFF 

------------------------------------- 
--分页方案一：(利用Not In和SELECT TOP分页) 
--语句形式： 
SELECT TOP 10 * 
FROM TestTable 
WHERE (ID NOT IN 
(SELECT TOP 20 id 
FROM TestTable 
ORDER BY id)) 
ORDER BY ID 
SELECT TOP 页大小 * 
FROM TestTable 
WHERE (ID NOT IN 
(SELECT TOP 页大小*页数 id 
FROM 表 
ORDER BY id)) 
ORDER BY ID 
------------------------------------- 
--分页方案二：(利用ID大于多少和SELECT TOP分页） 
--语句形式： 
SELECT TOP 10 * 
FROM TestTable 
WHERE (ID > 
(SELECT MAX(id) 
FROM (SELECT TOP 20 id 
FROM TestTable 
ORDER BY id) AS T)) 
ORDER BY ID 
SELECT TOP 页大小 * 
FROM TestTable 
WHERE (ID > 
(SELECT MAX(id) 
FROM (SELECT TOP 页大小*页数 id 
FROM 表 
ORDER BY id) AS T)) 
ORDER BY ID 
------------------------------------- 
--分页方案三：(利用SQL的游标存储过程分页) 
create procedure XiaoZhengGe 
@sqlstr nvarchar(4000), --查询字符串 
@currentpage int, --第N页 
@pagesize int --每页行数 
as 
set nocount on 
declare @P1 int, --P1是游标的id 
@rowcount int 
exec sp_cursoropen @P1 output,@sqlstr,@scrollopt=1,@ccopt=1,@rowcount=@rowcount output 
select ceiling(1.0*@rowcount/@pagesize) as 总页数--,@rowcount as 总行数,@currentpage as 当前页 
set @currentpage=(@currentpage-1)*@pagesize+1 
exec sp_cursorfetch @P1,16,@currentpage,@pagesize 
exec sp_cursorclose @P1 
set nocount off 
--其它的方案：如果没有主键，可以用临时表，也可以用方案三做，但是效率会低。 
--建议优化的时候，加上主键和索引，查询效率会提高。 
--通过SQL 查询分析器，显示比较：我的结论是: 
--分页方案二：(利用ID大于多少和SELECT TOP分页）效率最高，需要拼接SQL语句 
--分页方案一：(利用Not In和SELECT TOP分页) 效率次之，需要拼接SQL语句 
--分页方案三：(利用SQL的游标存储过程分页) 效率最差，但是最为通用 
--在实际情况中，要具体分析。 

--==================================================================================== 


 



 

