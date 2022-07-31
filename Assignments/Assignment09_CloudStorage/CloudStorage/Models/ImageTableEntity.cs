using Microsoft.Azure.Cosmos.Table;

namespace CloudStorage.Models
{
    /// <summary>
    /// This class is persisted to a database. It's a "model."
    /// </summary>
    public class ImageTableEntity : TableEntity
    {
        public string Name { get; set; }

        public string UserName
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string Id {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public bool UploadComplete { get; set; }
    }
}
