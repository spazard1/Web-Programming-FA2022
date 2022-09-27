namespace Hobbits.Services
{
    public class RequestIdGenerator : IRequestIdGenerator
    {

        public string RequestId { get; } = Guid.NewGuid().ToString();
    }
}
