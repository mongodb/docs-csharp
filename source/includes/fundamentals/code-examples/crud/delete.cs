using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace TestRun.Fundamentals;

public class Delete
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "<connection string>";

    public static void Main(string[] args)
    {
        Setup();

        var filter = Builders<Restaurant>.Filter
            .Regex("address.street", "Pearl Street");

        var options = new DeleteOptions { Hint = "borough_1" };

        Console.WriteLine("Deleting documents...");
        var result = _restaurantsCollection.DeleteMany(filter, options);

        Console.WriteLine($"Deleted documents: {result.DeletedCount}");
        Console.WriteLine($"Result acknowledged? {result.IsAcknowledged}");
    }

    private static void Setup()
    {
        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        var mongoClient = new MongoClient(_mongoConnectionString);
        var restaurantsDatabase = mongoClient.GetDatabase("sample_restaurants");
        _restaurantsCollection = restaurantsDatabase.GetCollection<Restaurant>("restaurants");
    }
}

// start-model
public class Restaurant
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }

    [BsonElement("restaurant_id")]
    public string RestaurantId { get; set; }
    public string Cuisine { get; set; }
    public Address Address { get; set; }
    public string Borough { get; set; }
    public List<GradeEntry> Grades { get; set; }
}
// end-model