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

        public ActionResult Game(Guid userUid)
        {
            var repos = new UserContextRepository();
            var context = repos.Get(userUid);
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
            var cardSizeProvider = new CardSizeProvider();
            var provider = new Cards(cardSizeProvider.GetCardSize(), new Shuffler<string>(), new StringCutter());
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
            var repos = new UserContextRepository();

            var guid = Guid.NewGuid();
            if (repos.GetAll().Any(x => x.UserName == userName))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cardSizeProvider = new CardSizeProvider();

            var user = repos.Create(guid, userName, cardSizeProvider.GetCardSize());
            repos.Add(user);

            //return RedirectToAction("Game", "Home", new { userUid = guid.ToString() });
            return Redirect("/Home/Index");
        }
    }
}