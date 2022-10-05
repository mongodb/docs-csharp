using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using static System.Console;

namespace TestRun.Fundamentals;

public class Insert
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    private static string _mongoConnectionString = "<Your MongoDB URI>";
    
    public static void Main(string[] args)
    {
        Setup();
        
        // start-insert
        List<Restaurant> restaurantsList = new List<Restaurant>();
        var r1 = new Restaurant() { Id = "2", Name = "Été Bleu", Cuisine = "French" };
        var r2 = new Restaurant() { Id = "4", Name = "Lucky Bird", Cuisine = "Café/Coffee/Tea" };
        var r3 = new Restaurant() { Id = "6", Name = "Wildflower Café", Cuisine = "Vegetarian" };
        var r4 = new Restaurant() { Id = "8", Name = "Blue Moon Grill", Cuisine = "American" };
        restaurantsList.AddRange(new List<Restaurant>() {r1, r2, r3, r4});

        InsertManyOptions opts = new InsertManyOptions() { BypassDocumentValidation = true };
        
        WriteLine("Inserting documents...");
        _restaurantsCollection.InsertMany(restaurantsList, opts);
        // end-insert
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
    public string Cuisine { get; set; }
}
// end-model