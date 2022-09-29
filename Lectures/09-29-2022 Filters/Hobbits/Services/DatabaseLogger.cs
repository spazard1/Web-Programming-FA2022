namespace Hobbits.Services
{
    public class DatabaseLogger : IHobbitLogger
    {
        public void WriteLine(string message)
        {
            // pretend this writes to a database somewhere
        }
    }
}
