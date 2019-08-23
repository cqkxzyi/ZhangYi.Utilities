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