﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class Comment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 文章id
        /// </summary>
        public int content_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; } = DateTime.Now;
    }
}
