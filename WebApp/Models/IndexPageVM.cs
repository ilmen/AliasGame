using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class IndexPageVM
    {
        public UserContext SelectedUC
        { get; set; }

        public IEnumerable<UserContext> UCList
        { get; set; }

        public IndexPageVM(IEnumerable<UserContext> ucList)
        {
            var lst = ucList.OrderBy(x => x.UserName).ToList();

            lst.Insert(0, new UserContext() { UserName = "Новый игрок", UserUid = Guid.Empty });
            
            UCList = lst;

            SelectedUC = UCList.FirstOrDefault();
        }

        // TODO: сделать отображение имени игрока и кнопки "выход", если пользователь попал на эту страницу уже авторизованным
        // TODO: не всегда прячеться textbox с новым именем, если в combox выбрано чтото отличное от "новый игрок
        // TODO: придумать сброс последовательности карточек
    }
}