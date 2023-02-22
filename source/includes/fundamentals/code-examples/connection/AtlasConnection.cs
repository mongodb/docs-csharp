using MongoDB.Driver;

// Replace the placeholders with your credentials
const string connectionUri = "mongodb+srv://<username>:<password>@cluster0.sample.mongodb.net/?retryWrites=true&w=majority";

var settings = MongoClientSettings.FromConnectionString(connectionString);

// Use the ServerAPI() method to set Stable API version 1
var serverApi = new ServerApi(ServerApiVersion.V1);
settings.ServerApi = serverApi;

// Create a new client and connect to the server
var client = new MongoClient(settings);