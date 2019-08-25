using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2._2_Web.Models
{
    /// <summary>
    /// 文章、评论 关系表
    /// </summary>
    public class ContentWithComment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 状态 1正常 0删除
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime add_time { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modify_time { get; set; }
        /// <summary>
        /// 文章评论list
        /// </summary>
        public IEnumerable<Comment> comments { get; set; }
    }
}
