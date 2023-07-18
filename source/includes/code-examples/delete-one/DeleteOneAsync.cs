using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CSharpExamples.UsageExamples.DeleteOne;

public class DeleteOneAsync
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<connection string>";

    public static async Task Main(string[] args)
    {
        Setup();

        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Name, "Ready Penny Inn");

        var doc = _restaurantsCollection.Find(filter).First();

        // Delete a document using builders
        Console.WriteLine("Deleting a document with builders...");
        var result = await DeleteARestaurantBuilderAsync();

        Console.WriteLine($"Deleted documents: {result.DeletedCount}");

        Restore(doc);
    }

    private static async Task<DeleteResult> DeleteARestaurantBuilderAsync()
    {
        // start-delete-one-builders-async
        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Name, "Ready Penny Inn");

        return await _restaurantsCollection.DeleteOneAsync(filter);
        // end-delete-one-builders-async
    }

    private static void Restore(Restaurant doc)
    {
        _restaurantsCollection.InsertOne(doc);
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

    public float Score { get; set; }
}
