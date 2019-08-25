<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="16combo.aspx.cs" Inherits="WebFrom.EasyUI页面.comboBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>自定义组合下拉框 - Combo</title>
   <script src="../Scripts/JQuery/jquery-1.8.0.min.js" type="text/javascript"></script>
   <script src="../Scripts/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
   <script type="text/javascript">
      $(function () {
         $('#cc').combo({
            required: true,
            editable: false,
            multiple:true  
         });
         $('#sp').appendTo($('#cc').combo('panel'));
         $('#sp input').click(function () {
            var v = $(this).val();
            var s = $(this).next('span').text();
            $('#cc').combo('setValue', v).combo('setText', s);
         });
      }); 
   </script>
   <form id="form1" runat="server">
   <div style="margin-top: 10px;">

      <input id="cc" value="001">

      <div id="sp">
         <div style="color: #99BBE8; background: #fafafa; padding: 5px;">请选择一门开发语言</div>
         <input type="radio" name="lang" value="01"><span>Java</span><br />
         <input type="radio" name="lang" value="02"><span>C#</span><br />
         <input type="radio" name="lang" value="03"><span>神州</span><br />
         <input type="radio" name="lang" value="04"><span>重庆</span><br />
         <input type="radio" name="lang" value="05"><span>成都</span>
      </div>
   </div>
   </form>
</body>
</html>
