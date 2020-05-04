using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoNet基础.多线程_异步_并行
{
    public class 安全队列
    {
        public static void 测试入口()
        {
            Task t = RunProgram();
            t.Wait();
            Console.ReadKey();
        }


        static async Task RunProgram()
        {
            var taskQueue = new ConcurrentQueue<CustomTask>();
            var cts = new CancellationTokenSource();
            //生成任务添加至并发队列
            var taskSource = Task.Run(() => TaskIn(taskQueue));
            //同时启动四个任务处理队列中的任务
            Task[] processors = new Task[4];
            for (int i = 1; i <= 4; i++)
            {
                string processId = i.ToString();
                processors[i - 1] = Task.Run(
                                             () => TaskOut(taskQueue, "Processor " + processId, cts.Token)
                                    );
            }
            await taskSource;
            //向任务发送取消信号
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            await Task.WhenAll(processors);
        }

        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        static async Task TaskIn(ConcurrentQueue<CustomTask> queue)
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(50);
                var workItem = new CustomTask { Id = i };
                //入列
                queue.Enqueue(workItem);
                Console.WriteLine("task {0} has been posted", workItem.Id);
            }
        }

        /// <summary>
        /// 出列
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        static async Task TaskOut(ConcurrentQueue<CustomTask> queue, string name, CancellationToken token)
        {
            CustomTask workItem;
            bool dequeueSuccesful = false;
            await GetRandomDelay();
            do
            {
                //出列
                dequeueSuccesful = queue.TryDequeue(out workItem);
                if (dequeueSuccesful)
                {
                    Console.WriteLine("task {0} has been processed by {1}", workItem.Id, name);
                }
                await GetRandomDelay();
            }
            while (!token.IsCancellationRequested);
        }

        static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1500);
            return Task.Delay(delay);
        }

    }

    class CustomTask
    {
        public int Id { get; set; }
    }


}
