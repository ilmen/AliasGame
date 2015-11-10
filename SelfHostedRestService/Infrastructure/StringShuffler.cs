using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Infrastructure
{
    public class StringShuffler : IShuffler<string>
    {
        public IEnumerable<string> Shuffle(IEnumerable<string> collection)
        {
            var seed = GetRandomSeed();
            var rnd = new Random(seed);
            
            return collection.OrderBy(x => rnd.Next());
        }

        private int GetRandomSeed()
        {
            return (int)SystemTime.Now.Ticks;
        }
    }
}
