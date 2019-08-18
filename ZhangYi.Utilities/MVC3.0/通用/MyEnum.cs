using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;


    /// <summary>
    /// 枚举类
    /// </summary>
    public static class MyEnum
    {
        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = -1,
            /// <summary>
            /// 男
            /// </summary>
            [Description("男")]
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Description("女")]
            Female = 2
        }
        #endregion

        #region 运营商列别
        /// <summary>
        /// 运营商列别
        /// </summary>
        public enum Category
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = -1,
            /// <summary>
            /// 移动
            /// </summary>
            [Description("移动")]
            yidong = 1,
            /// <summary>
            /// 联通
            /// </summary>
            [Description("联通")]
            liantong = 2,
            /// <summary>
            /// 电信
            /// </summary>
            [Description("电信")]
            dianxin = 3
        }
        #endregion

        #region 运营商列别
        /// <summary>
        /// 运营商列别
        /// </summary>
        public enum DBColumnName
        {
            /// <summary>
            /// 姓名
            /// </summary>
            [Description("姓名")]
            Name = 0,
            /// <summary>
            /// 密码
            /// </summary>
            [Description("密码")]
            Pwd = 1,
            /// <summary>
            /// 性别
            /// </summary>
            [Description("性别")]
            Sex = 2,
            /// <summary>
            /// 身份证号码
            /// </summary>
            [Description("身份证号码")]
            Identity = 3,
            /// <summary>
            /// 手机
            /// </summary>
            [Description("手机")]
            MobilePhone = 4,
            /// <summary>
            /// 电话
            /// </summary>
            [Description("电话")]
            Telephone = 5,
            /// <summary>
            /// 邮箱
            /// </summary>
            [Description("邮箱")]
            Email = 6,
            /// <summary>
            /// 联系地址
            /// </summary>
            [Description("联系地址")]
            Address = 7,
            /// <summary>
            /// 邮编
            /// </summary>
            [Description("邮编")]
            Postcode = 8,
            /// <summary>
            /// 区域
            /// </summary>
            [Description("区域")]
            Area = 9,
            /// <summary>
            /// 学历
            /// </summary>
            [Description("学历")]
            Education = 10,
            /// <summary>
            /// 类别
            /// </summary>
            [Description("类别")]
            Category = 11
        }
        #endregion

        #region 新增、编辑
        /// <summary>
        /// 新增、编辑
        /// </summary>
        public enum DetailType
        {
            /// <summary>
            /// 新增
            /// </summary>
            [Description("新增")]
            Add = 1,
            /// <summary>
            /// 编辑
            /// </summary>
            [Description("编辑")]
            Edit = 2
        }
        #endregion

        #region 执行Json状态
        /// <summary>
        /// 执行Json状态
        /// </summary>
        public enum JsonStatus
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success = 0,
            /// <summary>
            /// 未登录
            /// </summary>
            [Description("未登录")]
            UnLogin = 1,
            /// <summary>
            /// 系统错误
            /// </summary>
            [Description("系统错误")]
            Error = 2,
            /// <summary>
            /// 没有权限
            /// </summary>
            [Description("没有权限")]
            NoPermission = 3
        }
        #endregion

        #region 排序类型
        /// <summary>
        /// 排序类型
        /// </summary>
        public enum SortType
        {
            /// <summary>
            /// 未排序
            /// </summary>
            [Description("未排序")]
            All=0,
            /// <summary>
            /// 升序
            /// </summary>
            [Description("升序")]
            ASC=1,
            /// <summary>
            /// 降序
            /// </summary>
            [Description("降序")]
            DESC=2
        }

        #endregion




        #region 根据值获取枚举描述信息
        /// <summary>  
        /// 根据值获取枚举描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类</param>
        /// <param name="iValue">值</param>
        /// <returns></returns>
        public static string GetEnumDescription(Type enumSubitem, int iValue)
        {
            return GetEnumDescription(Enum.Parse(enumSubitem, iValue.ToString()));
        }

        /// <summary>
        /// 根据值获取枚举描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举项</param>
        /// <returns></returns>
        public static string GetEnumDescription(object enumSubitem)
        {
            FieldInfo fieldinfo = enumSubitem.GetType().GetField(enumSubitem.ToString());

            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return enumSubitem.ToString();
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 根据描述信息获取值
        /// <summary>
        /// 根据描述信息获取值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static int GetValByDescription(Type type, string description)
        {
            string _description;
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    _description = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    _description = da.Description;
                }

                if (_description == description)
                {
                    return (int)Enum.Parse(type, field.Name);
                }
            }
            return -1;
        }
        #endregion

        #region 根据枚举类型返回类型中的所有值，文本及描述
        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// 2013-5-6
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<string[]> GetEnumOption(Type type)
        {
            List<string[]> Strs = new List<string[]>();
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                string[] strEnum = new string[3];
                FieldInfo field = fields[i];
                //值列
                strEnum[1] = ((int)Enum.Parse(type, field.Name)).ToString();
                //文本列赋值
                strEnum[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    strEnum[0] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    strEnum[0] = da.Description;
                }

                Strs.Add(strEnum);
            }
            return Strs;
        }
        #endregion
    }

    #region 整套
    public enum RecruitingEnum
    {
        [EnumHelper.EnumDescription("全部")]
        全部 = 0,
        [EnumHelper.EnumDescription("选项A描述")]
        选项A = 1,
        [EnumHelper.EnumDescription("选项B描述")]
        选项B = 2
    }

    /// <summary>
    /// 枚举扩展附加属性
    /// </summary>
    public static class EnumHelper
    {
        #region 枚举扩展附加属性
        /// <summary>
        /// Provides a description for an enumerated type.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
        public sealed class EnumDescriptionAttribute : Attribute
        {
            private string description;
            public string Description
            {
                get { return this.description; }
            }

            /// <summary>
            /// Initializes a new instance of the class.
            /// </summary>
            /// <param name="description"></param>
            public EnumDescriptionAttribute(string description)
                : base()
            {
                this.description = description;
            }
        }
        #endregion

        #region 获取单个枚举的描述信息
        /// <summary>
        /// 获取单个枚举的描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes =
               (EnumDescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
        #endregion
    }
    #endregion 整套
