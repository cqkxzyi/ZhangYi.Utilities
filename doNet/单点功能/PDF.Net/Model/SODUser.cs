using PWMIS.Common;
using PWMIS.DataMap.Entity;
using System;

namespace PDF.Net
{
    [Serializable()]
    public partial class SODUser : EntityBase
    {
        public SODUser()
        {
            TableName = "SOD_User";
            Schema = "dbo";
            EntityMap = EntityMapType.Table;
            //IdentityName = "标识字段名";
            IdentityName = "id";

            //PrimaryKeys.Add("主键字段名");
            PrimaryKeys.Add("id");


        }


        protected override void SetFieldNames()
        {
            PropertyNames = new string[] { "ID", "Emal", "LogName", "UserName", "LogPwd", "Mobile", "Sex", "Age", "Head", "Birthday" };
        }

        protected override string[] SetFieldDescriptions()
        {
            //字段对应的描述
            return new string[] { "ID", "Emal", "登录名", "姓名", "密码", "手机号码", "性别", "", "", "" };
        }



        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Int32 ID
        {
            get { return getProperty<System.Int32>("ID"); }
            set { setProperty("ID", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String Emal
        {
            get { return getProperty<System.String>("Emal"); }
            set { setProperty("Emal", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String LogName
        {
            get { return getProperty<System.String>("LogName"); }
            set { setProperty("LogName", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String UserName
        {
            get { return getProperty<System.String>("UserName"); }
            set { setProperty("UserName", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String LogPwd
        {
            get { return getProperty<System.String>("LogPwd"); }
            set { setProperty("LogPwd", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String Mobile
        {
            get { return getProperty<System.String>("Mobile"); }
            set { setProperty("Mobile", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean Sex
        {
            get { return getProperty<System.Boolean>("Sex"); }
            set { setProperty("Sex", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Age
        {
            get { return getProperty<System.Int32>("Age"); }
            set { setProperty("Age", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String Head
        {
            get { return getProperty<System.String>("Head"); }
            set { setProperty("Head", value, 50); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime Birthday
        {
            get { return getProperty<System.DateTime>("Birthday"); }
            set { setProperty("Birthday", value); }
        }


    }
}

