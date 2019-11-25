using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace MvcObject.通用
{
    /// <summary>
    /// RabbitMQ消费者
    /// </summary>
    public class RabbitMQConsumerHelper
    {
        public static IConnection myConnection = null;

        public static IConnection GetConnection()
        {
            if (myConnection == null)
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "localhost";//RabbitMQ服务在本地运行
                factory.UserName = "admin";//用户名
                factory.Password = "admin";//密码

                myConnection = factory.CreateConnection();
            }
            return myConnection;
        }


        public static void 接收队列消息()
        {
            var connection = GetConnection();

            var channel = connection.CreateModel();
            //创建一个名称为queue_zhangyi的消息队列
            channel.QueueDeclare("queue_zhangyi", false, false, false, null);

            //构造消费者实例
            var consumer = new EventingBasicConsumer(channel);
            //绑定消息接收后的事件委托
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine(" 接收到消息： {0}", message);
                Thread.Sleep(1000);//模拟耗时
                Console.WriteLine(" 处理完毕 ");

                //autoAck:fasle的时候，需要手动消息确认。
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            //启动消费者
            channel.BasicConsume(queue: "queue_zhangyi", autoAck: false, consumer: consumer);
        }

        public static void 接收交换机消息()
        {
            var connection = GetConnection();

            var channel = connection.CreateModel();
            //创建一个名称为queue_zhangyi的消息队列
            channel.QueueDeclare("queue_2", false, false, false, null);
            channel.QueueBind("queue_2", "exchange_zhangyi", "", null);//绑定交换机

            //构造消费者实例
            var consumer = new EventingBasicConsumer(channel);
            //绑定消息接收后的事件委托
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine(" 接收到消息： {0}", message);
                Thread.Sleep(1000);//模拟耗时
                Console.WriteLine(" 处理完毕 ");

                //autoAck:fasle的时候，需要手动消息确认。
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            //启动消费者
            channel.BasicConsume(queue: "queue_2", autoAck: false, consumer: consumer);
        }


        public static void 消费负载均衡()
        {
            var connection = GetConnection();

            var channel = connection.CreateModel();
            
                //构造消费者实例
                var consumer = new EventingBasicConsumer(channel);
                //绑定消息接收后的事件委托
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    Console.WriteLine(" 接收到消息： {0}", message);
                    Thread.Sleep(1000);//模拟耗时
                    Console.WriteLine(" 处理完毕 ");

                    //autoAck:fasle的时候，需要手动消息确认。
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                //设置prefetchCount:1来告知RabbitMQ，在未收到消费端的消息确认时，不再分发消息，也就确保了当消费端处于忙碌状态时，不再分配任务。
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                //启动消费者
                channel.BasicConsume(queue: "queue_zhangyi", autoAck: false, consumer: consumer);
        }

    }
}