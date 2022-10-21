using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Common;
using Common.Entities;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Kuscotopia.Services
{
    public class QueueService
    {

        private readonly BasicAWSCredentials credentials;
        private readonly AmazonSQSClient sqsClient;

        public QueueService()
        {
            credentials = new BasicAWSCredentials(Constants.QueueKeyId, Constants.QueueKey);
            sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);
        }

        public async Task QueueWorkAsync(WorkerEntity workerEntity)
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = Constants.QueueUrl,
                MessageBody = JsonConvert.SerializeObject(workerEntity)
            };

            await sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
