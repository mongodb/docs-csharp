using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.UsageExamples.DeleteMany;

public class DeleteMany
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<Your MongoDB URI>";

    public static void Main(string[] args)
    {
        Setup();

        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Borough, "Brooklyn");

        var docs = _restaurantsCollection.Find(filter).ToList();

        // Deleting documents using builders
        WriteLine("Deleting documents...");
        var result = DeleteMultipleRestaurantsBuilder();

        WriteLine($"Deleted documents: {result.DeletedCount}");

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