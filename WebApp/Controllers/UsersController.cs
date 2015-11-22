using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public string Create()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this.Request.Params);
        }
    }
}