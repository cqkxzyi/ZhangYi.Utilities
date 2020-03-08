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
        Random a = new Random();


        /// <summary>
        /// 生产者测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int i)
        {
            ;
            switch (i)
            {
                case 1:
                    RabbitMQProducerHelper.发送到队列("发送到队列"+ a.Next(1, 10000)); break;
                case 2:
                    RabbitMQProducerHelper.Direct交换机("Direct交换机"+ a.Next(1, 10000)); break;
                case 3:
                    RabbitMQProducerHelper.Topic交换机("Topic交换机" + a.Next(1, 10000)); break;
                case 4:
                    RabbitMQProducerHelper.Fanout交换机("Fanout交换机" + a.Next(1, 10000)); break;
            }
            return View();
        }


        /// <summary>
        /// 消费者测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2(int i=1)
        {
            switch (i)
            {
                case 1:
                    RabbitMQConsumerHelper.接收队列消息();
                    break;
                case 2:
                    RabbitMQConsumerHelper.接收Direct交换机();
                    break;
                case 3:
                    RabbitMQConsumerHelper.接收Topic交换机();
                    break;
                case 4:
                    RabbitMQConsumerHelper.接收Fanout交换机();
                    break;
                case 5:
                    RabbitMQConsumerHelper.消费负载均衡();
                    break;
                case 6:
                    RabbitMQConsumerHelper.接收交换机消息2();
                    break;

            }

            return View();
        }


    }
}