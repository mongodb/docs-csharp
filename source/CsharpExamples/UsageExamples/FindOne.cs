// :snippet-start: find-one-full-example
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CsharpExamples.UsageExamples;

public class FindOne
{
    static IMongoCollection<Movie> _moviesCollection;
    // :replace-start: {
    // "terms": {
    //   "mongodb+srv://jordansmith:mongodb@samplecluster.fkmp2h0.mongodb.net/?retryWrites=true&w=majority": "<Your Mongodb URI>"
    //   }
    // }
    static string mongoConnectionString = "mongodb+srv://jordansmith:mongodb@samplecluster.fkmp2h0.mongodb.net/?retryWrites=true&w=majority";
    // :replace-end:
    public static void Main(string[] args)
    {
        setup();
            
        // Find one document using builders
        FindOneMovieBuilder();
        
        // Extra space for console readability 
        Console.WriteLine();
        
        // Find one document using LINQ
        FindOneMovieLINQ();
    }

    static void FindOneMovieBuilder()
    {
        // :snippet-start: find-one-builders
        var filter = Builders<Movie>.Filter
            .Eq("title", "The Great Train Robbery");

        var projection = Builders<Movie>.Projection
            .Include(m => m.Title)
            .Include(m => m.Year)
            .Include(m => m.Cast)
            .Include(m => m.Genres)
            .Exclude(m => m.Id);

        var movie = _moviesCollection.Find(filter).Project(projection).First();
            
        Console.WriteLine(movie);
        // :snippet-end:
    }

    static void FindOneMovieLINQ()
    {
        // :snippet-start: find-one-linq
        var query = _moviesCollection.AsQueryable()
            .Where(m => m.Title == "The Great Train Robbery")
            .Select(m => new {m.Title, m.Year, m.Cast, m.Genres});
            
        Console.WriteLine(query.ToBsonDocument());
        // :snippet-end:
    }

    static void setup()
    {
        // Because our data is stored in the database with their names in camelCase
        // we use these lines to convert the names to TitleCase
        var camelCaseConvention = new ConventionPack {new CamelCaseElementNameConvention()};
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);
        
        // Establish the connection to MongoDB and get the movies database
        var mongoUri = mongoConnectionString;
        var mflixClient = new MongoClient(mongoUri);    
        var moviesDatabase = mflixClient.GetDatabase("sample_mflix");
        _moviesCollection = moviesDatabase.GetCollection<Movie>("movies");
    }
}

public class Movie
{
    public ObjectId Id { get; set; }

    public string Title { get; set; }

    public object Year { get; set; }

    public List<string> Cast { get; set; }

    public List<string> Genres { get; set; }

    // Extra elements from the database unused in this example
    [BsonExtraElements]
    public BsonDocument ExtraElements { get; set; }
}
// :snippet-end:
