using AliasGameBL.Models;
using RestWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace RestWebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var csf = new CardStorageFactory();
            var cs = csf.GetCardStorage();

            WordsController.SetCardStorage(cs);
            CardsController.SetCardStorage(cs);
        }
    }
}
