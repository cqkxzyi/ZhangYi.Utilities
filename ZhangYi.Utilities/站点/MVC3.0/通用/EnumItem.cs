using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 枚举项实体
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 项名称
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 项描述
        /// </summary>
        public string Description { get; set; }


        public class _
        {
            ///<summary>项编码</summary>
            public const String Code = "Code";

            ///<summary>项描述</summary>
            public const String Description = "Description";

            ///<summary>项值</summary>
            public const String Value = "Value";
        }
    }





    /// <summary>
    /// ComboBox选项实体项
    /// </summary>
    public class ComboBoxListItem
    {
        private string _value = string.Empty;
        public string Value
        {
            get { return _value; }
            set { Value = value; }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public ComboBoxListItem(string text, string value)
        {
            this._text = text;
            this._value = value;
        }

        public override string ToString()
        {
            return this._text;
        }
    }
