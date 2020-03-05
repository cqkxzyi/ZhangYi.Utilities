using MvcObject.通用;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcObject.Controllers
{
    /// <summary>
    /// RabbitMQ测试
    /// ExchangeType有direct、Fanout和Topic三种，不同类型的Exchange路由的行为是不一样的
    /// http://localhost:15672
    /// </summary>
    public class TestRabbitMQController : Controller
    {
        /// <summary>
        /// 生产者测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //RabbitMQProducerHelper.发送到队列("发送到队列");
            RabbitMQProducerHelper.发送到交换机("发送到交换机");

            return View();
        }


        /// <summary>
        /// 消费者测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2(int i=1)
        {
            //RabbitMQConsumerHelper.接收队列消息();
            
            //RabbitMQConsumerHelper.消费负载均衡();

            if(i==1)
                RabbitMQConsumerHelper.接收交换机消息1();
            else
                RabbitMQConsumerHelper.接收交换机消息2();

            return View();
        }


    }
}