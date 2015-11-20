using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserListVM
    {
        public IEnumerable<UserContext> Users
        {
            get
            {
                return Factory.GetAllUserContext();
            }
        }

        public UserContextFactory Factory
        { get; set; }

        public Guid SelectedUserUid
        { get; set; }

        public string StringSelectedUserUid
        {
            get
            {
                return SelectedUserUid.ToString();
            }
            set
            {
                Guid guid;
                Guid.TryParse(value, out guid);
                SelectedUserUid = guid;
            }
        }

        public string SelectedUserName
        { get; set; }

        public UserListVM(UserContextFactory factory)
        {
            this.Factory = factory;
        
            var user = factory.GetAllUserContext().FirstOrDefault();
            if (user != null)
            {
                this.SelectedUserName = user.UserName;
                this.SelectedUserUid = user.UserUid;
            }
        }
    }
}