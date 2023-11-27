using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IPZ_docker.Entities
{
    public class Seller
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Surname{ get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public string sex { get; set; }

    }
}
