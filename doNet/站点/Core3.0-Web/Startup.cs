using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core3._0_Web.轮询任务;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core3._0_Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// 注册服务
        /// 以依赖注入的方式将服务添加到服务容器，IoC容器
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //中文乱码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //获取json参数配置
            GetConfig();

            services.AddControllersWithViews();

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

        RequestDelegate del = async (context) =>
        {
            await context.Response.WriteAsync("dfdfdfdf");
        };

        /// <summary>
        /// 配置中间件，中间件组成管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //测试中间件(没有next的话，会终段后面的中间件)
            app.Run( async (context)=> {
                await context.Response.WriteAsync("aaa");
            });
            app.Run(del);

            app.Use(async (context,next)=> { 
                await context.Response.WriteAsync("bbb");
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
                //开发人员，异常页面中间件
                app.UseExceptionHandler("/Home/Error");
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

                endpoints.MapGet("/home/index",async c=> {
                    await c.Response.WriteAsync("哈哈，这是home/index");
                });
            });
        }

        #region 获取json参数配置
        /// <summary>
        /// 获取json参数配置
        /// </summary>
        public void GetConfig()
        {

            //直接读取字符串
            string conn = Configuration.GetConnectionString("default");
            conn = Configuration["connection"];

            //读取GetSection
            IConfigurationSection configurationSection = Configuration.GetSection("connection");


            //获取运行路径各种方法
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var path = Environment.CurrentDirectory;
            var path2 = Directory.GetCurrentDirectory();
            var path3 = AppDomain.CurrentDomain.BaseDirectory;


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
