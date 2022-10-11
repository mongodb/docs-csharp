var results = _guitarsCollection.Find(r => r.EstablishedYear > 1985).ToList();

foreach (var doc in results)
{
    WriteLine(doc.ToBsonDocument());
}