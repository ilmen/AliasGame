using AliasGameBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class GameSession
    {
        public UserContext Context
        { get; set; }

        public Card CurrentCard
        { get; set; }
    }
}