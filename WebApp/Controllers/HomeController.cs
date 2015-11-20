using AliasGameBL;
using AliasGameBL.Models;
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
            var card = GetCard(context.CardIndexSequence.FirstOrDefault());
            var session = new GameSession()
            {
                Context = context,
                CurrentCard = card
            };

            return View(session);
        }

        private Card GetCard(int cardIndex)
        {
            var words = GetWords();
            var provider = new Cards(10, new StringShuffler(), new StringCutter());
            return provider.GetCards(words)
                .FirstOrDefault(x => x.Index == cardIndex);
        }

        private string[] GetWords()
        {
            var provider = new Words();
            return provider.GetAllWords();
        }

        public ActionResult CreateUser(string userName)
        {
            var factory = new UserContextFactory();

            var guid = GetGuid(factory);
            if (factory.GetAllUserContext().Any(x => x.UserName == userName))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            factory.AddUser(userName, guid);

            //return RedirectToAction("GamePage", "Home", new { userUid = guid.ToString() });
            return Redirect("/Home/Index");
        }

        private Guid GetGuid(UserContextFactory factory)
        {
            var guid = Guid.NewGuid();
            if (factory.GetAllUserContext().Any(x => x.UserUid == guid)) return GetGuid(factory);
            return guid;
        }
    }
}