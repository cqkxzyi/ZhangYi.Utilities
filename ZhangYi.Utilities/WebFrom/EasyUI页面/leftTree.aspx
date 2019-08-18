<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leftTree.aspx.cs" Inherits="WebFrom.EasyUI页面.leftTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
   <script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
<script type="text/javascript">
   $(function () {
      $("#tt").tree({
         url: "/Handler1.ashx?postType=getTree",
         lines:true,
         checkbox:true,
         dnd:true
      });
   })
</script>
   <ul id="tt" class="easyui-tree">
      <li><span>系统管理</span>
         <ul>
            <li><span>常规管理</span>
               <ul>
                  <li><span><a href="#">File 11</a></span> </li>
                  <li><span>File 12</span> </li>
                  <li><span>File 13</span> </li>
               </ul>
            </li>
            <li><span>File 2</span> </li>
            <li><span>File 3</span> </li>
         </ul>
      </li>
      <li><span>其他</span> </li>
   </ul>
</body>
</html>
