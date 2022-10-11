var results = _guitarsCollection.Find(r => r.Make == "Fender").ToList();

foreach (var doc in results)
{
    WriteLine(doc.ToBsonDocument());
}