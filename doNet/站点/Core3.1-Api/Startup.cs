using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Core31.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        public void ConfigureServices(IServiceCollection services)
        {
            // ע��Swagger����
            services.AddSwaggerGen(c =>
            {
                // ����ĵ���Ϣ
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "��Ŀ�ӿ�", Version = "v1", Description = "ASP.NET CORE WebApi ����",
                    Contact = new OpenApiContact
                    {
                        Name = "����",
                        Email = "cqkxzyi@gmail.com"
                    }
                });
                // ʹ�÷����ȡxml�ļ�����������ļ���·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse.
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddControllers();
        }

       

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ����Swagger�м��
            app.UseSwagger();
            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApi");
                c.RoutePrefix = string.Empty;//�����ַΪ��Ŀ¼

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //RESTful API��ʽ
                //endpoints.MapControllers();

                //�Զ���ģʽ
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
