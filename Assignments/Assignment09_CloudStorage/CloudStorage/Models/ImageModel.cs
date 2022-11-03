using Azure;
using Azure.Data.Tables;
using CloudStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Models
{

    public class ImageModel : ITableEntity
    {
        public ImageModel()
        {

        }

        public ImageModel(string userName, string name)
        {
            this.UserName = userName;
            this.Name = name;
        }

        public string PartitionKey { get => this.UserName; set { this.UserName = value; } }

        public string RowKey { get => this.Id; set { this.Id = value; } }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string UserName { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public bool UploadComplete { get; set; }
    }
}
