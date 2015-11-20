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
            var factory = new UserContextFactory();
            var vm = new UserListVM(factory);

            return View(vm);
        }

        public ActionResult GamePage(Guid userUid)
        {
            var factory = new UserContextFactory();
            var context = factory.GetUserContext(userUid);

            return View(context);
        }

        public ActionResult CreateUser(string userName)
        {
            var factory = new UserContextFactory();

            var guid = GetGuid(factory);
            if (factory.GetAllUserContext().Any(x => x.UserName == userName))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            factory.AddUser(userName, guid);

            return RedirectToAction("GamePage", "Home", new { userUid = guid.ToString() });
        }

        private Guid GetGuid(UserContextFactory factory)
        {
            var guid = Guid.NewGuid();
            if (factory.GetAllUserContext().Any(x => x.UserUid == guid)) return GetGuid(factory);
            return guid;
        }
    }
}