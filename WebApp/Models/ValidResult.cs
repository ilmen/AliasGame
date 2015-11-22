using System;

namespace WebApp.Models
{
    public class ValidResult
    {
        static ValidResult valid = new ValidResult()
        {
            IsValid = true,
            ErrorMessage = String.Empty
        };

        public static ValidResult Valid
        {
            get
            {
                return valid;
            }
        }

        #region Свойства
        public bool IsValid
        { get; private set; }

        public string ErrorMessage
        { get; private set; } 
        #endregion

        private ValidResult() { }

        public static ValidResult GetInvalidResult(string errorMessage)
        {
            return new ValidResult()
            {
                IsValid = false,
                ErrorMessage = errorMessage
            };
        }

        public void ThrowArgExceptionIfNotValid()
        {
            if (!this.IsValid) throw new ArgumentException(this.ErrorMessage);
        }
    }
}