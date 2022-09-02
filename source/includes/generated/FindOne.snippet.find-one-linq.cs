var query = _moviesCollection.AsQueryable()
    .Where(m => m.Title == "The Great Train Robbery")
    .Select(m => new {m.Title, m.Year, m.Cast, m.Genres});
    
Console.WriteLine(query.ToBsonDocument());
