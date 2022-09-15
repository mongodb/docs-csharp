using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.UsageExamples;

public class DeleteOneAsync
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "mongodb+srv://m001-student:m001-mongodb-basics@sandbox.lvj8g98.mongodb.net/?retryWrites=true&w=majority";

    public static void Main(string[] args)
    {
        Setup();

        // Delete a document using builders
        WriteLine("Deleting a document with builders...:");
        var result = DeleteARestaurantBuilderAsync();
        
        WriteLine($"Deleted documents: {result.Result.DeletedCount}");

        // Extra space for console readability 
        WriteLine();
    }

    private static async Task<DeleteResult> DeleteARestaurantBuilderAsync()
    {
        // start-delete-one-builders-async
        var filter = Builders<Restaurant>.Filter
            .Eq("cuisine", "Continental");

        var result = await _restaurantsCollection.DeleteOneAsync(filter);
        return result;
        // end-delete-one-builders-async
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

    public object Address { get; set; }

    public string Borough { get; set; }

    public List<object> Grades { get; set; }
}
// end-model