using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public class UserNameProvider : IUserNameProvider
    {
        public string UserName => throw new InvalidDataException("Replace this exception with your username");
    }
}
