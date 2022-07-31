using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public interface IUserNameProvider
    {
        string UserName { get; }
    }
}
