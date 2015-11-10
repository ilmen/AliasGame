using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfHostedRestService.Infrastructure
{
    public interface IShuffler<T>
    {
        IEnumerable<T> Shuffle(IEnumerable<T> collection);
    }
}
