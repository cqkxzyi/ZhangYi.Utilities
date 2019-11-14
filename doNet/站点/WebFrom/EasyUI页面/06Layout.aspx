<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06Layout.aspx.cs" Inherits="WebFrom.EasyUI页面._06Layout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Layout测试</title>  
   <link href="../Scripts/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
   <link href="../Scripts/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />

   <script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
   <script src="../Scripts/Base/syUtil.js" type="text/javascript"></script>

   <script type="text/javascript" charset="utf-8">
      
   </script>
</head>

<%--Layout 不能删除中间的面板，其他都可以删除--%>
<body id="indexLayout" class="easyui-layout" fit="true">
<form id="form1" method="get">
   <div region="east"  split="true" title="东边的太阳"  iconCls="icon-reload" style="width:200px;"></div> 
	<div region="south" title="南方深圳" style="height:20px;overflow: hidden;"></div>
	<div region="west"  split="true" href="leftTree.aspx"  title="西边导航" style="width:200px;"></div>
	<div region="north" title="北边" style="height:20px;overflow: hidden;"></div>

   <%--针对谷歌浏览器overflow: hidden;--%>
	<div region="center" href="userList.aspx" title=""  split="true" style="overflow: hidden;">
      
   </div>
</form>
</body>
</html>
