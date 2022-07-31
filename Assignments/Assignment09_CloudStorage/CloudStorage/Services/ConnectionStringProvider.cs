using CloudStorage.Services;
using System.IO;

namespace CloudStorage.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString => throw new InvalidDataException("Replace this exception with the connection string from moodle");

        public string AccountKey {
            get {
                var startIndex = ConnectionString.IndexOf("AccountKey=") + "AccountKey=".Length;
                var length = ConnectionString.IndexOf(";", startIndex) - startIndex;
                return ConnectionString.Substring(startIndex, length);
            }
        }
    }
}
