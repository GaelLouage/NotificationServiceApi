using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Infrastructuur.Enums;

namespace Infrastructuur.Models
{
    public class Logger
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public LoggerType Type { get; set; }
        public DateTime? Date { get; set; }
    }
}
