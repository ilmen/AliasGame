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
    public class GameController : Controller
    {
        static Random cardIndexRandomizer = new Random((int)DateTime.Now.Ticks);

        // GET: Game
        public ActionResult Index()
        {
            var uc = ReadUserContextFromSession();
            if (uc == null) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var cardSizeProvider = new CardSizeProvider();
            var cardIndex = GetNextCardIndex(uc);

            var card = GetCard(cardIndex);

            var vm = new GameVM()
            {
                Context = uc,
                CurrentCard = card
            };

            return View(vm);
        }

        private Card GetCard(int cardIndex)
        {
            var words = GetWords();
            var cardSizeProvider = new CardSizeProvider();
            var provider = new CardFactory(cardSizeProvider.GetCardSize(), new Shuffler<string>(), new Cutter<string>());
            return provider.GetCards(words)
                .FirstOrDefault(x => x.Index == cardIndex);
        }

        private string[] GetWords()
        {
            var provider = new WordFactory();
            return provider.GetAllWords();
        }

        protected virtual int GetNextCardIndex(UserContext uc)
        {
            var maxIndex = uc.CardIndexSequence.Max();
            return cardIndexRandomizer.Next(0, maxIndex);
        }

        protected virtual UserContext ReadUserContextFromSession()
        {
            if (!Session.Keys.Cast<string>().Contains("usercontext")) return null;

            return (UserContext)Session["usercontext"];
        }

        //public ActionResult Game(Guid userUid)
        //{
        //    var repos = new UserContextRepository();
        //    var context = repos.Get(userUid);
        //    var card = GetCard(context.CardIndexSequence.FirstOrDefault());
        //    var session = new GameSession()
        //    {
        //        Context = context,
        //        CurrentCard = card
        //    };

        //    return View(session);
        //}
    }
}