using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.UsageExamples.FindOne;

public class FindOne
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<Your MongoDB URI>";

    public static void Main(string[] args)
    {
        Setup();

        // Find one document using builders
        WriteLine("Finding a document with builders...");
        FindOneRestaurantBuilder();

        // Extra space for console readability 
        WriteLine();

        // Find one document using LINQ
        WriteLine("Finding a document with LINQ...");
        FindOneRestaurantLinq();
    }

    private static void FindOneRestaurantBuilder()
    {
        // start-find-builders
        var filter = Builders<Restaurant>.Filter
            .Eq(r => r.Name, "Bagels N Buns");

        var restaurant = _restaurantsCollection.Find(filter).FirstOrDefault();
        // end-find-builders

        WriteLine(restaurant.ToBsonDocument());
    }

    private static void FindOneRestaurantLinq()
    {
        // start-find-linq
        var query = _restaurantsCollection.AsQueryable()
            .Where(r => r.Name == "Bagels N Buns").FirstOrDefault();
        // end-find-linq

        WriteLine(query.ToBsonDocument());
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