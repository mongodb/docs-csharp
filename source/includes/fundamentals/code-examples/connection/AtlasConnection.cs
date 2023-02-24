using MongoDB.Driver;
using MongoDB.Bson;

// Replace the placeholders with your credentials
const string connectionUri = "mongodb+srv://<username>:<password>@cluster0.sample.mongodb.net/?retryWrites=true&w=majority";

var settings = MongoClientSettings.FromConnectionString(connectionString);

// Set the ServerApi field of the settings object to Stable API version 1
var serverApi = new ServerApi(ServerApiVersion.V1);
settings.ServerApi = serverApi;

// Create a new client and connect to the server
var client = new MongoClient(settings);

// Send a ping to confirm a successful connection
var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
Console.WriteLine(result.ToJson());