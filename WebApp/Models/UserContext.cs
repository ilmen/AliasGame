using AliasGameBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Serializable]
    public class UserContext : IValidatedModel
    {
        public Guid UserUid
        { get; set; }

        public string UserName
        { get; set; }

        public IEnumerable<int> CardIndexSequence
        { get; set; }

        public ValidResult IsValid()
        {
            if (UserUid == Guid.Empty)
            {
                return ValidResult.GetInvalidResult("Не задано поле UserUid в контексте пользователя!");
            }

            if (String.IsNullOrEmpty(UserName))
            {
                return ValidResult.GetInvalidResult("Не задано поле UserName в контексте пользователя!");
            }
            
            if (CardIndexSequence == null)
            {
                return ValidResult.GetInvalidResult("Не задано поле CardIndexSequence в контексте пользователя!");
            }
            
            //if (CardIndexSequence.Count() != Cards.CardCount)
            //{
            //    return ValidResult.CreateInvalidResult("Некорректная длина поля CardIndexSequence! Ожидаемая длина: " + Cards.CardCount);
            //}
            
            if (CardIndexSequence.Count() != CardIndexSequence.Distinct().Count())
            {
                return ValidResult.GetInvalidResult("Поле CardIndexSequence должно содержать только уникальные значения!");
            }

            var maxIndex = CardIndexSequence.Max();
            if (Enumerable.Range(0, maxIndex).Except(CardIndexSequence).Count() != 0)
            {
                return ValidResult.GetInvalidResult("Поле CardIndexSequence должно содержать все числа от 0 до " + maxIndex + "!");
            }

            return ValidResult.Valid;
        }
    }
}