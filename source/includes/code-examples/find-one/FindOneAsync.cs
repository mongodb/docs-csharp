using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using static System.Console;

namespace CSharpExamples.UsageExamples.FindOne;

public class FindOneAsync
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<Your MongoDB URI>";

    public static async Task Main(string[] args)
    {
        Setup();

        // Find one document using builders
        var buildersDocument = await FindOneRestaurantBuilderAsync().ToBsonDocument();
        WriteLine("Finding a document with builders...");
        WriteLine(buildersDocument);

        // Extra space for console readability
        WriteLine();

        // Find one document using LINQ
        var linqDocument = await FindOneRestaurantLinqAsync().ToBsonDocument();
        WriteLine("Finding a document with LINQ...");
        WriteLine(linqDocument);
    }

    private static async Task<Restaurant> FindOneRestaurantBuilderAsync()
    {
        // start-find-builders
        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Name, "Bagels N Buns");

        return await _restaurantsCollection.Find(filter).FirstOrDefaultAsync();
        // end-find-builders
    }

    private static async Task<Restaurant> FindOneRestaurantLinqAsync()
    {
        // start-find-linq
        return await _restaurantsCollection.AsQueryable()
            .Where(r => r.Name == "Bagels N Buns").FirstOrDefaultAsync();
        // end-find-linq
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