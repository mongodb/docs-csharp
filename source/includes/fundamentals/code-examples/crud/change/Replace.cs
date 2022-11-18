using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.Fundamentals.ReplaceOne;

public class ReplaceOne
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private const string MongoConnectionString = "<Your MongoDB URI>";

    public static void Main(string[] args)
    {
        Setup();

        // Create filter 
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Name, "Pizza Town");

        // Find restaurant named "Pizza Town"
        var oldRestaurant = _restaurantsCollection.Find(filter).First();
        WriteLine($"Restaurant with ID {oldRestaurant.Id} before replacement: {oldRestaurant.Name}");

        // Replace one document synchronously
        var syncResult = ReplaceOneRestaurant();
        WriteLine($"Restaurants modified by replacement: {syncResult.ModifiedCount}");

        var firstPizzaRestaurant = _restaurantsCollection.Find(filter).First();
        WriteLine($"Restaurant with id {oldRestaurant.Id} after replacement: {firstPizzaRestaurant.Name}");

        Write("Resetting sample data...");
        _restaurantsCollection.ReplaceOneAsync(filter, oldRestaurant);
        WriteLine("done.");
    }

    private static ReplaceOneResult ReplaceOneRestaurant()
    {
        // start-parameters
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Name, "Pizza Town");

        Restaurant newRestaurant = new()
        {
            Name = "Food World",
            Cuisine = "American",
            Address = new BsonDocument
            {
                {"street", "Food St"},
                {"zipcode", "10003"},
            },
            Borough = "Manhattan",
        };
        // end-parameters

        // Find ID of first pizza restaurant
        var oldRestaurant = _restaurantsCollection.Find(filter).First();
        var oldId = oldRestaurant.Id;

        return _restaurantsCollection.ReplaceOne(filter, newRestaurant);
        // end-method-body
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