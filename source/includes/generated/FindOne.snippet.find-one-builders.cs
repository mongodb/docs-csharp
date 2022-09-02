var filter = Builders<Movie>.Filter
    .Eq("title", "The Great Train Robbery");

var projection = Builders<Movie>.Projection
    .Include(m => m.Title)
    .Include(m => m.Year)
    .Include(m => m.Cast)
    .Include(m => m.Genres)
    .Exclude(m => m.Id);

var movie = _moviesCollection.Find(filter).Project(projection).First();
    
Console.WriteLine(movie);
