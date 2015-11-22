using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL.Utillity
{
    public interface IShuffler<T>
    {
        IEnumerable<T> Shuffle(IEnumerable<T> collection);
    }
}
