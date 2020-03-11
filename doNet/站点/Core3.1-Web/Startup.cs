using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using  Core31.Web.Common;
using Core31.Web.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Core.Common;

namespace Core31.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>g
        /// 注册服务
        /// 以依赖注入的方式将服务添加到服务容器，IoC容器
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //中文乱码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //本机地址服务
            services.AddSingleton(serviceProvider =>
            {
                var server = serviceProvider.GetRequiredService<IServer>();
                var temp = server.Features.Get<IServerAddressesFeature>();
                return temp;
            });
            //获取客户端IP信息
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //AddAutoMapper注册
            services.AddAutoMapper(typeof(AutoMapperConfig)); //services.AddAutoMapper(Assembly.Load("其他程序集dll名称"));

            //获取json参数配置
            GetConfig();


            services.AddControllersWithViews(option =>
            {
                //添加全局异常控制
                option.Filters.Add(new GlobalExceptionFilter());
            });

            //跨域
            services.AddCors();

            //注册轮询任务
            services.AddSingleton<IHostedService, BackManagerService>(factory =>
            {
                OrderManagerService order = new OrderManagerService();
                return new BackManagerService(options =>
                {
                    options.Name = "订单超时检查";
                    options.CheckTime = 5 * 1000;
                    options.Callback = order.CheckOrder;
                    options.Handler = order.OnBackHandler;
                });
            });

        }

        /// <summary>
        /// 配置中间件，中间件组成管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => {
                //context.Response.ContentType = "text/plain; charset=utf-8";
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                //await context.Response.WriteAsync("第一个中间件执行完毕");
                await next();
            });

            //获取路劲
            string webRootPath = env.WebRootPath;
            string contentRootPath = env.ContentRootPath;


            //跨域
            app.UseCors();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ////开发人员，异常页面中间件
                //app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            //静态文件中间件
            app.UseStaticFiles();

            //路由中间件，解析路由信息
            app.UseRouting();

            //授权
            app.UseAuthorization();

            //终结点中间件，配置路由和终结点之间的关系
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //路由过滤
                //endpoints.MapGet("/home/index",async c=> {
                //    await c.Response.WriteAsync("哈哈，这是home/index");
                //});
            });


            //终结点，在管道尾端增加一个中间件，之后的中间件不再执行
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("之后的中间件不再执行");
            });

            //方式二
            RequestDelegate del = async (context) =>
            {
                await context.Response.WriteAsync("del中间件输出内容");
            };

            app.Run(del);

            //Map()、MapWhen()管道中增加分支，条件匹配就走分支，且不切换回主分支
        }


        #region 获取json参数配置
        /// <summary>
        /// 获取json参数配置
        /// </summary>
        public void GetConfig()
        {
            //直接读取字符串
            string conn = Configuration.GetConnectionString("a");
            conn = Configuration["connection"];

            //读取GetSection
            IConfigurationSection configurationSection = Configuration.GetSection("connection");


            //获取运行路径各种方法
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);//运行时根目录

            var path3 = AppDomain.CurrentDomain.BaseDirectory;//运行时根目录
            var path4 = AppContext.BaseDirectory;//运行时根目录
            var path = Environment.CurrentDirectory;
            var path2 = Directory.GetCurrentDirectory();

            //系统相关System.Runtime.InteropServices名称空间下。相关类名都带Runtime或者Environment
            string name = RuntimeInformation.ProcessArchitecture.ToString();
            name = Assembly.GetExecutingAssembly().GetName().Name;//获取程序集名称



            //添加 json 文件路径
            //Directory.GetCurrentDirectory()获取的是执行dotnet命令所在目录
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(currentDirectory).AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();


            //弱类型方式读取
            var id = configurationRoot["model:id"];
            var val1 = configurationRoot.GetSection("name");
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"name: {val1.Value}");
            var val2 = configurationRoot.GetSection("model").GetSection("name");
            Console.WriteLine($"name: {val2.Value}");

            //强类型方式读取
            var info = configurationRoot.GetValue<int>("model:id");


            //内存字段模式添加配置
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "key1","value1" },
                { "key2","value2" },
                { "section1:key4","value4" },
            });



        }
        #endregion
    }
}
