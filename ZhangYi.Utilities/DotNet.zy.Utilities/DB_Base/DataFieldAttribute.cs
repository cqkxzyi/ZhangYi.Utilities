/*mysql 数据库操作方法
 * 
 * 版权所有：2010张毅
 * 开发部门：IT 
 * 程序负责：zhangyi
 * 电话：13594663608
 * 其他联系：重庆市、深圳市
 * Email：kxyi-lover@163.com
 * MSN：10011
 * QQ:284124391
 * 
 * 开发时间：2012年2月28日
 * 声明：仅限于您自己使用，不得进行商业传播，违者必究！
 */
using System;
using System.Collections.Generic;
using System.Text;

  public class DataFieldAttribute : Attribute
  {
    private string _FieldName;
    private string _PK;
    public DataFieldAttribute(string fieldname)
    {
      this._FieldName = fieldname;
    }

    public DataFieldAttribute(string fieldname, string pk)
    {
      this._FieldName = fieldname;
      this._PK = pk;
    }

    public string PK
    {
      get { return _PK; }
      set { _PK = value; }
    }

    public string FieldName
    {
      get { return this._FieldName; }
      set { this._FieldName = value; }
    }
  }
