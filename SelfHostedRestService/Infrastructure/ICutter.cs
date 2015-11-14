using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Infrastructure
{
    public interface ICutter<T>
    {
        T[] CutMultipleOfBasis(T[] entities, int basis);
    }
}
