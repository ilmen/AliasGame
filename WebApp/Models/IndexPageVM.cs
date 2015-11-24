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
            lst.Add(new UserContext() { UserName = "Новый игрок", UserUid = Guid.Empty });
            
            UCList = lst;

            SelectedUC = UCList.FirstOrDefault();
        }
    }
}