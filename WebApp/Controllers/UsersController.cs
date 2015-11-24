using System;
using System.Collections.Generic;
using System.Linq;
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
            AddToSession(uc);

            return RedirectToGamePage();
        }

        public ActionResult Create()
        {
            var data = GetUserData();

            if (!data.IsNewUser())
            {
                return Logon();    // TODO: передавать контекст-объект
            }

            data.IsValid().ThrowArgExceptionIfNotValid();
 
            var provider = new UserContextRepository();
            var cardSizeProvider = new CardSizeProvider();
            var newUC = provider.Create(Guid.NewGuid(), data.UserName, cardSizeProvider.GetCardSize());
            provider.Add(newUC);
            AddToSession(newUC);

            return RedirectToGamePage();
        }

        protected virtual ActionResult RedirectToLoginPage()
        {
            return RedirectToAction("Index", "Home");
        }

        protected virtual ActionResult RedirectToGamePage()
        {
            return RedirectToAction("Game", "Home");
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