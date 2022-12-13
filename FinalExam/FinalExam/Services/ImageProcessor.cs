using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace FinalExam.Services
{
    public class ImageProcessor
    {
        private readonly BasicAWSCredentials credentials;
        private readonly AmazonSQSClient sqs;

        public ImageProcessor()
        {
            credentials = new BasicAWSCredentials("ABCDEFGHIJKLMNO", "thisisthekeyformyamazonaccountnotreallyhaha");
            sqs = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);
        }

        public void QueueProcessImage(string name, string url)
        {
            var sendMessage = new SendMessageRequest
            {
                QueueUrl = "https://sqs.us-east-1.amazonaws.com/570108184261/boardgameimages",
                MessageBody = JsonConvert.SerializeObject(new ImageMessage() { Name = name, Url = url })
            };
            sqs.SendMessageAsync(sendMessage);
        }

        private class ImageMessage
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}
