using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL.Utillity
{
    public class Shuffler<T> : IShuffler<T>
    {
        public IEnumerable<T> Shuffle(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException("Ссылка на список для сортировки не передана!");

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
