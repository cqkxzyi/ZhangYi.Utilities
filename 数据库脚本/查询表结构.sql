--**查询某个表的外键约束列表**
select fk.name,fk.object_id,OBJECT_NAME(fk.parent_object_id) as referenceTableName
	from sys.foreign_keys as fk
	join sys.objects as o on fk.referenced_object_id=o.object_id
where o.name='iFramework_Department'

--**查询数据库字典--表结构**
SELECT   --a.id,
      CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名,
      CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明,
      a.colorder AS 字段序号, a.name AS 字段名, CASE WHEN COLUMNPROPERTY(a.id,
      a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,
      CASE WHEN EXISTS
          (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,
      b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')
      AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,
      CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '')
      AS 默认值, ISNULL(g.[value], '') AS 字段说明, d.crdate AS 创建时间,
      CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间
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

--**数据库字典--索引**
SELECT TOP 100 PERCENT --a.id,
      CASE WHEN b.keyno = 1 THEN c.name ELSE '' END AS 表名,
      CASE WHEN b.keyno = 1 THEN a.name ELSE '' END AS 索引名称, d.name AS 列名,
      b.keyno AS 索引顺序, CASE indexkey_property(c.id, b.indid, b.keyno, 'isdescending')
      WHEN 1 THEN '降序' WHEN 0 THEN '升序' END AS 排序, CASE WHEN p.id IS NULL
      THEN '' ELSE '√' END AS 主键, CASE INDEXPROPERTY(c.id, a.name, 'IsClustered')
      WHEN 1 THEN '√' WHEN 0 THEN '' END AS 聚集, CASE INDEXPROPERTY(c.id,
      a.name, 'IsUnique') WHEN 1 THEN '√' WHEN 0 THEN '' END AS 唯一,
      CASE WHEN e.id IS NULL THEN '' ELSE '√' END AS 唯一约束,
      a.OrigFillFactor AS 填充因子, c.crdate AS 创建时间, c.refdate AS 更改时间
FROM dbo.sysindexes a INNER JOIN
      dbo.sysindexkeys b ON a.id = b.id AND a.indid = b.indid INNER JOIN
      dbo.syscolumns d ON b.id = d.id AND b.colid = d.colid INNER JOIN
      dbo.sysobjects c ON a.id = c.id AND c.xtype = 'U' LEFT OUTER JOIN
      dbo.sysobjects e ON e.name = a.name AND e.xtype = 'UQ' LEFT OUTER JOIN
      dbo.sysobjects p ON p.name = a.name AND p.xtype = 'PK'
WHERE (OBJECTPROPERTY(a.id, N'IsUserTable') = 1) AND (OBJECTPROPERTY(a.id,
      N'IsMSShipped') = 0) AND (INDEXPROPERTY(a.id, a.name, 'IsAutoStatistics') = 0)
ORDER BY c.name, a.name, b.keyno


--**查询--主键.外键.约束.视图.函数.存储过程.触发器**
SELECT DISTINCT
      TOP 100 PERCENT o.xtype,
      CASE o.xtype WHEN 'X' THEN '扩展存储过程' WHEN 'TR' THEN '触发器' WHEN 'PK' THEN
       '主键' WHEN 'F' THEN '外键' WHEN 'C' THEN '约束' WHEN 'V' THEN '视图' WHEN 'FN'
       THEN '函数-标量' WHEN 'IF' THEN '函数-内嵌' WHEN 'TF' THEN '函数-表值' ELSE '存储过程'
       END AS 类型, o.name AS 对象名, o.crdate AS 创建时间, o.refdate AS 更改时间,
      c.text AS 声明语句
FROM dbo.sysobjects o LEFT OUTER JOIN
      dbo.syscomments c ON o.id = c.id
WHERE (o.xtype IN ('X', 'TR', 'C', 'V', 'F', 'IF', 'TF', 'FN', 'P', 'PK')) AND
      (OBJECTPROPERTY(o.id, N'IsMSShipped') = 0)
ORDER BY CASE o.xtype WHEN 'X' THEN '扩展存储过程' WHEN 'TR' THEN '触发器' WHEN
       'PK' THEN '主键' WHEN 'F' THEN '外键' WHEN 'C' THEN '约束' WHEN 'V' THEN '视图'
       WHEN 'FN' THEN '函数-标量' WHEN 'IF' THEN '函数-内嵌' WHEN 'TF' THEN '函数-表值'
       ELSE '存储过程' END DESC