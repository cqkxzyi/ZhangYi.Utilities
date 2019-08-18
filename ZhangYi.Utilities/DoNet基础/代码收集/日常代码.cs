using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 日常代码
{

   public class 其他
   {
       public void DataSQL()
       {
           //
           //调用存储过程
           //
           SqlConnection myConnection = new SqlConnection("链接字符串");
           myConnection.Open();
           SqlCommand myCommand = new SqlCommand("存储过程名称", myConnection);
           myCommand.CommandType = CommandType.StoredProcedure;

           SqlParameter para = new SqlParameter("@name", SqlDbType.Char, 10);
           para.Direction = ParameterDirection.Input;
           para.Value = "名字";
           SqlParameter para2 = new SqlParameter("@pwd", SqlDbType.Char, 10);
           para.Direction = ParameterDirection.Input;
           para.Value = "密码";
           SqlParameter para3 = new SqlParameter("@ISValid", SqlDbType.Char, 10);
           para3.Direction = ParameterDirection.ReturnValue;

           myCommand.Parameters.Add(para);
           myCommand.Parameters.Add(para3);
           myCommand.Parameters.Add("@ContentType", SqlDbType.VarChar, 50).Value = UpFile.ContentType;//三个属性同时插入进去了
           myCommand.ExecuteNonQuery();
           string isvalid = myCommand.Parameters["@ISvalid"].Value.ToString(); //返回值
           myCommand.Parameters.Clear();

           myCommand.Parameters.AddWithValue("@名称", "数值");//更为简洁的方法添加参数
           myCommand.Parameters.Add(new SqlParameter("@名称", "值"));//用于传递一个参数到sql语句中，建议在sql语句要用参数时，都以这样的方式加参数，防止注入式攻击。

       }

       public void QtTa()
       {
           //将窗体总是显示在最前端
           //this.TopMost = true;	
       }

   }   
}
//VisualHelper.SetDependencyPropertyValue(this.dgWarning, DataGrid.DataContextProperty, list);     需要在代码里面绑定  ItemsSource="{Binding}
//VisualHelper.SetDependencyPropertyValue(this.dgWarning, DataGrid.ItemsSourceProperty, list);则不需要在页面绑定
//MainWindow mainWindow = MainWindow as MainWindow;   mainWindow.OpenWindow(typeof(TroubleViewDeleteWindow)); //获取程序运行的主窗体
//弹出新窗口  AddExperienceLibraryWindow addWindow = new AddExperienceLibraryWindow(exLibraryEntity);	
//                 addWindow.ShowDialog();

//=====================================================报表========================================
//ShowRefreshButton="False" ShowGroupTreeButton="False" DisplayGroupTree="False" DisplayToolbar="False" DisplayStatusBar="False" EnableDrillDown="False"
//======================================================C#================================
//转换成特定的格式：  
//StringFormat=yyyy-MM-dd HH:mm:ss
//数据库表关系设计中，多个对应一个，那么箭头就指向一个那方.(有汇总意义的提取出来做外键表)
//字符串分开成数组： string[] strTag = chectBox.Tag.ToString().Split('*'); ;
//字符串处理  cmbStartYear.Text.Replace('年', '-')  
//移除指定的string.remove(参数);   
//截取指定的string.Substring()
//数据库中转换DateTime  to_char(add_months(to_date(#StartDate#,'yyyy-mm-dd'),-12),'yyyy-mm-dd')
//获取光标，使之成为沙漏形状
 //Windows.Forms.Cursor.Current = Cursors.WaitCursor  


//======================================================其他=======================================
//#region  Fields字段   Constructors构造器  Events事件 Methods方法  Properties属性
//JS调用随机数  var r=Math.random()
//关闭应用程序 Application.Current.Shutdown();
//获取正则表达式的匹配结果 bool check = Regex.IsMatch(timeLength, @"^[0-9]*$");
//  .net 通用转义含义Environment.NewLine   c#中用\   vb中使用 ""  来进行转义
//Oracle连接字符串" Persist Security Info=True;User ID=testunicom;Password=testunicom;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.10.1.59)(PORT=1521)))(CONNECT_DATA=(SID=kbc)(SERVER=DEDICATED)))";

//打开本地程序
 Process.Start("IExplore.exe", "www.northwindtraders.com"); //附加要打开的具体信息
ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
  startInfo.Arguments = "www.northwindtraders.com";
 Process.Start(startInfo);
 string myFavoritesPath = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);//打开本地文件系统

