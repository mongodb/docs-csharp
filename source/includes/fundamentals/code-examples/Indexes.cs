using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.GeoJsonObjectModel;

public class Indexes
{

    public static void Main(string[] args)
    {   
        // begin-declaration
        // Replace with your connection string
        const string uri = "<YOUR_CONNECTION_STRING>";

        var mongoClient = new MongoClient(uri);
        var database = mongoClient.GetDatabase("sample_mflix");
        var movieCollection = database.GetCollection<Movie>("movies");
        var theaterCollection = database.GetCollection<Theater>("theaters");
        // end-declaration

        SingleIndex(movieCollection);
        CompoundIndex(movieCollection);
        MultiKeyIndex(movieCollection);
        TextIndex(movieCollection);
        GeoSpatialIndex(movieCollection);
        UniqueIndex(theaterCollection);
        WildcardIndex(theaterCollection);
    }

    private static void SingleIndex(IMongoCollection<Movie> collection)
    {
        Console.WriteLine("single index");

        // begin-single-index
        collection.Indexes.CreateOne("{ title : 1 }");
        // end-single-index

        // begin-single-index-query
        // Define query parameters
        var filter = Builders<Movie>.Filter.Eq("title", "Batman");
        var sort = Builders<Movie>.Sort.Ascending("title");
        var projection = Builders<Movie>.Projection.Include("title").Exclude("_id");

        // Execute query
        var results = collection.Find(filter).Sort(sort).Project(projection);
        // end-single-index-query
    }

    private static void CompoundIndex(IMongoCollection<Movie> collection)
    {
        Console.WriteLine("compound index");

        // begin-compound-index
        collection.Indexes.CreateOne("{ type : 1, rated: 1 }");
        // end-compound-index

        // begin-compound-index-query
        // Define query parameters
        var typeFilter = Builders<Movie>.Filter.Eq("type", "movie");
        var ratedFilter = Builders<Movie>.Filter.Eq("rated", "G");
        var filter = Builders<Movie>.Filter.And(typeFilter, ratedFilter);
        var sort = Builders<Movie>.Sort.Ascending("{ type : 1, rated: 1 }");
        var projection = Builders<Movie>.Projection.Include("type").Include("rated").Exclude("_id");

        // Execute query
        var results = collection.Find(filter).Sort(sort).Project(projection);
        // end-compound-index-query
    }

    private static void MultiKeyIndex(IMongoCollection<Movie> collection)
    {
        Console.WriteLine("multi-key index");

        // begin-multi-key-index
        collection.Indexes.CreateOne("{ rated: 1, genres: 1, title: 1 }");
        // end-multi-key-index

        // begin-multi-key-query
        // Define query parameters
        var genreFilter = Builders<Movie>.Filter.Eq("genres", "Animation");
        var ratedFilter = Builders<Movie>.Filter.Eq("rated", "G");
        var filter = Builders<Movie>.Filter.And(genreFilter, ratedFilter);
        var sort = Builders<Movie>.Sort.Ascending("title");
        var projection = Builders<Movie>.Projection.Include("title").Include("rated").Exclude("_id");

        // Execute query
        var results = collection.Find(filter).Sort(sort).Project(projection);
        // end-multi-key-query
    }

    private static void TextIndex(IMongoCollection<Movie> collection)
    {
        Console.WriteLine("text index");

        try
        {
            // begin-text-index
            collection.Indexes.CreateOne("{ plot : \"text\" }");
            // end-text-index
        }
        // Prints a message if a text index already exists with a different configuration 
        catch (MongoCommandException e)
        {
            if (e.CodeName == "IndexOptionsConflict")
            {
                Console.WriteLine("There is an existing text index with different options");
            }
        }

        // begin-text-query
        // Define query parameters
        var filter = Builders<Movie>.Filter.Text("java coffee shop");
        var projection = Builders<Movie>.Projection.Include("plot").Exclude("_id");

        // Execute query
        var results = collection.Find(filter).Project(projection);
        // end-text-query
    }

    private static void GeoSpatialIndex(IMongoCollection<Movie> collection)
    {
        Console.WriteLine("geospatial index");

        try
        {
            // begin-geospatial-index
            collection.Indexes.CreateOne("{ \"location.geo\": \"2dsphere\" }");
            // end-geospatial-index
        }
        // Prints a message if a geospatial index already exists with a different configuration 
        catch (MongoCommandException e)
        {
            if (e.CodeName == "IndexOptionsConflict")
            {
                Console.WriteLine("There is an existing geospatial index with different options");
            }
        }

        // begin-geospatial-query
        // Stores the coordinates of the NY MongoDB headquarters
        var refPoint = GeoJson.Point(GeoJson.Position(-73.98456, 40.7612));

        // Creates a filter to match documents that represent locations up to 1000 meters from the specified point directly from the geospatial index
        var filter = Builders<Movie>.Filter.Near("location.geo", refPoint, 1000.0, 0.0);

        // Execute the query
        var results = collection.Find(filter);
        // end-geospatial-query
    }

    private static void UniqueIndex(IMongoCollection<Theater> collection)
    {
        Console.WriteLine("unique index");

        // begin-unique-index
        var options = new CreateIndexOptions { Unique = true };
        collection.Indexes.CreateOne("{ theaterId : -1 }", options);
        // end-unique-index
    }

    private static void WildcardIndex(IMongoCollection<Theater> collection)
    {
        Console.WriteLine("wildcard index");

        // begin-wildcard-index
        collection.Indexes.CreateOne("{ \"location.$**\" : 1 }");
        // end-wildcard-index
    }

    private static void ListIndexes(IMongoCollection<Movie> collection)
    {
        // begin-list-indexes
        var indexes = collection.Indexes.List();

        foreach (var index in indexes.ToList())
        {
            Console.WriteLine(index);
        }
        // end-list-indexes
    }

    public class Movie
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("rated")]
        public string Rated { get; set; }

        [BsonElement("genres")]
        public List<string> Genres { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("plot")]
        public string Plot { get; set; }

    }

    public class Theater
    {
        [BsonElement("theaterId")]
        public string TheaterId { get; set; }
    }

    public class Vendor
    {
        [BsonId]
        public string Id { get; set; }
    }
}

