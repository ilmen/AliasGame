using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL.Utillity
{
    public interface ICutter<T>
    {
        IEnumerable<T> CutMultipleOfBasis(IEnumerable<T> entities, int basis);
    }
}
