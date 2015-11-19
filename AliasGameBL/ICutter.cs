using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliasGameBL
{
    public interface ICutter<T>
    {
        T[] CutMultipleOfBasis(T[] entities, int basis);
    }
}
