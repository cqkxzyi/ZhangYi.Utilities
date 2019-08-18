using System;

/// <summary>
/// 运行库是否在有效期内
/// </summary>
public class IsEnrollAA
{
    CommonDAL commonDAL = new CommonDAL();
    /// <summary>
    /// 判断是否可用资源文件
    /// </summary>
    /// <returns></returns>
    public static bool IsEnroll()
    {
        DateTime nowDate = DateTime.Parse(CommonDAL.GetServerDate(""));
        DateTime endDate = DateTime.Parse("2015-12-25");

        int haha = DateTime.Compare(endDate, nowDate);
        if (haha > 0)
        {
            return true;
        }
        else
        {
           JsHelper.MsgBox("DoNetFrame is null,引用为空，无法判断其引用地址!  请联系软件原著作者！");
           JsHelper.MsgBox("DoNetFrame is null,引用为空！使用了非法的操作！");
            return false;
        }
    }

}