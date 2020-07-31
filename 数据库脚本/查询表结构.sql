--/ͳ�Ʊ�
select * from information_schema.tables  
--/��ѯ����Ϣ
select * from syscolumns where  id =(select id from sysobjects where xtype='U' and name='������')
--��ȡ���д洢����
select * from sysobjects where type='p' 



--��ѯ������ 
SELECT 
����=case when a.colorder=1 then d.name else '' end, 
��˵��=case when a.colorder=1 then isnull(f.value,'') else '' end, 
�ֶ����=a.colorder, 
�ֶ���=a.name, 
��ʶ=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '��'else '' end, 
����=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in ( 
SELECT name FROM sysindexes WHERE indid in( 
SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid 
))) then '��' else '' end, 
����=b.name, 
ռ���ֽ���=a.length, 
����=COLUMNPROPERTY(a.id,a.name,'PRECISION'), 
С��λ��=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), 
�����=case when a.isnullable=1 then '��'else '' end, 
Ĭ��ֵ=isnull(e.text,''), 
�ֶ�˵��=isnull(g.[value],'') 
FROM syscolumns a 
left join systypes b on a.xtype=b.xusertype 
inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
left join syscomments e on a.cdefault=e.id 
left join sysproperties g on a.id=g.id and a.colid=g.smallid 
left join sysproperties f on d.id=f.id and f.smallid=0 
--where d.name='Ҫ��ѯ�ı�' --���ֻ��ѯָ����,���ϴ����� 
order by a.id,a.colorder 


 --================================================================================ 
