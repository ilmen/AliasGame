using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Linq;
using SelfHostedRestService.Models;

namespace SelfHostedRestService
{
    class Program
    {
        static void Main(string[] args)
        {
            // проверяем входной параметр - номер порта для сервера
            if (args.Length < 1 || !IsNumeric(args.First()) || Convert.ToInt32(args.First()) < 0 || Convert.ToInt32(args.First()) > ushort.MaxValue)
            {
                Console.WriteLine("Неверный входной параметр! Ожидался номер порта!");
                WaitKeyPressedAndExit();
            }

            var url = "http://localhost:" + args.First();

            var configProvider = new HttpSelfHostConfigurationProvider(url);
            var config = configProvider.GetConfiguration();

            using (var server = new HttpSelfHostServer(config))
            {
                // запускаем сервер
                try
                {
                    server.OpenAsync().Wait();
                    Console.WriteLine("Service started (" + url + ")");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка");
                    Console.WriteLine(ex);
                }
                WaitKeyPressedAndExit();
            }
        }

        static bool IsNumeric(string arg)
        {
            if (String.IsNullOrEmpty(arg)) return false;
            return arg.All(char.IsDigit);
        }

        static void WaitKeyPressedAndExit()
        {
            Console.Write("<Press any key to exit>");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
