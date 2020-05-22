using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public interface ILogContext
    {
        IDisposable PushProperty(string key, string value);
    }
}
