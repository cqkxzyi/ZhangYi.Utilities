﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core2._2_Web.Filter;
using Core2._2_Web.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Core2._2_Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            GetConfig();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
            .AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //配置SingnalR
            services.AddSignalR();
        }




        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            string webRootPath = env.WebRootPath;
            string contentRootPath = env.ContentRootPath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //配置NLog
            loggerFactory.AddNLog();

            //配置SingnalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<WeChatHub>("/wechatHub");
            });
        }

        /// <summary>
        /// 获取json参数配置
        /// </summary>
        public void GetConfig() {
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);

            //添加 json 文件路径
            //Directory.GetCurrentDirectory()获取的是执行dotnet命令所在目录
            var builder = new ConfigurationBuilder().SetBasePath(currentDirectory).AddJsonFile("appsettings.json");
            //创建配置根对象
            var configurationRoot = builder.Build();

            //取配置根下的 Logging 部分
            var nameSection = configurationRoot.GetSection("AllowedHosts");
            //取配置根下的 family 部分
            var familySection = configurationRoot.GetSection("family");
            //取 family 部分下的 mother 部分下的 name 部分
            var motherNameSection = familySection.GetSection("Logging").GetSection("LogLevel");

            //Value 为文本值
            Console.WriteLine($"name: {nameSection.Value}");
        }
    }
}
