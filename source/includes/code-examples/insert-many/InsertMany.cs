using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace CsharpExamples.UsageExamples;

public class InsertMany
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "<Your MongoDB URI>";

    public static void Main(string[] args)
    {
        Setup();

        // Attempt to find document before insert
        var filter = Builders<Restaurant>.Filter.Eq("name", "Mongo's Pizza");

        var foundRestaurants = _restaurantsCollection.Find(filter).ToList();

        WriteLine($"Number of restaurants found before insert: {foundRestaurants.Count}");

        // Extra space for console readability
        WriteLine();

        WriteLine("Inserting documents...");
        InsertManyRestaurants();

        // find and print newly inserted document
        foundRestaurants = _restaurantsCollection.Find(filter).ToList();

        WriteLine($"Number of restaurants inserted: {foundRestaurants.Count}");

        Cleanup();
    }

    private static void InsertManyRestaurants()
    {
        // delete sample document if already exists
        Cleanup();

        // start-insert-many
        // Helper method to generate 5 new restaurants
        List<Restaurant> restaurants = GenerateDocuments();

        _restaurantsCollection.InsertMany(restaurants);
        // end-insert-many
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

    private static List<Restaurant> GenerateDocuments()
    {
        // Generate 5 new restaurant documents
        List<Restaurant> restaurantsList = new List<Restaurant>();
        for (int i = 1; i <= 5; i++)
        {
            Restaurant newRestaurant = new()
            {
                Name = "Mongo's Pizza",
                RestaurantId = $"12345-{i}",
                Cuisine = "Pizza",
                Address = new BsonDocument
                {
                    {"street", "Pizza St"},
                    {"zipcode", "10003"},
                },
                Borough = "Manhattan",
            };

            restaurantsList.Add(newRestaurant);
        }

        return restaurantsList;
    }

    private static void Cleanup()
    {
        var filter = Builders<Restaurant>.Filter.Eq("name", "Mongo's Pizza");

        _restaurantsCollection.DeleteMany(filter);
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
}
// end-model
