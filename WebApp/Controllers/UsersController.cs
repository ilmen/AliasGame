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
    public class UsersController : Controller
    {
        public class UserData : IValidatedModel
        {
            public string UserName
            { get; set; }

            public Guid UserUid
            { get; set; }

            public bool IsNewUser()
            {
                return UserUid == Guid.Empty;
            }

            public ValidResult IsValid()
            {
                if (String.IsNullOrWhiteSpace(UserName)) return ValidResult.GetInvalidResult("Введено некорректное имя пользователя!");

                return ValidResult.Valid;
            }
        }

        public ActionResult Logon()
        {
            var data = GetUserData();

            if (data.IsNewUser())
            {
                return Create();    // TODO: передавать контекст-объект
            }

            var provider = new UserContextRepository();
            var uc = provider.Get(data.UserUid);
            if (uc == null) return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Неверные параметры входа! Пользователь не найден!");

            AddToSession(uc);

            return RedirectToGamePage();
        }

        public ActionResult Create()
        {
            var data = GetUserData();   // TODO: переход по ссылке [localhost] + "чтото" в умной строке адреса у яндекс браузера создает нового пользователя "чтото"

            if (!data.IsNewUser())
            {
                return Logon();    // TODO: передавать контекст-объект
            }

            try
            {
                data.IsValid().ThrowArgExceptionIfNotValid();
                
                var provider = new UserContextRepository();
                var wordProvider = new Words();
                var cardSizeProvider = new CardSizeProvider();
                var cardProvider = new Cards(cardSizeProvider.GetCardSize(), new Shuffler<string>(), new Cutter<string>());
                var cardCount = cardProvider.GetCards(wordProvider.GetAllWords()).Length;

                // TODO: уйти от запросов постоянных, просто кешировать слова, выделить слова и карточки все в один класс, в синглтон

                var newUC = provider.Create(Guid.NewGuid(), data.UserName, cardCount);
                provider.Add(newUC);
                AddToSession(newUC);

                return RedirectToGamePage();
            }
            catch (ArgumentException aex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, aex.Message);
            }
        }

        protected virtual ActionResult RedirectToLoginPage()
        {
            return RedirectToAction("Index", "Home");
        }

        protected virtual ActionResult RedirectToGamePage()
        {
            return RedirectToAction("Index", "Game");
        }

        protected virtual UserData GetUserData()
        {
            Guid userUid;
            Guid.TryParse(Request.Params["selectedUserUid"], out userUid);

            return new UserData()
            {
                UserUid = userUid,
                UserName = Request.Params["newusername"].ToString()
            };
        }

        protected virtual void AddToSession(UserContext uc)
        {
            if (uc == null) throw new ArgumentNullException("Получен не инициализированный контекст пользователя!");

            Session.Add("usercontext", uc);
        }
    }
}