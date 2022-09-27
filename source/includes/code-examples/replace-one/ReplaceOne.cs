using System.Threading.Tasks.Sources;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CSharpExamples.UsageExamples.ReplaceOne;

public class ReplaceOne
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "<Your MongoDB URI>";


    public static void Main(string[] args)
    {
        Setup();

        // Create filter for pizza restaurant
        var filter = Builders<Restaurant>.Filter.Eq("cuisine", "Pizza");

        // Cache first pizza restaurant
        var oldPizzaRestaurant = _restaurantsCollection.Find(filter).First();

        WriteLine($"First pizza restaurant before replacement: {oldPizzaRestaurant.Name}");
        string id = oldPizzaRestaurant.RestaurantId;

        // Replace one document synchronously
        var syncResult = ReplaceOneRestaurant(id);
        WriteLine($"Restaurants modified by replacement: {syncResult.ModifiedCount}");

        var firstPizzaRestaurant = _restaurantsCollection.Find(filter).First();
        WriteLine($"First pizza restaurant after replacement: {firstPizzaRestaurant.Name}");

        Write("Resetting sample data...");
        _restaurantsCollection.ReplaceOne(filter, oldPizzaRestaurant);
        WriteLine("done.");
    }

    private static ReplaceOneResult ReplaceOneRestaurant(string newId)
    {
        // start-replace-one
        Restaurant newPizzaRestaurant = new()
        {
            RestaurantId = newId,
            Name = "Mongo's Pizza",
            Cuisine = "Pizza",
            Address = new BsonDocument
            {
                {"street", "Pizza St"},
                {"zipcode", "10003"},
            },
            Borough = "Manhattan",
        };

        var filter = Builders<Restaurant>.Filter
            .Eq("cuisine", "Pizza");

        return _restaurantsCollection.ReplaceOne(filter, newPizzaRestaurant);
        // end-replace-one
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