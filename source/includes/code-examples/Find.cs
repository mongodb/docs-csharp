using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CsharpExamples.UsageExamples;

public class FindOne
{
    static IMongoCollection<Restaurant> _restaurantsCollection;
    static string mongoConnectionString = "<Your Mongodb URI>";

    public static void Main(string[] args)
    {
        setup();

        // Find one document using builders
        FindOneRestaurantBuilder();

        // Extra space for console readability 
        Console.WriteLine();

        // Find one document using LINQ
        FindOneRestaurantLINQ();
    }

    static void FindOneRestaurantBuilder()
    {
        // start-find-builders
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Bagels N Buns");

        var restaurant = _restaurantsCollection.Find(filter).First();

        Console.WriteLine(restaurant.ToBsonDocument());
        // end-find-builders
    }

    static void FindOneRestaurantLINQ()
    {
        // start-find-linq
        var query = _restaurantsCollection.AsQueryable()
            .Where(r => r.Name == "Bagels N Buns");

        Console.WriteLine(query.ToBsonDocument());
        // end-find-linq
    }

    static void setup()
    {
        // Because our data is stored in the database with their names in camelCase
        // we use these lines to convert the names to TitleCase
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        var mongoUri = mongoConnectionString;
        var mongoClient = new MongoClient(mongoUri);
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
