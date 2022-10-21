using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Common;
using Common.Entities;
using Newtonsoft.Json;

namespace Worker
{
    internal class Program
    {
        private static BasicAWSCredentials credentials;
        private static AmazonSQSClient sqsClient;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting to read messages from the queue...");

            credentials = new BasicAWSCredentials(Constants.QueueKeyId, Constants.QueueKey);
            sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

            await ReadMessagesAsync();
        }

        public static async Task ReadMessagesAsync()
        {
            var request = new ReceiveMessageRequest()
            {
                QueueUrl = Constants.QueueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 10
            };

            while (true)
            {
                var messages = await sqsClient.ReceiveMessageAsync(request);

                foreach (var message in messages.Messages)
                {
                    var workerEntity = JsonConvert.DeserializeObject<WorkerEntity>(message.Body);

                    _ = sqsClient.DeleteMessageAsync(new DeleteMessageRequest()
                    {
                        QueueUrl = Constants.QueueUrl,
                        ReceiptHandle = message.ReceiptHandle
                    });
                }
            }
        }
    }
}