// Find all documents with a "make" value of "Fender"
var results = _guitarsCollection.Find(g => g.Make == "Fender").ToList();

foreach (var doc in results)
{
    // Print the document in bson (json) format
    Console.WriteLine(doc.ToBsonDocument());
}