﻿using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHostedRestService.Infrastructure
{
    public class HttpSelfHostConfigurationProvider
    {
        private readonly string Url;

        public HttpSelfHostConfigurationProvider(string url)
        {
            Url = url;
        }

        public HttpSelfHostConfiguration GetConfiguration()
        {
            var selfHostConfiguraiton = new HttpSelfHostConfiguration(Url);

            // оставляем только JSON сериализатор
            selfHostConfiguraiton.Formatters.Clear();
            var jsonFormater = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            selfHostConfiguraiton.Formatters.Add(jsonFormater);

            // добавляем шаблон Rest-контроллеров
            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // добавляем Index страницу
            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" });

            return selfHostConfiguraiton;
        }
    }
}
