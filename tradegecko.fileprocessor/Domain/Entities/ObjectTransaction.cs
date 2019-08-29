using System;
using System.Collections.Generic;

namespace tradegecko.fileprocessor.Domain.Entities
{
    public partial class ObjectTransaction
    {
        public int TransactionId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ObjectChanges { get; set; }
    }
}
