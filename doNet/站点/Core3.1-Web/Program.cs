using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core31.Web
{
    /// <summary>
    /// 配置主机
    /// 启动流程
    /// </summary>
    public class Program
    {
        //执行顺序：
        //ConfigureWebHostDefaults》ConfigureHostConfiguration》ConfigureAppConfiguration》
        //ConfigureServices》Configure》

        public static void Main(string[] args)
        {
            //创建主机生成器，创建主机，运行主机
            //
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 启动程序、生命周期管理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //创建默认生成器

            //加载主机配置、环境变量、命令行参数
            //配置日志组件、如果用到IIs则会配置iis集成
            Host.CreateDefaultBuilder(args)
           
                //配置web主机、自带默认配置
                //web主机会配置Kestrel   》主机》 Kestrel高性能、
                //加载前缀为"aspnetcore"环境变量
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //可以配置各种参数

                    //主机地址配置(一般配置在json中)
                    //webBuilder.UseUrls("http://*:4999");

                    //日志级别
                    //webBuilder.ConfigureLogging(c => c.SetMinimumLevel(LogLevel.Information));

                    //配置静态文件夹名称
                    //webBuilder.UseWebRoot("文件夹名称");

                    //配置Kestrel
                    webBuilder.UseKestrel(options =>
                    {
                        options.AddServerHeader = true;
                        options.Limits.MaxRequestBodySize = null;//不限制请求大小，也可以在controller、action上配置[DisableRequestSizeLimit]、[RequestSizeLimit(100_000_000)]
                        options.Configure();
                    });
                    //webBuilder.ConfigureKestrel(c => c.Limits.MaxRequestBodySize = 1000 * 1024 * 1024);

                    //制定web应用启动类
                    webBuilder.UseStartup<Startup>();

                    ////绑定地址
                    //webBuilder.UseUrls("http://localhost:5000");
                });
    }
}
