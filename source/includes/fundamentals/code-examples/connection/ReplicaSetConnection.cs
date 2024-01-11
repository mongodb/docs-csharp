// Connects to a specific replica set by using a URI

// start replica set connection
using MongoDB.Driver;

// Sets the connection URI
const string connectionUri = "mongodb://sample.host1:27017/?replicaSet=sampleRS";

// Creates a new client and connects to the server
var client = new MongoClient(connectionUri);
// start replica set connection