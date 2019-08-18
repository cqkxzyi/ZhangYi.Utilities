<%--<script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
<script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>--%>

<script type="text/javascript">
   var _dataTable;
   var _admin_user_searchForm;
   var _nowEditRowIndex = undefined; //当前编辑行的索引

   $(function () {
       _dataTable = $("#_dataTable").datagrid({
           url: "/Handler1.ashx?postType=userList",
           title: "用户管理",
           iconCls: "icon-save",
           singleSelect: true,
           pagination: true,
           pageSize: 5,
           pageList: [5, 10, 15],
           fit: true,
           fitColumns: false, //如果有冻结列 ，这里就一定要设置成false
           nowrap: true,      //当数据长度超出列宽时将会自动截取。
           border: false,
           idField: "id", //可以跨页面选定操作
           loadMsg: "正在加载数据，请稍等...",
           sortName: "LogName",
           sortOrder: "asc",
           rowStyler: function (rowIndex, rowData) {
               if (rowData.UserName == "12") {
                   return "background:red";
               }
           },
           frozenColumns: [[//冻结咧
            {title: "编号", width: 100, sortable: false, checkbox: true },
            { title: "登录名", field: "LogName", width: 100, sortable: true,
                editor: { type: "validatebox", options: { required: true} }
            }
         ]],
           columns: [[
                  { title: "姓名", field: "UserName", width: 150, sortable: true,
                      editor: { type: "validatebox", options: { required: true} },
                      align: "right"
                  },
                  { title: "手机号码", field: "Mobile", width: 150, sortable: true,
                      editor: { type: "validatebox", options: { required: true} }
                  },
                  { title: "创建时间", field: "CreateDate", width: 150, sortable: true,
                      formatter: function (value, rowData, rowIndex) {
                          return value;
//                          return new Date(value).format("yyyy年MM月dd日 hh:mm:ss");
                      }
                  },
                  { title: "地址", field: "Address", width: 150, sortable: true,
                      editor: { type: "validatebox", options: { required: true} },
                      formatter: function (value, rowData, rowIndex) { //一定要返回一个字符串
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: "操作", field: "id", width: 150, sortable: false,
                      formatter: function (value, rowData, rowIndex) { //1：密码变成*，2：:弹出提示功能，3：列可以变成按钮,4:数据格式
                          return "<button onclick='show(" + rowIndex + ")'>编辑</button>";
                      },
                      editor: { type: "validatebox", options: { required: true} }
                  }
               ]],
           toolbar: //toolbar可以单独指定一个div 的id，例如：toolbar:"#_div"  好处就是：可以创建多行的div
          [
          { text: "增加", iconCls: "icon-add",
              handler: function () {
                  if (_nowEditRowIndex == undefined) {
                      _dataTable.datagrid("addEditor", {//扩展方法
                          field: "CreateDate",
                          editor: { type: "datetimebox", options: { required: true, editable: false} }
                      });

                      _dataTable.datagrid("insertRow", {
                          index: 0,
                          row: {
                              id: sy.UUID()
                          }
                      });
                      //var editIndex = _dataTable.datagrid("getRows").length;
                      _dataTable.datagrid("beginEdit", 0);
                      _nowEditRowIndex = 0;
                  }
              }
          }, "-",
          { text: "删除", iconCls: "icon-remove",
              handler: function () {
                  var Rows = _dataTable.datagrid("getSelections");
                  if (Rows.length > 0) {
                      $.messager.confirm("请确认", "真的要删除吗？", function (data) {
                          if (data) {
                              var ids = [];
                              for (var i = 0; i < Rows.length; i++) {
                                  ids.push(Rows[i].id);
                              }

                              $.ajax({
                                  url: "/Handler1.ashx?postType=DeleteUser",
                                  data: { ids: ids.join(",") },
                                  dataType: "json",
                                  success: function (data) {
                                      if (data && data.success) {
                                          $.messager.alert("系统提示", "刪除成功！", "info");
                                          _nowEditRowIndex = undefined;
                                          _dataTable.datagrid("load");
                                          _dataTable.datagrid("unselectAll");
                                      }
                                      else {
                                          $.messager.alert("系统提示", "系统出錯！", "error");
                                          _dataTable.datagrid("unselectAll");
                                      }
                                  }
                              });
                          }
                      });
                  }
                  else {
                      $.messager.alert("系统提示", "请选择要删除的数据！", "error");
                  }
              }
          }, "-",
          { text: "修改", iconCls: "icon-edit",
              handler: function () {
                  var Rows = _dataTable.datagrid("getSelections");
                  if (Rows.length == 1) {
                      if (_nowEditRowIndex != undefined) {
                          _dataTable.datagrid("endEdit", _nowEditRowIndex);
                      }

                      if (_nowEditRowIndex == undefined) {
                          _dataTable.datagrid("removeEditor", "CreateDate"); //扩展方法

                          var index = _dataTable.datagrid("getRowIndex", Rows[0]);
                          _dataTable.datagrid("beginEdit", index);
                          _nowEditRowIndex = index;
                          _dataTable.datagrid("unselectAll");
                      }
                  }
              }
          }, "-",
          { text: "保存", iconCls: "icon-save",
              handler: function () {
                  _dataTable.datagrid("endEdit", _nowEditRowIndex);

              }
          }, "-",
          { text: "取消", iconCls: "icon-redo",
              handler: function () {
                  _nowEditRowIndex = undefined;
                  _dataTable.datagrid("rejectChanges");
                  _dataTable.datagrid("unselectAll");
              }
          }, "-",
          { text: "清空数据", iconCls: "icon-redo",
              handler: function () {
                  _dataTable.datagrid("loadData", []);
              }
          }, "-"
         ], //[[]]支持多级表头

           onAfterEdit: function (rowIndex, rowData, changes) {//rowData：刚刚结束编辑的哪一行；changes：被修改过的数据
               var inserteds = _dataTable.datagrid("getChanges", "inserted");
               var updateds = _dataTable.datagrid("getChanges", "updated");
               var _url;
               if (inserteds.length > 0) {
                   _url = "/Handler1.ashx?postType=AddUser";
               }
               else {
                   _url = "/Handler1.ashx?postType=EditUser";
               }
               $.ajax({
                   url: _url,
                   data: rowData,
                   dataType: "json",
                   success: function (data) {
                       if (data && data.success) {
                           $.messager.show({ msg: data.msg, title: "成功！" });
                           _dataTable.datagrid("acceptChanges"); //确定提交，前台修改状态使用
                       }
                       else {
                           $.messager.alert("系统提示", "系统出錯！", "error");
                           _dataTable.datagrid("rejectChanges");
                       }
                       _nowEditRowIndex = undefined;
                       _dataTable.datagrid("unselectAll");
                   }
               });
           },
           //双击行事件
           onDblClickRow: function (rowIndex, rowData) {
               if (_nowEditRowIndex != undefined) {
                   _dataTable.datagrid("endEdit", _nowEditRowIndex);
               }

               if (_nowEditRowIndex == undefined) {
                   _dataTable.datagrid("beginEdit", rowIndex);
                   _nowEditRowIndex = rowIndex;
               }
           },
           //右键菜单事件
           onRowContextMenu: function (e, rowIndex, rowData) {
               e.preventDefault();
               _dataTable.datagrid("unselectAll");
               _dataTable.datagrid("selectRow", rowIndex);
               $('#menu').menu('show', {
                   left: e.pageX,
                   top: e.pageY
               });
           }
       });
       $(".datagrid-header div").css('textAlign', 'center');
   });

    //查询方法
    function Search() {
       _admin_user_searchForm = $("#_admin_user_searchForm").form();
       var obj = sy.serializeObject(_admin_user_searchForm);//扩展的方法

       _dataTable.datagrid('load', obj); //查询时，最好用load，从1页开始查询，reload是从当前页码开始查询，如果没有数据就会不好了
    }
    //清空方法
    function ClearSearch() {
       _admin_user_searchForm.find("input").val("");
       _dataTable.datagrid('load', {});
    }
    //显示
    function show(i) {
       alert(i);
       var rows = _dataTable.datagrid('getRows');
       var selectRow = rows[i];
       alert(selectRow);
    }


</script>

<div class="easyui-tabs" fit="true" border="false">
   <div title="用户管理" border="false" >
      <div class="easyui-layout" fit="true" border="false">
         <div region="north" title="查询" style="height:100px; overflow: hidden;">
            <form id="_admin_user_searchForm">
               <table class="tableForm datagrid-toolbar" style="width:100%;height:100%">
                  <tr>
                     <th style="width:60px;">
                        用户名：
                     </th>
                     <td>
                        <input id="name" name="name"/>
                     </td>
                  </tr>
                  <tr>
                     <th>
                        创建时间：
                     </th>
                     <td>
                        <input id="begintime" name="begintime" editable="false" class="easyui-datetimebox" style="width:150px;"/>
                        至：
                        <input id="endtime" name="endtime" editable="false" class="easyui-datetimebox" style="width:150px;"/>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="Search()">查询</a>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearSearch()">清空</a>
                     </td>
                  </tr>
               </table>
            </form>
         </div>
         <div region="center" title="" fit="true" split="true" style="overflow: hidden;">
            <table id="_dataTable" fit="true">
            </table>
         </div>
      </div>
   </div>
</div>

    <div id="menu" class="easyui-menu" style="width:120px;display: none;">
        <div onclick="append();" iconCls="icon-add">增加</div>
        <div onclick="remove();" iconCls="icon-remove">删除</div>
        <div onclick="edit();" iconCls="icon-edit">编辑</div>
    </div>


