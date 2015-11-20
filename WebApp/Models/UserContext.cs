using AliasGameBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserContext
    {
        public Guid UserUid
        { get; set; }

        public string UserName
        { get; set; }

        public int CardIndexSequenceStartSeed
        { get; set; }

        public List<int> CardIndexSequence
        { get; set; }
    }
}