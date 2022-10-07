using MongoDB.Driver;
using static System.Console;
using MongoDB.Bson;

namespace TestRun.Fundamentals;

public class Builders
{
    private static IMongoCollection<Flower> _flowerCollection;
    private static string _mongoConnectionString = "<Your MongoDB URI>";
    
    public static void Main(string[] args)
    {
        Setup();
        
        var builder = Builders<Flower>.Filter;
        var filter = builder.AnyEq(flower => flower.Season, "winter");

        WriteLine("Finding documents...");
        var result = _flowerCollection.Find(filter).First();

        WriteLine(result.ToJson());
        // WriteLine($"Result acknowledged? {result.IsAcknowledged}");
    }
    private static void Setup()
    {
        // Establish the connection to MongoDB and get the restaurants database
        var mongoClient = new MongoClient(_mongoConnectionString);
        var myDatabase = mongoClient.GetDatabase("plants");
        _flowerCollection = myDatabase.GetCollection<Flower>("flowers");
    }
}

// start-model
public class Flower
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public List<string> Season { get; set; }
}
// end-model