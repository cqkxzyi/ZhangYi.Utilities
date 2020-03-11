﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Web.Common
{
    /// <summary>
    /// 回调内容定义
    /// </summary>
    public class BackHandler
    {
        /// <summary>
        ///  0=Info，1=Debug，2=Error
        /// </summary>
        public int Level { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public object State { get; set; }
    }
}
