<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02Dialog.aspx.cs" Inherits="WebFrom.EasyUI页面._02Dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>jqueryEasyUI测试学习</title>
   <link href="../Scripts/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
   <link href="../Scripts/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
   <script type="text/javascript" charset="utf-8">
      $(function () {
      //复杂的组件建议用js进行，简单的可以用class的方式
         $("#_div").dialog({
            title: "登录窗口",
            modal: true, //modal的属性是从window中继承的。
            closed: false,
            buttons: [{
               text: '注册',
               handler: function () {
                  regForm.submit();
               }
            },
            {
               text: '登录',
               handler: function () {
                  regForm.submit();
               }
            }
            ],
            onOpen: function () {
               setTimeout(function () {
                  regForm.find('input[name=name]').focus();
               }, 1);
            },
            onClose: function () {
               loginTabs.tabs('select', 0);
            }
         });

         //console.info($("_div"));

         $('#p').panel({
            width: 500,
            height: 150,
            title: 'Panel标题',
            collapsible: true,
            closable: true,
            doSize: true,
            tools: [{
               iconCls: 'icon-add',
               handler: function () { alert('new') }
            }, {
               iconCls: 'icon-save',
               handler: function () { alert('save') }
            }]
         });
      });

      //easyui可以动态加载需要的文件,可以动态引用js、css
      easyloader.load("message", function () {
         $.messager.alert("Title", "信息哦！");
      });

      using("message", function () {
         $.messager.alert("Title", "信息哦！");
      });

   </script>
</head>
<body>
   <form id="form1" runat="server" method="post">
   <div id="_div" title="dialog标题" style="width: 300px; height: 200px">
      <p>
         <label>
            用户名</label>
         <input id="_userName" type="text" />
      </p>
      <p>
         <label>
            密码：</label>
         <input id="Text1" type="password" />
      </p>
   </div>
   <div id="p" style="padding: 10px;">
      <p>
         panel 啊啊.</p>
      <p>
         panel 拜拜.</p>
   </div>
   </form>
</body>
</html>
