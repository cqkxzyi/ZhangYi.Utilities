using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 结构体
    /// </summary>
    public static class MyStruct
    {
        /// <summary>
        /// 功能描述：ZTreeJson 结构体
        /// 创建日期：2013-05-08 17:30
        /// </summary>
        public struct ZTreeJson
        {
            public ZTreeJson(Int64 id, Int64 pid, string name, bool open, bool check, bool isParent)
            {
                this.Id = id;
                this.Pid = pid;
                this.Name = name;
                this.Open = open;
                this.Checked = check;
                this.IsParent = isParent;
            }

            public Int64 Id;
            public Int64 Pid;
            public string Name;
            public bool Open;
            public bool Checked;
            public bool IsParent;
        }

        /// <summary>
        /// 内容结构体
        /// </summary>
        public struct MmsContent
        {
            public int index;
            public long msgId;
            public string title;
            public string showTime;
            public string mmsText;
            public decimal mmsTextLength;
            public string imgUrl;
            public decimal imgLength;
            public string audioUrl;
            public decimal audioLength;
            public string viodUrl;
            public decimal viodLength;
        }
    }
