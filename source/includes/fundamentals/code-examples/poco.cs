using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestRun.Fundamentals;

public class Builders
{
    private static IMongoCollection<Clothing> _myColl;
    private static string _mongoConnectionString = "<Your MongoDB URI>";
    
    public static void Main(string[] args)
    {
        Setup();
        
        // start-insert
        var doc = new Clothing() 
        { 
            name = "Denim Jacket", 
            instock = false, 
            price = 32.99m, 
            color = new List<string>() {"dark wash", "light wash"}
        };
        
        _myColl.InsertOne(doc);
        // end-insert
    }
    private static void Setup()
    {
        // Establish the connection to MongoDB and get the restaurants database
        var mongoClient = new MongoClient(_mongoConnectionString);
        var myDatabase = mongoClient.GetDatabase("sample_db");
        _myColl = myDatabase.GetCollection<Clothing>("sample_coll");
    }
}

// start-model
public class Clothing
{
    public ObjectId _id { get; set; }
    public string name { get; set; }

    [BsonElement("in_stock")]
    public bool instock { get; set; }
    
    [BsonRepresentation(BsonType.Double)]
    public decimal price { get; set; }
    public List<string> color { get; set; }
}
// end-model