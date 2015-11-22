using System;
namespace WebApp.Models
{
    public interface IModelWithValidCheck
    {
        ValidResult IsValid();
    }
}
