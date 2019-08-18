<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04登录演示.aspx.cs" Inherits="WebFrom.EasyUI页面._02Dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>jqueryEasyUI登录演示</title>
   <link href="../Scripts/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
   <link href="../Scripts/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/syUtils.js" type="text/javascript"></script>
   <script type="text/javascript" charset="utf-8">
	var loginAndRegDialog;
	var regDialog;
	var loginTabs;

	var loginDatagridName;
	var loginComboboxName;
   
	var loginInputForm;
	var regForm;
	var loginDatagridForm;
	var loginComboboxForm;

      $(function () {
         //登录和注册框
         loginAndRegDialog = $("#loginAndRegDialog").dialog({
            title: "登录窗口",
            modal: true, //modal的属性是从window中继承的。
            closed: false,
            closable: false,
            buttons: [{
               text: '去注册',
               handler: function () {
                  regDialog.dialog('open');
//                  if (loginInputForm.form("validate")) {//先验证是否填写完整
//                     $.ajax({
//                        type: "get",
//                        cache: false,
//                        dataType: "text",
//                        url: "/Handler1.ashx",
//                        data: $("#loginInputForm").serialize(),
//                        success: function (data) {
//                           loginAndRegDialog.dialog("close");
//                           $.messager.show({
//                              title: "提示",
//                              msg: data
//                           });
//                        },
//                        error: function (XMLHttpRequest, textStatus, errorThrown) {
//                           alert("服务器处理失败！");
//                        }
//                     });
//                  }
               }
            },
            {
               text: '登录',
               handler: function () {
                  loginInputForm.submit();
               }
            }
            ],
            onOpen: function () {
               $("#userName").focus();
            },
            onClose: function () {

            }
         });

         //登陆框form提交 他是普通的文本提交用了一个隐藏ifram域，ajax是异步提交，
         //手动验证form是否通过验证:$("#loginInputForm").form("validate")
         loginInputForm = $('#loginInputForm').form({
            url: '/Handler1.ashx',
            success: function (data) {
               var d = $.parseJSON(data);
               if (d.success) {
                  alert(d.name);
                  //                  loginAndRegDialog.dialog('close');
                  //                  $('#indexLayout').layout('panel', 'center').panel('setTitle', sy.fs('[{0}]，欢迎您！[{1}]', d.obj.name, d.obj.ip));
               } else {
                  loginInputForm.find('input[name=password]').focus();
               }

               $.messager.show({
                  msg: d.name,
                  title: '提示'
               });
            }
         });
         	

      //注册框
      regDialog = $('#regDialog').show().dialog({
			modal : true,
			title : '注册',
			closed : true,
			buttons : [ {
				text : '注册',
				handler: function () {
					regForm.submit();
				}
			} ],
			onOpen : function() {
				setTimeout(function() {
					regForm.find('input[name=name]').focus();
				}, 1);
			},
			onClose : function() {
//				loginTabs.tabs('select', 0);
			}
		});

      //注册框form提交
      regForm = $('#regForm').form({
			url : '/Handler1.ashx',
			success : function(data) {
				var d = $.parseJSON(data);
				if (d.success) {
					regDialog.dialog('close');
					loginInputForm.find('input[name=name]').val(regForm.find('input[name=name]').val());
					loginInputForm.find('input[name=password]').val(regForm.find('input[name=password]').val());
					loginInputForm.submit();//如果注册成功，就顺便登录了。
				} else {
					regForm.find('input[name=name]').focus();
				}
				$.messager.show({
					msg : d.msg,
					title : '提示'
				});
			}
		});	



      });
   </script>
<script type="text/javascript" charset="utf-8">
   $(function () {
      loginInputForm.find('input').on('keyup', function (event) {/* 增加回车提交功能 */
         if (event.keyCode == '13') {
            loginInputForm.submit();
         }
      });
      regForm.find('input').on('keyup', function (event) {/* 增加回车提交功能 */
         if (event.keyCode == '13') {
            regForm.submit();
         }
      });
   })
</script>
</head>
<body id="mybody">
      <div id="loginAndRegDialog" title="dialog标题" style="width: 300px; height: 200px">
         <form id="loginInputForm" name="loginInputForm" runat="server" method="get">
         <input id="postType" name="postType" type="hidden" value="login" />
            <p>
               <label>
                  用户名</label>
               <input id="userName" name="userName" class="easyui-validatebox" required="true" type="text" value="aaa" />
            </p>
            <p>
               <label>
                  密码：</label>
               <input id="password" name="password" class="easyui-validatebox" required="true" type="password"  />
            </p>
         </form>
      </div>

<div id="regDialog" style="width:250px;display: none;padding: 5px;" align="center">
	<form id="regForm" method="post">
      <input id="Hidden1" name="postType" type="hidden" value="reg" />
		<table class="tableForm">
			<tr>
				<th>登录名</th>
				<td><input name="name" class="easyui-validatebox" required="true" /></td>
			</tr>
			<tr>
				<th>密码</th>
				<td><input name="password" type="password" class="easyui-validatebox" required="true" /></td>
			</tr>
			<tr>
				<th>重复密码</th>
				<td><input name="rePassword" type="password" class="easyui-validatebox" required="true" validType="eqPassword['#regForm input[name=password]']" /></td>
			</tr>
		</table>
	</form>
</div>
</body>
</html>
