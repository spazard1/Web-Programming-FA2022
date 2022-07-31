using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }

        string AccountKey { get; }
    }
}
