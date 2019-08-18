using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class BindList
    {
        #region 初始运营商列表(通过ComboBoxListItem)
        ///// <summary>
        ///// 初始运营商列表
        ///// </summary>
        //public static List<ListItem> IniISPList(bool isNeedDefualt = true, string deDescript = "请选择...")
        //{
        //    List<IsP> list = IsP.GetAllIsP();

        //    List<ComboBoxListItem> retnlist = new List<ComboBoxListItem>();
        //    foreach (IsP item in list)
        //    {
        //        retnlist.Add(new ComboBoxListItem(item.Name, item.ID.ToString()));
        //    }

        //    if (isNeedDefualt)
        //    {
        //        retnlist.Insert(0, new ComboBoxListItem(deDescript, "0"));
        //    }

        //    return retnlist;
        //}
        #endregion


        #region 初始化类别列表（通过枚举）
        /// <summary>
        /// 初始化类别列表（通过枚举）
        /// </summary>
        /// <param name="isNeedAll">是否需要 枚举中的全部项"-1" 可选 默认为 false</param>
        /// <param name="isNeedDefault">是否需要增加默认选项"请选择" 可选 默认为 false</param>
        /// <param name="DefaultDescription">默认选项 显示文本 可选 默认为 请选择</param>
        /// <param name="DefaultValue">默认选项的值 可选 默认为 "" </param>
        public static List<EnumItem> IniCategoryList(bool isNeedAll = false, bool isNeedDefault = false,
                                               string DefaultDescription = "请选择", string DefaultValue = "")
        {
            List<EnumItem> CategoryList = GetEnumItem(typeof(MyEnum.Category), isNeedAll, isNeedDefault, DefaultDescription, DefaultValue);
            return CategoryList;
        }
        #endregion

        #region 初始化数据库字段列表（通过枚举）
        /// <summary>
        /// 初始化数据库字段列表（通过枚举）
        /// </summary>
        /// <param name="isNeedAll">是否需要 枚举中的全部项"-1" 可选 默认为 false</param>
        /// <param name="isNeedDefault">是否需要增加默认选项"请选择" 可选 默认为 false</param>
        /// <param name="DefaultDescription">默认选项 显示文本 可选 默认为 请选择</param>
        /// <param name="DefaultValue">默认选项的值 可选 默认为 "" </param>
        public static List<EnumItem> IniDBColumnNameList(bool isNeedAll = false, bool isNeedDefault = false,
                                               string DefaultDescription = "请选择", string DefaultValue = "")
        {
            List<EnumItem> DBColumnNameList = GetEnumItem(typeof(MyEnum.DBColumnName), isNeedAll, isNeedDefault, DefaultDescription, DefaultValue);
            return DBColumnNameList;
        }
        #endregion

        #region 初始化性别列表（通过枚举）
        /// <summary>
        /// 初始化性别列表（通过枚举）
        /// </summary>
        /// <param name="isNeedAll">是否需要 枚举中的全部项"-1" 可选 默认为 false</param>
        /// <param name="isNeedDefault">是否需要增加默认选项"请选择" 可选 默认为 false</param>
        /// <param name="DefaultDescription">默认选项 显示文本 可选 默认为 请选择</param>
        /// <param name="DefaultValue">默认选项的值 可选 默认为 "" </param>
        public static List<EnumItem> IniSexList(bool isNeedAll = false, bool isNeedDefault = false,
                                               string DefaultDescription = "请选择", string DefaultValue = "")
        {
            List<EnumItem> SexList = GetEnumItem(typeof(MyEnum.Sex), isNeedAll, isNeedDefault, DefaultDescription, DefaultValue);
            return SexList;
        }
        #endregion
    

        #region 获取枚举实体项列表
        /// <summary>
        /// 获取枚举实体项列表
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="isNeedAll">是否需要 枚举中的全部项"-1" 可选 默认为 false</param>
        /// <param name="isNeedDefault">是否需要增加默认选项"请选择" 可选 默认为 false</param>
        /// <param name="DefaultDescription">默认选项 显示文本 可选 默认为 请选择</param>
        /// <param name="DefaultValue">默认选项的值 可选 默认为 "" </param>
        /// <param name="FilterValues">需要过滤的枚举项值 多个请用逗号分隔</param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItem(Type type, bool isNeedAll = false,bool isNeedDefault = false, string DefaultDescription = "请选择",
                                                 string DefaultValue = "", string FilterValues = "")
        {
            List<EnumItem> enumItemList = null;
            List<string[]> strList = MyEnum.GetEnumOption(type);
            EnumItem enumItem = null;
            bool isStop = false;
            string[] szFilterValues = null;


            if (!string.IsNullOrEmpty(FilterValues) && FilterValues.Contains(","))
            {
                szFilterValues = FilterValues.Split(',');
            }
            else if (!string.IsNullOrEmpty(FilterValues) && FilterValues.Length > 0)
            {
                szFilterValues = new string[1];
                szFilterValues[0] = FilterValues;
            }

            if (null != strList && strList.Count > 0)
            {
                enumItemList = new List<EnumItem>();

                if (isNeedDefault)
                {
                    enumItem = new EnumItem();
                    enumItem.Description = DefaultDescription;
                    enumItem.Value = DefaultValue;
                    enumItem.Code = "";
                    enumItemList.Add(enumItem);
                }

                foreach (var item in strList)
                {
                    isStop = false;

                    if (!isNeedAll && item[1].ToString() == "-1")
                    {
                        isStop = true;
                    }

                    if (null != szFilterValues && szFilterValues.Length > 0)
                    {
                        for (int k = 0; k < szFilterValues.Length; k++)
                        {
                            if (item[1].ToString() == szFilterValues[k].ToString())
                            {
                                isStop = true;
                            }
                        }
                    }

                    if (isStop)
                        continue;

                    enumItem = new EnumItem();
                    enumItem.Description = item[0].ToString();
                    enumItem.Value = item[1].ToString();
                    enumItem.Code = item[2].ToString();

                    if (item[1].ToString() == "-1")
                    {
                        enumItemList.Insert(0, enumItem);
                    }
                    else
                    {
                        enumItemList.Add(enumItem);
                    }
                }
            }
            return enumItemList;
        }
        #endregion
    }
