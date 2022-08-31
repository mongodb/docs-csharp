using MongoDB.Driver;

// Connection URI
const string connectionUri = "mongodb://user1:password1@sample.host:27017/?maxPoolSize=20&w=majority";

// Create a new client and connect to the server
var client = new MongoClient(connectionUri);

// Print hostname and port (sample.host:27017)
Console.WriteLine(client.Settings.Server);
Console.ReadKey();