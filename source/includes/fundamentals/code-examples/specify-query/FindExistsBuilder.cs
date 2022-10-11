var filter = Builders<Guitar>.Filter.Exists("rating");
var result = _guitarsCollection.Find(filter).ToList();

foreach (var doc in result)
{
    WriteLine(doc.ToBsonDocument());
}