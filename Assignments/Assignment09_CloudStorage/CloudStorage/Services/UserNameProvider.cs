using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public class UserNameProvider : IUserNameProvider
    {
        public string UserName => throw new InvalidOperationException("Replace this exception with your username.");
    }
}
