using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcObject.通用
{
    /// <summary>
    /// RabbitMQ生产者
    /// </summary>
    public class RabbitMQProducerHelper
    {
        public static IConnection myConnection = null;

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnection()
        {
            if (myConnection == null)
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "localhost";//RabbitMQ服务在本地运行
                factory.UserName = "admin";//用户名
                factory.Password = "admin";//密码
                factory.VirtualHost = "zy_host";  //默认为"/"

                myConnection = factory.CreateConnection();
            }
            return myConnection;
        }



        public static void 发送到队列(string message)
        {
            var connection = GetConnection();

            using (var channel = connection.CreateModel())
            {
                //声明消息队列
                channel.QueueDeclare("queue_zhangyi", false, false, false, null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "queue_zhangyi", null, body); //开始传递
                Console.WriteLine("已发送： {0}", message);
            }
        }



        public static void Direct交换机(string message)
        {
            var connection = GetConnection();

            string exchange_name = "exchange_Direct";
            string routingkey = "key";

            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange_name, routingkey, null, body); //开始传递

                Console.WriteLine("已发送： {0}", message);
            }
        }

        public static void Topic交换机(string message)
        {
            var connection = GetConnection();

            string exchange_name = "exchange_Topic";
            string routingkey = "key";

            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange_name, routingkey, null, body); //开始传递
                channel.BasicPublish(exchange_name, "key.1", null, body); //开始传递
                channel.BasicPublish(exchange_name, "key.2.2", null, body); //开始传递
                channel.BasicPublish(exchange_name, "key.33", null, body); //开始传递
                channel.BasicPublish(exchange_name, "1key1.33", null, body); //开始传递

                Console.WriteLine("已发送： {0}", message);
            }
        }

        public static void Fanout交换机(string message)
        {
            var connection = GetConnection();

            string exchange_name = "exchange_Fanout";

            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange_name, "可以乱写", null, body); //开始传递
                channel.BasicPublish(exchange_name, "b", null, body); //开始传递
                channel.BasicPublish(exchange_name, "c", null, body); //开始传递
                channel.BasicPublish(exchange_name, "d", null, body); //开始传递
                channel.BasicPublish(exchange_name, "e", null, body); //开始传递

                Console.WriteLine("已发送： {0}", message);
            }
        }

        public static void 确认消息是否到达消息队列中1(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var connection = GetConnection();

            using (var channel = connection.CreateModel())
            {
                channel.BasicReturn += Channel_BasicReturn;
                channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: null, body: body);
            }
        }


        public static void 确认消息是否到达消息队列中_普通Confirm模式(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var connection = GetConnection();

            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();

                channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: null, body: body);

                if (channel.WaitForConfirms())
                {
                    Console.WriteLine("普通发送方确认模式");
                }
            }
        }
        public static void 事务(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var connection = GetConnection();

            using (var channel = connection.CreateModel())
            {
                try
                {
                    //声明事物
                    channel.TxSelect();

                    channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: null, body: body);
                    channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: null, body: body);

                    //提交事物
                    channel.TxCommit();
                }
                catch (Exception)
                {
                    //回滚
                    channel.TxRollback();
                    //1.autoAck = false的时候是支持事务的，也就是说即使你已经手动确认了消息已经收到了，但在确认消息会等事务的返回解决之后，
                    //    在做决定是确认消息还是重新放回队列，如果你手动确认现在之后这儿回滚了事务，此条消息会重新放回队列；
                    //2.autoAck = true的情况是不支持事务的，也就是说你即使在收到消息之后在回滚事务也是于事无补的，队列已经把消息移除了；

                }
            }
        }

        public static void 消息持久化(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var connection = GetConnection();

            using (var channel = connection.CreateModel())
            {
                BasicProperties pro = new BasicProperties();
                pro.DeliveryMode = 2;
                pro.Priority = 1;

                channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: pro, body: body);

            }
        }

        public static void 简单测试 (string message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.Port = 5672;
            factory.UserName = "admin";
            factory.Password = "admin";
            factory.VirtualHost = "TestVHost";
            //创建connetion
            using (var connetion = factory.CreateConnection())
            {
                connetion.CallbackException += Connetion_CallbackException;
                connetion.RecoverySucceeded += Connetion_RecoverySucceeded;
                connetion.ConnectionRecoveryError += Connetion_ConnectionRecoveryError;
                connetion.ConnectionBlocked += Connetion_ConnectionBlocked;
                connetion.ConnectionUnblocked += Connetion_ConnectionUnblocked;
                //连接关闭的时候
                connetion.ConnectionShutdown += Connetion_ConnectionShutdown;

                //创建channel
                using (var channel = connetion.CreateModel())
                {

                    //消息会在何时被 confirm？
                    //The broker will confirm messages once:
                    //broker 将在下面的情况中对消息进行 confirm ：
                    //it decides a message will not be routed to queues
                    //(if the mandatory flag is set then the basic.return is sent first) or
                    //broker 发现当前消息无法被路由到指定的 queues 中（如果设置了 mandatory 属性，则 broker 会先发送 basic.return）
                    //a transient message has reached all its queues(and mirrors) or
                    //非持久属性的消息到达了其所应该到达的所有 queue 中（和镜像 queue 中）
                    //a persistent message has reached all its queues(and mirrors) and been persisted to disk(and fsynced) or
                    //持久消息到达了其所应该到达的所有 queue 中（和镜像 queue 中），并被持久化到了磁盘（被 fsync）
                    //a persistent message has been consumed(and if necessary acknowledged) from all its queues
                    //持久消息从其所在的所有 queue 中被 consume 了（如果必要则会被 acknowledge）


                    //broker 发现当前消息无法被路由到指定的 queues 中（如果设置了 mandatory 属性，则 broker 会先发送 basic.return）
                    channel.BasicReturn += Channel_BasicReturn;

                    //(可以不声明)如果不声明交换机 ，那么就使用默认的交换机  （每一个vhost都会有一个默认交换机）
                    //channel.ExchangeDeclare("amq.direct", ExchangeType.Direct,true);

                    //创建一个队列  bool durable(持久化), bool exclusive（专有的）, bool autoDelete（自动删除）
                    //channel.QueueDeclare("queue_zhangyi", true, false, false, null);
                    //不做绑定的话，使用默认的交换机。
                    //channel.QueueBind("TestQueue", "amq.direct", "queue_zhangyi", null);

                    //发布消息
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("amq.direct", routingKey: "queue_zhangyi", mandatory: true, basicProperties: null, body: body);
                }

                Console.WriteLine("执行成果!");
            }
        }




        /// <summary>
        /// 发现当前消息无法被路由到指定的 queues 中（如果设置了 mandatory 属性，则 broker 会先发送 Channel_BasicReturn）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Channel_BasicReturn(object sender, RabbitMQ.Client.Events.BasicReturnEventArgs e)
        {
            Console.WriteLine("Channel_BasicReturn");
        }

        private static void Connetion_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Connetion_ConnectionShutdown");
        }

        private static void Connetion_ConnectionUnblocked(object sender, EventArgs e)
        {
            Console.WriteLine("Connetion_ConnectionUnblocked");
        }

        private static void Connetion_ConnectionBlocked(object sender, RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            Console.WriteLine("Connetion_ConnectionBlocked");
        }

        private static void Connetion_ConnectionRecoveryError(object sender, RabbitMQ.Client.Events.ConnectionRecoveryErrorEventArgs e)
        {
            Console.WriteLine("Connetion_ConnectionRecoveryError");
        }

        private static void Connetion_RecoverySucceeded(object sender, EventArgs e)
        {
            Console.WriteLine("Connetion_RecoverySucceeded");
        }

        private static void Connetion_CallbackException(object sender, RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            Console.WriteLine("Connetion_CallbackException");
        }



    }


    /// <summary>
    /// 远程过程调用RPC
    /// </summary>
    public class 远程过程调用RPC
    {

        public void 客户端()
        {
            var connection = RabbitMQProducerHelper.GetConnection();
            var channel = connection.CreateModel();

            //申明唯一guid用来标识此次发送的远程调用请求
            var correlationId = Guid.NewGuid().ToString();
            //申明需要监听的回调队列
            var replyQueue = channel.QueueDeclare().QueueName;
            var properties = channel.CreateBasicProperties();
            properties.ReplyTo = replyQueue;//指定回调队列
            properties.CorrelationId = correlationId;//指定消息唯一标识

            var body = Encoding.UTF8.GetBytes("123");
            //发布消息
            channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: properties, body: body);
            Console.WriteLine($"[*] Request fib()");

            //创建消费者用于处理消息回调（远程调用返回结果）
            var callbackConsumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: replyQueue, autoAck: true, consumer: callbackConsumer);
            callbackConsumer.Received += (model, ea) =>
            {
                    //仅当消息回调的ID与发送的ID一致时，说明远程调用结果正确返回。
                    if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var responseMsg = $"Get Response: {Encoding.UTF8.GetString(ea.Body)}";
                    Console.WriteLine($"[x]: {responseMsg}");
                }
            };
        }

        public void 服务端()
        {
            var connection = RabbitMQProducerHelper.GetConnection();
            var channel = connection.CreateModel();
                //申明队列接收远程调用请求
                channel.QueueDeclare(queue: "rpc_queue", durable: false,exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                Console.WriteLine("[*] Waiting for message.");

                //请求处理逻辑
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    int n = int.Parse(message);
                    Console.WriteLine($"Receive request of Fib({n})");
                    int result = n;
                    //从请求的参数中获取请求的唯一标识，在消息回传时同样绑定
                    var properties = ea.BasicProperties;
                    var replyProerties = channel.CreateBasicProperties();
                    replyProerties.CorrelationId = properties.CorrelationId;
                    //将远程调用结果发送到客户端监听的队列上
                    channel.BasicPublish(exchange: "", routingKey: properties.ReplyTo, basicProperties: replyProerties, body: Encoding.UTF8.GetBytes(result.ToString()));
                    //手动发回消息确认
                    channel.BasicAck(ea.DeliveryTag, false);
                    Console.WriteLine($"Return result: Fib({n})= {result}");
                };
                //启动消费者
                channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);
            }
    }
}