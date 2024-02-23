// Deletes multiple documents from a collection by using the C# driver

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CSharpExamples.UsageExamples.DeleteMany;

public class DeleteMany
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<connection string>";

    public static void Main(string[] args)
    {
        Setup();

        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Borough, "Brooklyn");

        var docs = _restaurantsCollection.Find(filter).ToList();

        // Deleting documents using builders
        Console.WriteLine("Deleting documents...");
        var result = DeleteMultipleRestaurantsBuilder();

        Console.WriteLine($"Deleted documents: {result.DeletedCount}");

        Restore(docs);
    }

    private static DeleteResult DeleteMultipleRestaurantsBuilder()
    {
        // start-delete-many-builders
        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Borough, "Brooklyn");

        return _restaurantsCollection.DeleteMany(filter);
        // end-delete-many-builders
    }

    private static void Restore(IEnumerable<Restaurant> docs)
    {
        _restaurantsCollection.InsertMany(docs);
        Console.WriteLine("Resetting sample data...done.");
    }

    private static void Setup()
    {
        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        var mongoClient = new MongoClient(MongoConnectionString);
        var restaurantsDatabase = mongoClient.GetDatabase("sample_restaurants");
        _restaurantsCollection = restaurantsDatabase.GetCollection<Restaurant>("restaurants");
    }
}

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

public class Address
{
    public string Building { get; set; }

    [BsonElement("coord")]
    public double[] Coordinates { get; set; }

    public string Street { get; set; }

    [BsonElement("zipcode")]
    public string ZipCode { get; set; }
}

public class GradeEntry
{
    public DateTime Date { get; set; }

    public string Grade { get; set; }

    public float? Score { get; set; }
}
