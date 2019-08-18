using PWMIS.Common;
using PWMIS.DataMap.Entity;
using System;

namespace PDF.Net
{
    [Serializable()]
    public partial class SODUserRemark : EntityBase
    {
        public SODUserRemark()
        {
            TableName = "SOD_UserRemark";
            Schema = "dbo";
            EntityMap = EntityMapType.Table;
            //IdentityName = "标识字段名";
            IdentityName = "ID";

            //PrimaryKeys.Add("主键字段名");
            PrimaryKeys.Add("ID");


        }


        protected override void SetFieldNames()
        {
            PropertyNames = new string[] { "ID", "UserId", "Remark" };
        }

        protected override string[] SetFieldDescriptions()
        {
            //字段对应的描述
            return new string[] { "ID", "用户ID", "备注" };
        }



        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID
        {
            get { return getProperty<System.Int32>("ID"); }
            set { setProperty("ID", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UserId
        {
            get { return getProperty<System.Int32>("UserId"); }
            set { setProperty("UserId", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark
        {
            get { return getProperty<System.String>("Remark"); }
            set { setProperty("Remark", value, 100); }
        }


    }
}
