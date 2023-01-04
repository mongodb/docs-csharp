using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.UsageExamples;

public class DeleteManyAsync
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "<Your MongoDB URI>";

    public static async Task Main(string[] args)
    {
        Setup();

        var docs = _restaurantsCollection.Find(Builders<Restaurant>.Filter
            .Eq(r => r.Borough, "Brooklyn")).ToList();

        // Deleting documents using builders
        WriteLine("Deleting documents...");
        var result = await DeleteMultipleRestaurantsBuilderAsync();

        WriteLine($"Deleted documents: {result.DeletedCount}");

        Restore(docs);

        return result;
    }

    private static async Task<DeleteResult> DeleteMultipleRestaurantsBuilderAsync()
    {
        // start-delete-many-async
        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Borough, "Brooklyn");

        return await _restaurantsCollection.DeleteManyAsync(filter);
        // end-delete-many-async
    }

    private static void Restore(IEnumerable<Restaurant> docs)
    {
        _restaurantsCollection.InsertMany(docs);
        WriteLine("Resetting sample data...done.");
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