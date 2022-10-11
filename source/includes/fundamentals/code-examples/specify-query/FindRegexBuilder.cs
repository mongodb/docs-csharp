var filter = Builders<Guitar>.Filter.Regex("make", "^G");
var result = _guitarsCollection.Find(filter).ToList();

foreach (var doc in result)
{
    WriteLine(doc.ToBsonDocument());
}