--SQL��*��ʵ�� 
--ʵ�ֽ�*�����ݿ����SQL SERVER 2000�� 
--���� 
--�ڲ�ѯ�����������У� 
CREATE TABLE [Test] ( 
[id] [int] IDENTITY (1, 1) NOT NULL , 
[name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[subject] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , 
[Source] [numeric](18, 0) NULL 
) ON [PRIMARY] 
GO 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'����',60) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'��ѧ',70) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'Ӣ��',80) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'��ѧ',75) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'����',57) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'����',80) 
INSERT INTO [test] ([name],[subject],[Source]) values (N'����',N'Ӣ��',100) 
Go 
--��*������ʵ�֣� 
--���ڣ���*���������ȷ���� 
select name,sum(case subject when '��ѧ' then source else 0 end) as '��ѧ', 
sum(case subject when 'Ӣ��' then source else 0 end) as 'Ӣ��', 
sum(case subject when '����' then source else 0 end) as '����' 
from test 
group by name 
--���ڣ���*��������ǲ�ȷ���� 
declare @sql varchar(8000) 
set @sql = 'select name,' 
select @sql = @sql + 'sum(case subject when '''+subject+''' 
then source else 0 end) as '''+subject+''',' 
from (select distinct subject from test) as a 
select @sql = left(@sql,len(@sql)-1) + ' from test group by name' 
exec(@sql) 
go 


--******************���ɽ�*��ļ�ͨ�ô洢���� 

/*--���ɽ�*��ļ�ͨ�ô洢���� 
����ָ���ı���,�ݺ��ֶ�,ͳ���ֶ�,�Զ����ɽ�*�� 
���ɸ�����Ҫ�����ݺ���������ĺϼ� 
ע��,�����ֶ���Ŀ������������ֶ���Ŀ,���Զ������ݺ��ֶ� 
�����Ҫ�˹���,��ȥ������������ 
--*/ 
create proc p_qry 
@TableName sysname, --���� 
@���� sysname, --��*����������� 
@���� sysname, --��*����������� 
@�������� sysname, --��*����������ֶ� 
@�Ƿ�Ӻ���ϼ� bit,--Ϊ1ʱ�ڽ�*��������ұ߼Ӻ���ϼ� 
@�Ƿ������ϼ� bit --Ϊ1ʱ�ڽ�*���������±߼�����ϼ� 
as 
declare @s nvarchar(4000),@sql varchar(8000) 
--�жϺ����ֶ��Ƿ���������ֶ���Ŀ,�����,�򽻻��ݺ��ֶ� 
set @s='declare @a sysname 
if(select case when count(distinct ['+@����+'])<count(distinct ['+@����+']) then 1 else 0 end 
from ['+@TableName+'])=1 
select @a=@����,@����=@����,@����=@a' 
exec sp_executesql @s 
,N'@���� sysname out,@���� sysname out' 
,@���� out,@���� out 
--���ɽ�*������� 
set @s=' 
set @s='''' 
select @s=@s+'',[''+cast(['+@����+'] as varchar)+'']=sum(case ['+@���� 
+'] when ''''''+cast(['+@����+'] as varchar)+'''''' then ['+@��������+'] else 0 end)'' 
from ['+@TableName+'] 
group by ['+@����+']' 
exec sp_executesql @s 
,N'@s varchar(8000) out' 
,@sql out 
--�Ƿ����ɺϼ��ֶεĴ��� 
declare @sum1 varchar(200),@sum2 varchar(200),@sum3 varchar(200) 
select @sum1=case @�Ƿ�Ӻ���ϼ� 
when 1 then ',[�ϼ�]=sum(['+@��������+'])' 
else '' end 
,@sum2=case @�Ƿ������ϼ� 
when 1 then '['+@����+']=case grouping([' 
+@����+']) when 1 then ''�ϼ�'' else cast([' 
+@����+'] as varchar) end' 
else '['+@����+']' end 
,@sum3=case @�Ƿ������ϼ� 
when 1 then ' with rollup' 
else '' end 
--���ɽ�*�� 
exec('select 
from ['+@TableName+'] 
group by ['+@����+']'+@sum3) 

--����������
exec p_qry 'syscolumns','id','colid','colid',1,1 
--ɾ���洢����
--if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[p_qry]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) 
--drop procedure [dbo].[p_qry]  

-----------------End




--**��ѯĳ��������Լ���б�**
select fk.name,fk.object_id,OBJECT_NAME(fk.parent_object_id) as referenceTableName
	from sys.foreign_keys as fk
	join sys.objects as o on fk.referenced_object_id=o.object_id
where o.name='iFramework_Department'

--**��ѯ���ݿ��ֵ�--��ṹ**
SELECT   --a.id,
      CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS ����,
      CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS ��˵��,
      a.colorder AS �ֶ����, a.name AS �ֶ���, CASE WHEN COLUMNPROPERTY(a.id,
      a.name, 'IsIdentity') = 1 THEN '��' ELSE '' END AS ��ʶ,
      CASE WHEN EXISTS
          (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '��' ELSE '' END AS ����,
      b.name AS ����, a.length AS ����, COLUMNPROPERTY(a.id, a.name, 'PRECISION')
      AS ����, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS С��λ��,
      CASE WHEN a.isnullable = 1 THEN '��' ELSE '' END AS �����, ISNULL(e.text, '')
      AS Ĭ��ֵ, ISNULL(g.[value], '') AS �ֶ�˵��, d.crdate AS ����ʱ��,
      CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS ����ʱ��
FROM dbo.syscolumns a LEFT OUTER JOIN
      dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
      dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND
      d.status >= 0 LEFT OUTER JOIN
      dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN
      sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND
      g.name = 'MS_Description' LEFT OUTER JOIN
      sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND
      f.name = 'MS_Description'
--where d.name = 'ERP_Supplier'
ORDER BY d.name, a.colorder

--**���ݿ��ֵ�--����**
SELECT TOP 100 PERCENT --a.id,
      CASE WHEN b.keyno = 1 THEN c.name ELSE '' END AS ����,
      CASE WHEN b.keyno = 1 THEN a.name ELSE '' END AS ��������, d.name AS ����,
      b.keyno AS ����˳��, CASE indexkey_property(c.id, b.indid, b.keyno, 'isdescending')
      WHEN 1 THEN '����' WHEN 0 THEN '����' END AS ����, CASE WHEN p.id IS NULL
      THEN '' ELSE '��' END AS ����, CASE INDEXPROPERTY(c.id, a.name, 'IsClustered')
      WHEN 1 THEN '��' WHEN 0 THEN '' END AS �ۼ�, CASE INDEXPROPERTY(c.id,
      a.name, 'IsUnique') WHEN 1 THEN '��' WHEN 0 THEN '' END AS Ψһ,
      CASE WHEN e.id IS NULL THEN '' ELSE '��' END AS ΨһԼ��,
      a.OrigFillFactor AS �������, c.crdate AS ����ʱ��, c.refdate AS ����ʱ��
FROM dbo.sysindexes a INNER JOIN
      dbo.sysindexkeys b ON a.id = b.id AND a.indid = b.indid INNER JOIN
      dbo.syscolumns d ON b.id = d.id AND b.colid = d.colid INNER JOIN
      dbo.sysobjects c ON a.id = c.id AND c.xtype = 'U' LEFT OUTER JOIN
      dbo.sysobjects e ON e.name = a.name AND e.xtype = 'UQ' LEFT OUTER JOIN
      dbo.sysobjects p ON p.name = a.name AND p.xtype = 'PK'
WHERE (OBJECTPROPERTY(a.id, N'IsUserTable') = 1) AND (OBJECTPROPERTY(a.id,
      N'IsMSShipped') = 0) AND (INDEXPROPERTY(a.id, a.name, 'IsAutoStatistics') = 0)
ORDER BY c.name, a.name, b.keyno


--**��ѯ--����.���.Լ��.��ͼ.����.�洢����.������**
SELECT DISTINCT
      TOP 100 PERCENT o.xtype,
      CASE o.xtype WHEN 'X' THEN '��չ�洢����' WHEN 'TR' THEN '������' WHEN 'PK' THEN
       '����' WHEN 'F' THEN '���' WHEN 'C' THEN 'Լ��' WHEN 'V' THEN '��ͼ' WHEN 'FN'
       THEN '����-����' WHEN 'IF' THEN '����-��Ƕ' WHEN 'TF' THEN '����-��ֵ' ELSE '�洢����'
       END AS ����, o.name AS ������, o.crdate AS ����ʱ��, o.refdate AS ����ʱ��,
      c.text AS �������
FROM dbo.sysobjects o LEFT OUTER JOIN
      dbo.syscomments c ON o.id = c.id
WHERE (o.xtype IN ('X', 'TR', 'C', 'V', 'F', 'IF', 'TF', 'FN', 'P', 'PK')) AND
      (OBJECTPROPERTY(o.id, N'IsMSShipped') = 0)
ORDER BY CASE o.xtype WHEN 'X' THEN '��չ�洢����' WHEN 'TR' THEN '������' WHEN
       'PK' THEN '����' WHEN 'F' THEN '���' WHEN 'C' THEN 'Լ��' WHEN 'V' THEN '��ͼ'
       WHEN 'FN' THEN '����-����' WHEN 'IF' THEN '����-��Ƕ' WHEN 'TF' THEN '����-��ֵ'
       ELSE '�洢����' END DESC