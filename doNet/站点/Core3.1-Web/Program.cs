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
    /// ��������
    /// ��������
    /// </summary>
    public class Program
    {
        //ִ��˳��
        //ConfigureWebHostDefaults��ConfigureHostConfiguration��ConfigureAppConfiguration��
        //ConfigureServices��Configure��

        public static void Main(string[] args)
        {
            //����������������������������������
            //
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ���������������ڹ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //����Ĭ��������

            //�����������á����������������в���
            //������־���������õ�IIs�������iis����
            Host.CreateDefaultBuilder(args)
                //����web�������Դ�Ĭ������
                //web����������Kestrel   �������� Kestrel�����ܡ�
                //����ǰ׺Ϊ"aspnetcore"��������
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //�������ø��ֲ���
                    //����Kestrel
                    webBuilder.ConfigureKestrel(c => c.Limits.MaxRequestBodySize = 1000 * 1024 * 1024);

                    //������ַ����(һ��������json��)
                    //webBuilder.UseUrls("http://*:4999");

                    //��־����
                    //webBuilder.ConfigureLogging(c => c.SetMinimumLevel(LogLevel.Information));

                    //���þ�̬�ļ�������
                    //webBuilder.UseWebRoot("�ļ�������");

                    //�ƶ�webӦ��������
                    webBuilder.UseStartup<Startup>();
                });
    }
}
