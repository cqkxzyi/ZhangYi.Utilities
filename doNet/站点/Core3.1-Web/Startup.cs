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
using Microsoft.Extensions.FileProviders;

namespace Core31.Web
{
    public class Startup
    {
        public readonly IConfiguration _configuration;
        private readonly IConfigurationRoot _appConfiguration;

        public Startup( IConfiguration configuration)
        {
            //ϵͳĬ�϶�ȡ�������ã������ֶ����¶�ȡ
           // _configuration = configuration;

            string configType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{configType ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

            //��� json �ļ�·��
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"appsettings.{configType ?? "Production"}.json", optional: true);
            _appConfiguration = configurationBuilder.AddEnvironmentVariables().Build();

        }


        /// <summary>g
        /// ע�����
        /// ������ע��ķ�ʽ��������ӵ�����������IoC����
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //������������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            //��ȡjson��������
            GetConfig();

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

            services.AddControllersWithViews(option =>
            {
                //���ȫ���쳣����
                option.Filters.Add(new GlobalExceptionFilter());
            });


            //����
            services.AddCors();

            //ע����ѯ����
            //services.AddSingleton<IHostedService, BackManagerService>(factory =>
            //{
            //    OrderManagerService order = new OrderManagerService();
            //    return new BackManagerService(options =>
            //    {
            //        options.Name = "������ʱ���";
            //        options.CheckTime = 5 * 1000;
            //        options.Callback = order.CheckOrder;
            //        options.Handler = order.OnBackHandler;
            //    });
            //});

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
                Console.WriteLine("��һ���м��ִ�����");
                await next();
            });

            //��ȡ·��
            string rootPath = env.ContentRootPath;
            string webRootPath = env.WebRootPath;
            string runType = env.EnvironmentName;

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

            //���þ�̬�ļ�����
            app.UseStaticFiles();
            //�ļ���ָ����·���ض���RequestPath
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    //RequestPath = "/file",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "file")),
            //    OnPrepareResponse = ctx =>
            //    {
            //        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
            //    }
            //});

            //���Է���Ŀ¼
            //app.UseDirectoryBrowser();

            //���Բ������ļ���������Ĭ���ļ�
            //app.UseDefaultFiles();

            //��ͬ��������ʵĿ¼�Ĺ���
            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = new PathString("/StaticFiles"),
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "�ļ�")),
                //�Ƿ�����Ŀ¼
                EnableDirectoryBrowsing = true
            });



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
            string conn = _configuration.GetConnectionString("conn");
            conn = _configuration["connection"];
            conn= _configuration["name"];
            conn = _configuration["default"];

            //GetSection��ʽ
            IConfigurationSection configurationSection = _configuration.GetSection("connection");


            //��ȡ����·�����ַ���
            dynamic type = (new Program()).GetType();

            var path1 = AppDomain.CurrentDomain.BaseDirectory;//����ʱ��Ŀ¼
            var path2 = AppContext.BaseDirectory;//����ʱ��Ŀ¼
            var path5 = Path.GetDirectoryName(type.Assembly.Location);//����ʱ��Ŀ¼
            var path3 = Environment.CurrentDirectory;//��ȡ����ִ��dotnet��������Ŀ¼
            var path4 = Directory.GetCurrentDirectory();//��ȡ����ִ��dotnet��������Ŀ¼

            //ϵͳ���System.Runtime.InteropServices���ƿռ��¡������������Runtime����Environment
            string name = RuntimeInformation.ProcessArchitecture.ToString();
            name = Assembly.GetExecutingAssembly().GetName().Name;//��ȡ��������



            conn = _appConfiguration.GetConnectionString("conn");
            conn = _appConfiguration["default"];


            //�����ͷ�ʽ��ȡ
            var id = _appConfiguration["model:id"];
            var val1 = _appConfiguration.GetSection("name");
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"name: {val1.Value}");
            var val2 = _appConfiguration.GetSection("model").GetSection("name");
            Console.WriteLine($"name: {val2.Value}");

            //ǿ���ͷ�ʽ��ȡ
            var info = _appConfiguration.GetValue<int>("model:id");


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
