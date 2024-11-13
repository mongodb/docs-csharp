using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

public class LimitSortSkip
{
    // Replace with your connection string
    private const string MongoConnectionString = "<connection string URI>";

    public static void Main(string[] args)
    {   
        var mongoClient = new MongoClient(MongoConnectionString);
        var database = mongoClient.GetDatabase("sample_restaurants");
        var collection = database.GetCollection<Restaurant>("restaurants");
        
        {
            // start-project-include
            var filter = Builders<Restaurant>.Filter.Eq("name", "Emerald Pub");
            var projection = Builders<Restaurant>.Projection
                .Include("name")
                .Include("cuisine");

            var results = collection.Find(filter).Project(projection).ToList();
            foreach (var result in results)
            {
                Console.WriteLine(result.ToJson());
            }
            // end-project-include
        }

        {
            // start-project-include-without-id
            var filter = Builders<Restaurant>.Filter.Eq("name", "Emerald Pub");
            var projection = Builders<Restaurant>.Projection
                .Include("name")
                .Include("cuisine")
                .Exclude("_id");

            var results = collection.Find(filter).Project(projection).ToList();
            foreach (var result in results)
            {
                Console.WriteLine(result.ToJson());
            }
            // end-project-include-without-id
        }

        {
            // start-project-exclude
            var filter = Builders<Restaurant>.Filter.Eq("name", "Emerald Pub");
            var projection = Builders<Restaurant>.Projection
                .Exclude("cuisine");

            var results = collection.Find(filter).Project(projection).ToList();
            foreach (var result in results)
            {
                Console.WriteLine(result.ToJson());
            }
            // end-project-exclude
        }

    }
}

// start-model
public class Restaurant {
    public ObjectId? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("cuisine")]
    public string? Cuisine { get; set; }

    [BsonElement("address")]
    public Address? Address { get; set; }
}

public class Address
{
    public string Building { get; set; }

    [BsonElement("coord")]
    public double[] Coordinates { get; set; }

    public string Street { get; set; }

    [BsonElement("zipcode")]
    public string ZipCode { get; set; }
}
// end-model