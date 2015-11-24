using System;
namespace WebApp.Models
{
    public interface IValidatedModel
    {
        ValidResult IsValid();
    }
}
