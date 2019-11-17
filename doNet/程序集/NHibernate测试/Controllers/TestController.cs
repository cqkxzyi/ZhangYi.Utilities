
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate测试.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            NHibernateHelper _NHibernateHelper = new NHibernateHelper();
            ISession _ISession = _NHibernateHelper.GetSession();

            MsgLog msgLog = new MsgLog { fromUser = "李dd", toUser = "永京dd", InfoID = 100, CreteTime = DateTime.Now };
            NHibernateSample _sample = new NHibernateSample(_ISession);
            _sample.CreateMsgLog(msgLog);

            MsgLog customer = _sample.GetMsgLogById(137);
            int customerId = customer.ID;

            return View();
        }

        /// <summary>
        /// NHibernate查询语言
        /// </summary>
        /// <returns></returns>
        public ActionResult HQL()
        {
            NHibernateHelper _NHibernateHelper = new NHibernateHelper();
            ISession _ISession = _NHibernateHelper.GetSession();

            IList<MsgLog> list = _ISession.CreateQuery("from MsgLog").List<MsgLog>();
            IList<int> list2 = _ISession.CreateQuery("select id from MsgLog order by cretetime").List<int>();
            IList<object[]> list3 = _ISession.CreateQuery("select toUser,count(id) from MsgLog group by toUser").List<object[]>();

            //查询语句(四种方式，越来越好)
            list = _ISession.CreateQuery("from MsgLog where ID>100").List<MsgLog>();
            list = _ISession.CreateQuery("from MsgLog where ID>?").SetInt32(0,100).List<MsgLog>();
            list = _ISession.CreateQuery("from MsgLog where ID>:ID").SetInt32("ID", 100).List<MsgLog>();



            return View();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryByCriteria()
        {
            NHibernateHelper _NHibernateHelper = new NHibernateHelper();
            ISession _ISession = _NHibernateHelper.GetSession();

            //查询
            ICriteria crit = _ISession.CreateCriteria(typeof(MsgLog));
            crit.SetMaxResults(50);
            IList<MsgLog> list = crit.List<MsgLog>();


            //限制条件查询
            crit = _ISession.CreateCriteria(typeof(MsgLog));
            crit.Add(Restrictions.Like("Firstname", "a%"))
                .Add(Restrictions.Gt("ID",100))
                .List<MsgLog>();

            return View();
        }

        public ActionResult QueryByExample()
        {
            NHibernateHelper _NHibernateHelper = new NHibernateHelper();
            ISession _ISession = _NHibernateHelper.GetSession();

            MsgLog msg = new MsgLog() {ID=155 };
            IList<MsgLog> list = _ISession.CreateCriteria(typeof(MsgLog))
                .Add(Example.Create(msg))
                .List<MsgLog>();


            return View();
        }



        /// <summary>
        /// NHibernate帮助类
        /// </summary>
        public class NHibernateHelper
        {
            private ISessionFactory _sessionFactory;
            public NHibernateHelper()
            {
                Configuration config = new Configuration();
                _sessionFactory = config.Configure().BuildSessionFactory();
            }

            public ISession GetSession()
            {
                return _sessionFactory.OpenSession();
            }
        }



        /// <summary>
        /// 业务层
        /// </summary>
        public class NHibernateSample
        {
            protected ISession Session { get; set; }
            public NHibernateSample(ISession session)
            {
                Session = session;
            }

            public void CreateMsgLog(MsgLog msglog)
            {
                Session.Save(msglog);
                Session.Flush();
            }
            /// <summary>
            /// 根据ID得到实体
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public MsgLog GetMsgLogById(int id)
            {
                return Session.Get<MsgLog>(id);
            }
        }


 




    }
}
