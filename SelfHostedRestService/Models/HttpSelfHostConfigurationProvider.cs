using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHostedRestService.Models
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
            selfHostConfiguraiton.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            // настраиваем роуты
            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return selfHostConfiguraiton;
        }
    }
}
