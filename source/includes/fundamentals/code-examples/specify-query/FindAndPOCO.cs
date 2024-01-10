// Finds all documents with am "establishedYear" value greater than 1985
// and a "make" value of "Kiesel"
var results = _guitarsCollection.Find(g => g.EstablishedYear >= 1985 && r.Make != "Kiesel").ToList();

foreach (var doc in results)
{
    // Prints the document in bson (json) format
    Console.WriteLine(doc.ToBsonDocument());
}