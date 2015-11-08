using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Linq;

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
                Console.ReadLine();
                return;
            }

            var url = "http://localhost:" + args.First();

            var selfHostConfiguraiton = new HttpSelfHostConfiguration(url);

            // оставляем только JSON сериализатор
            selfHostConfiguraiton.Formatters.Clear();
            selfHostConfiguraiton.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            // настраиваем роуты
            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // запускаем сервер
            try
            {
                using (var server = new HttpSelfHostServer(selfHostConfiguraiton))
                {
                    server.OpenAsync().Wait();
                }
                Console.WriteLine("Service started (" + url + ")");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка");
                Console.WriteLine(ex);
            }
            Console.Write("<Press any key to exit>");
            Console.ReadLine();
        }

        static bool IsNumeric(string arg)
        {
            if (String.IsNullOrEmpty(arg)) return false;
            return arg.All(char.IsDigit);
        }
    }
}
