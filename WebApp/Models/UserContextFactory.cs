using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApp.Models
{
    public class UserContextFactory
    {
        public static int CardCount
        { get { return 10; } }
        
        static List<UserContext> UserContexts = new List<UserContext>();

        public UserContext GetUserContext(Guid userUid)
        {
            var context = UserContexts.FirstOrDefault(x => x.UserUid == userUid);

            if (context == null)
            {
                context = CreateUserContext(userUid);
            }

            return context;
        }

        public IEnumerable<UserContext> GetAllUserContext()
        {
            return UserContexts;
        }

        public UserContext CreateUserContext(Guid userUid)
        {
            var seed = CreateUserSeed();
            var cardIndexs = GetCardIndexSequence(seed, CardCount);

            return new UserContext()
            {
                UserUid = userUid,
                CardIndexSequenceStartSeed = seed,
                CardIndexSequence = cardIndexs,
            };
        }

        private List<int> GetCardIndexSequence(int userSeed, int cardCount)
        {
            var rnd = new Random(userSeed);
            return Enumerable.Range(1, cardCount)
                .OrderBy(x => rnd.Next(cardCount))
                .ToList();
        }

        private int CreateUserSeed()
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            return rnd.Next(int.MaxValue);
        }

        public void AddUser(string userName, Guid guid)
        {
            var user = GetUserContext(guid);
            user.UserName = userName;
            UserContexts.Add(user);
        }
    }
}
