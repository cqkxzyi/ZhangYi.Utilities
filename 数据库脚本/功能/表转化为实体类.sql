DECLARE @TableName sysname = 'Order';
 DECLARE @Result VARCHAR(MAX) = 'public class ' + @TableName + '
 {';

 SELECT  @Result = @Result  + CHAR(13) + '    ///<summary>' + CHAR(13)
         + '    ///' + CAST(desr AS NVARCHAR) + CHAR(13)
         + '    ///</summary>' + '
     public ' + ColumnType + NullableSign + ' ' + colname + ' '  + ' { get; set; }
     '
 FROM    (SELECT    column_id ColumnId,
                     ISNULL(t.desctr, '') AS desr,
                     col.name colname,
                      CASE typ.name
                       WHEN 'bigint' THEN 'long'
                       WHEN 'binary' THEN 'byte[]'
                       WHEN 'bit' THEN 'bool'
                       WHEN 'char' THEN 'string'
                       WHEN 'date' THEN 'DateTime'
                       WHEN 'datetime' THEN 'DateTime'
                       WHEN 'datetime2' THEN 'DateTime'
                       WHEN 'datetimeoffset' THEN 'DateTimeOffset'
                       WHEN 'decimal' THEN 'decimal'
                       WHEN 'float' THEN 'float'
                       WHEN 'image' THEN 'byte[]'
                       WHEN 'int' THEN 'int'
                       WHEN 'money' THEN 'decimal'
                       WHEN 'nchar' THEN 'string'
                       WHEN 'ntext' THEN 'string'
                       WHEN 'numeric' THEN 'decimal'
                       WHEN 'nvarchar' THEN 'string'
                       WHEN 'real' THEN 'double'
                       WHEN 'smalldatetime' THEN 'DateTime'
                       WHEN 'smallint' THEN 'short'
                       WHEN 'smallmoney' THEN 'decimal'
                       WHEN 'text' THEN 'string'
                       WHEN 'time' THEN 'TimeSpan'
                       WHEN 'timestamp' THEN 'DateTime'
                       WHEN 'tinyint' THEN 'byte'
                       WHEN 'uniqueidentifier' THEN 'Guid'
                       WHEN 'varbinary' THEN 'byte[]'
                       WHEN 'varchar' THEN 'string'
                       ELSE 'UNKNOWN_' + typ.name
                     END ColumnType ,
                     CASE WHEN col.is_nullable = 1
                               AND typ.name IN ( 'bigint', 'bit', 'date',
                                                 'datetime', 'datetime2',
                                                 'datetimeoffset', 'decimal',
                                                 'float', 'int', 'money',
                                                 'numeric', 'real',
                                                 'smalldatetime', 'smallint',
                                                 'smallmoney', 'time',
                                                 'tinyint', 'uniqueidentifier' )
                          THEN '?'
                          ELSE ''
                     END NullableSign
           FROM      sys.columns col
                    LEFT  JOIN ( SELECT  ISNULL(CAST(g.value AS VARCHAR), '') desctr ,
                                         g.minor_id AS minor_id
                                 FROM    sys.extended_properties g
                                 WHERE   g.major_id = OBJECT_ID(@TableName)
                               ) t ON t.minor_id = col.column_id
                     JOIN sys.types typ ON col.system_type_id = typ.system_type_id
                                           AND col.user_type_id = typ.user_type_id   
           WHERE     object_id = OBJECT_ID(@TableName)
         ) t
 ORDER BY ColumnId;

 SET @Result = @Result + '
 }';

 PRINT @Result;