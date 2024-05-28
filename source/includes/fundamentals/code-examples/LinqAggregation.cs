using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

public class Aggregation
{
    // Replace with your connection string
    private const string MongoConnectionString = "<YOUR_CONNECTION_STRING>";

    public static void Main(string[] args)
    {   
        var mongoClient = new MongoClient(MongoConnectionString);

        var database = mongoClient.GetDatabase("sample_restaurants");
        var collection = database.GetCollection<Restaurant>("restaurants");
        var queryableCollection = collection.AsQueryable();

        // begin-aggregation
        // Executes the $match and $group aggregation stages
        var query = queryableCollection
                        .Where(r => r.Cuisine == "Bakery")
                        .GroupBy(r => r.Borough)
                        .Select(g => new { _id = g.Key, Count = g.Count() });
    

        // Prints the aggregated results
        foreach(var result in query.ToList())
        {
            Console.WriteLine(result);
        }
        // end-aggregation
    }

    public class Restaurant {
        [BsonElement("borough")]
        public string Borough { get; set; }

        [BsonElement("cuisine")]
        public string Cuisine { get; set; }
    }
}