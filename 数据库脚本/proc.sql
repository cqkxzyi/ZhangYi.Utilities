
    /*--调用示例       
    exec       p_show       'select       *       from       jobs ',5,3       
        
    exec       p_show       'select       top       100       percent       *       from       地区资料       order       by       地区名称 ',5,3, '地区编号,地区名称,助记码 '       
    --*/       
    CREATE       Proc       p_show       
    @QueryStr       nvarchar(4000),   --表名、视图名、查询语句       
    @PageSize       int=10,   --每页的大小(行数)       
    @PageCurrent       int=1,   --要显示的页       
    @FdShow       nvarchar       (4000)= ' ',   --要显示的字段列表,如果查询结果不需要标识字段,需要指定此值,且不包含标识字段       
    @FdOrder       nvarchar       (1000)= ' '   --排序字段列表       
    as       
    set       nocount       on       
    declare       @FdName       nvarchar(250)   --表中的主键或表、临时表中的标识列名       
    ,@Id1       varchar(20),@Id2       varchar(20)   --开始和结束的记录号       
    ,@Obj_ID       int   --对象ID       
    --表中有复合主键的处理       
    declare       @strfd       nvarchar(2000)   --复合主键列表       
    ,@strjoin       nvarchar(4000)   --连接字段       
    ,@strwhere       nvarchar(2000)   --查询条件       
        
        
    select       @Obj_ID=object_id(@QueryStr)       
    ,@FdShow=case       isnull(@FdShow, ' ')       when       ' '       then       '       * '       else       '       '+@FdShow       end       
    ,@FdOrder=case       isnull(@FdOrder, ' ')       when       ' '       then       ' '       else       '       order       by       '+@FdOrder       end       
    ,@QueryStr=case       when       @Obj_ID       is       not       null       then       '       '+@QueryStr       else       '       ( '+@QueryStr+ ')       a '       end       
        
    --如果显示第一页，可以直接用top来完成       
    if       @PageCurrent=1       
    begin       
    select       @Id1=cast(@PageSize       as       varchar(20))       
    exec( 'select       top       '+@Id1+@FdShow+ '       from       '+@QueryStr+@FdOrder)       
    return       
    end       
        
    --如果是表,则检查表中是否有标识更或主键       
    if       @Obj_ID       is       not       null       and       objectproperty(@Obj_ID, 'IsTable ')=1       
    begin       
    select       @Id1=cast(@PageSize       as       varchar(20))       
    ,@Id2=cast((@PageCurrent-1)*@PageSize       as       varchar(20))       
        
    select       @FdName=name       from       syscolumns       where       id=@Obj_ID       and       status=0x80       
    if       @@rowcount=0   --如果表中无标识列,则检查表中是否有主键       
    begin       
    if       not       exists(select       1       from       sysobjects       where       parent_obj=@Obj_ID       and       xtype= 'PK ')       
    goto       lbusetemp   --如果表中无主键,则用临时表处理       
        
    select       @FdName=name       from       syscolumns       where       id=@Obj_ID       and       colid       in(       
    select       colid       from       sysindexkeys       where       @Obj_ID=id       and       indid       in(       
    select       indid       from       sysindexes       where       @Obj_ID=id       and       name       in(       
    select       name       from       sysobjects       where       xtype= 'PK '       and       parent_obj=@Obj_ID       
    )))       
    if       @@rowcount> 1   --检查表中的主键是否为复合主键       
    begin       
    select       @strfd= ' ',@strjoin= ' ',@strwhere= ' '       
    select       @strfd=@strfd+ ',[ '+name+ '] '       
    ,@strjoin=@strjoin+ '       and       a.[ '+name+ ']=b.[ '+name+ '] '       
    ,@strwhere=@strwhere+ '       and       b.[ '+name+ ']       is       null '       
    from       syscolumns       where       id=@Obj_ID       and       colid       in(       
    select       colid       from       sysindexkeys       where       @Obj_ID=id       and       indid       in(       
    select       indid       from       sysindexes       where       @Obj_ID=id       and       name       in(       
    select       name       from       sysobjects       where       xtype= 'PK '       and       parent_obj=@Obj_ID       
    )))       
    select       @strfd=substring(@strfd,2,2000)       
    ,@strjoin=substring(@strjoin,5,4000)       
    ,@strwhere=substring(@strwhere,5,4000)       
    goto       lbusepk       
    end       
    end       
    end       
    else       
    goto       lbusetemp       
        
    /*--使用标识列或主键为单一字段的处理方法--*/       
    lbuseidentity:       
    exec( 'select       top       '+@Id1+@FdShow+ '       from       '+@QueryStr       
    + '       where       '+@FdName+ '       not       in(select       top       '       
    +@Id2+ '       '+@FdName+ '       from       '+@QueryStr+@FdOrder       
    + ') '+@FdOrder       
    )       
    return       
        
    /*--表中有复合主键的处理方法--*/       
    lbusepk:       
    exec( 'select       '+@FdShow+ '       from(select       top       '+@Id1+ '       a.*       from       
    (select       top       100       percent       *       from       '+@QueryStr+@FdOrder+ ')       a       
    left       join       (select       top       '+@Id2+ '       '+@strfd+ '           
    from       '+@QueryStr+@FdOrder+ ')       b       on       '+@strjoin+ '       
    where       '+@strwhere+ ')       a '       
    )       
    return       
        
    /*--用临时表处理的方法--*/       
    lbusetemp:       
    select       @FdName= '[ID_ '+cast(newid()       as       varchar(40))+ '] '       
    ,@Id1=cast(@PageSize*(@PageCurrent-1)       as       varchar(20))       
    ,@Id2=cast(@PageSize*@PageCurrent-1       as       varchar(20))       
        
    exec( 'select       '+@FdName+ '=identity(int,0,1), '+@FdShow+ '       
    into       #tb       from(select       top       100       percent       *       from '+@QueryStr+@FdOrder+ ')a       
    select       '+@FdShow+ '       from       #tb       where       '+@FdName+ '       between       '       
    +@Id1+ '       and       '+@Id2       
    ) 
GO 

