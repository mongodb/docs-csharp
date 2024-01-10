// Creates a filter for all documents with an "establishedYear" greater than 1985
var filter = Builders<Guitar>.Filter.Gt(g => g.EstablishedYear, 1985);

// Find all documents that match the filter
var result = _guitarsCollection.Find(filter).ToList();

foreach (var doc in result)
{
    // Prints a document in bson (json) format
    Console.WriteLine(doc.ToBsonDocument());
}