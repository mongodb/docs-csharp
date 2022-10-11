var results = _guitarsCollection.Find(r => r.EstablishedYear >= 1985 && r.Make != "Kiesel").ToList();

foreach (var doc in results)
{
    WriteLine(doc.ToBsonDocument());
}