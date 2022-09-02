using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CsharpExamples.Models
{
    public class Movie
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        public object Year { get; set; }

        public List<string> Cast { get; set; }

        public DateTime Released { get; set; }

        public string Rated { get; set; }

        public string Type { get; set; }

        public List<string> Directors { get; set; }

        public List<string> Genres { get; set; }

        public int Runtime { get; set; }

        public BsonDocument Tomatoes { get; set; }

        public BsonDocument Awards { get; set; }

        [BsonExtraElements]
        public BsonDocument ExtraFields { get; set; }
    }
}