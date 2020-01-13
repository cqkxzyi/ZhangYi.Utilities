using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core3._0_Web.��ѯ����;
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
        /// ע�����
        /// ������ע��ķ�ʽ��������ӵ�����������IoC����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //������������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //��ȡjson��������
            GetConfig();

            services.AddControllersWithViews();

            //����
            services.AddCors();

            //ע����ѯ����
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BackManagerService>(factory =>
            {
                OrderManagerService order = new OrderManagerService();
                return new BackManagerService(options =>
                {
                    options.Name = "������ʱ���";
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
        /// �����м�����м����ɹܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�����м��
            app.Run( async (context)=> {
                await context.Response.WriteAsync("aaa");
            });
            app.Run(del);

            app.Use(async (context,next)=> { 
                await context.Response.WriteAsync("bbb");
                await next();
            });


            string webRootPath = env.WebRootPath;
            string contentRootPath = env.ContentRootPath;

            //����
            app.UseCors();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //������Ա���쳣ҳ���м��
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            //��̬�ļ��м��
            app.UseStaticFiles();


            //·���м��������·����Ϣ
            app.UseRouting();

            //��Ȩ
            app.UseAuthorization();

            //�ս���м��������·�ɺ��ս��֮��Ĺ�ϵ
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapGet("/home/index",async c=> {
                    await c.Response.WriteAsync("����������home/index");
                });
            });
        }

        #region ��ȡjson��������
        /// <summary>
        /// ��ȡjson��������
        /// </summary>
        public void GetConfig()
        {

            //ֱ�Ӷ�ȡ�ַ���
            var conn = Configuration.GetConnectionString("default");
            conn = Configuration["connection"];
            //Configuration.Bind();


            //��ȡ����·��
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            var path = Environment.CurrentDirectory;


            //��� json �ļ�·��
            //Directory.GetCurrentDirectory()��ȡ����ִ��dotnet��������Ŀ¼
            var configurationRoot = new ConfigurationBuilder().SetBasePath(currentDirectory).AddJsonFile("appsettings.json").Build();


            //�����ͷ�ʽ��ȡ
            var id = configurationRoot["model:id"];
            var val1 = configurationRoot.GetSection("name");
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"name: {val1.Value}");
            var val2 = configurationRoot.GetSection("model").GetSection("name");
            Console.WriteLine($"name: {val2.Value}");

            //ǿ���ͷ�ʽ��ȡ
            var info = configurationRoot.GetValue<int>("model:id");
        }
        #endregion

        
    }
}
