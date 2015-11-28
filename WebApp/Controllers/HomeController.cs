using AliasGameBL;
using AliasGameBL.Models;
using AliasGameBL.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var repos = new UserContextRepository();
            var ucList = repos.GetAll();
            var vm = new IndexPageVM(ucList);

            return View(vm);
        }
    }
}