using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHibernate测试.Models
{
    public abstract class Entity
    {
        virtual public int ID { get; set; }
    }


    /// <summary>
    /// 单表
    /// </summary>
    public class MsgLog
    {
        public virtual int ID { get; set; }
        public virtual string fromUser { get; set; }
        public virtual string toUser { get; set; }

        public virtual int InfoID { get; set; }
        public virtual string msg { get; set; }
        public virtual DateTime CreteTime { get; set; }
    }


    /// <summary>
    /// 用户表
    /// </summary>
    public class User : Entity
    {
        public virtual string Name { get; set; }
        public virtual string No { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }

    /// <summary>
    /// 用户详情表
    /// </summary>
    public class UserDetails : Entity
    {
        public virtual User User { get; set; }
        public virtual int Sex { get; set; }
        public virtual int Age { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual decimal Height { get; set; }
    }

    /// <summary>
    /// 项目表
    /// </summary>
    public class Project : Entity
    {
        public Project()
        {
            Tasks = new List<Task>();
            Products = new List<Product>();
        }

        public virtual string Name { get; set; }
        public virtual User User { get; set; }

        public virtual IList<Task> Tasks { get; protected set; }
        public virtual IList<Product> Products { get; set; }

    }

    /// <summary>
    /// 产品表
    /// </summary>
    public class Product : Entity
    {
        public Product()
        {
            Projects = new List<Project>();
        }

        public virtual IList<Project> Projects { get; set; }
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
    }

    /// <summary>
    /// 任务表
    /// </summary>
    public class Task : Entity
    {
        public virtual Project Project { get; set; }
        public virtual string Name { get; set; }
    }
}