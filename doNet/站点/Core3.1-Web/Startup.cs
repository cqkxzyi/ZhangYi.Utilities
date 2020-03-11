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
        /// ע�����
        /// ������ע��ķ�ʽ��������ӵ�����������IoC����
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //������������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //������ַ����
            services.AddSingleton(serviceProvider =>
            {
                var server = serviceProvider.GetRequiredService<IServer>();
                var temp = server.Features.Get<IServerAddressesFeature>();
                return temp;
            });
            //��ȡ�ͻ���IP��Ϣ
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //AddAutoMapperע��
            services.AddAutoMapper(typeof(AutoMapperConfig)); //services.AddAutoMapper(Assembly.Load("��������dll����"));

            //��ȡjson��������
            GetConfig();


            services.AddControllersWithViews(option =>
            {
                //���ȫ���쳣����
                option.Filters.Add(new GlobalExceptionFilter());
            });

            //����
            services.AddCors();

            //ע����ѯ����
            services.AddSingleton<IHostedService, BackManagerService>(factory =>
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

        /// <summary>
        /// �����м�����м����ɹܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => {
                //context.Response.ContentType = "text/plain; charset=utf-8";
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                //await context.Response.WriteAsync("��һ���м��ִ�����");
                await next();
            });

            //��ȡ·��
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
                ////������Ա���쳣ҳ���м��
                //app.UseExceptionHandler("/Home/Error");

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

                //·�ɹ���
                //endpoints.MapGet("/home/index",async c=> {
                //    await c.Response.WriteAsync("����������home/index");
                //});
            });


            //�ս�㣬�ڹܵ�β������һ���м����֮����м������ִ��
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("֮����м������ִ��");
            });

            //��ʽ��
            RequestDelegate del = async (context) =>
            {
                await context.Response.WriteAsync("del�м���������");
            };

            app.Run(del);

            //Map()��MapWhen()�ܵ������ӷ�֧������ƥ����߷�֧���Ҳ��л�������֧
        }


        #region ��ȡjson��������
        /// <summary>
        /// ��ȡjson��������
        /// </summary>
        public void GetConfig()
        {
            //ֱ�Ӷ�ȡ�ַ���
            string conn = Configuration.GetConnectionString("a");
            conn = Configuration["connection"];

            //��ȡGetSection
            IConfigurationSection configurationSection = Configuration.GetSection("connection");


            //��ȡ����·�����ַ���
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);//����ʱ��Ŀ¼

            var path3 = AppDomain.CurrentDomain.BaseDirectory;//����ʱ��Ŀ¼
            var path4 = AppContext.BaseDirectory;//����ʱ��Ŀ¼
            var path = Environment.CurrentDirectory;
            var path2 = Directory.GetCurrentDirectory();

            //ϵͳ���System.Runtime.InteropServices���ƿռ��¡������������Runtime����Environment
            string name = RuntimeInformation.ProcessArchitecture.ToString();
            name = Assembly.GetExecutingAssembly().GetName().Name;//��ȡ��������



            //��� json �ļ�·��
            //Directory.GetCurrentDirectory()��ȡ����ִ��dotnet��������Ŀ¼
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(currentDirectory).AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();


            //�����ͷ�ʽ��ȡ
            var id = configurationRoot["model:id"];
            var val1 = configurationRoot.GetSection("name");
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"name: {val1.Value}");
            var val2 = configurationRoot.GetSection("model").GetSection("name");
            Console.WriteLine($"name: {val2.Value}");

            //ǿ���ͷ�ʽ��ȡ
            var info = configurationRoot.GetValue<int>("model:id");


            //�ڴ��ֶ�ģʽ�������
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
