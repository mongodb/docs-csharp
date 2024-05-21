using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

public class BookTransaction
{
    // Replace with your connection string
    private const string MongoConnectionString = "<YOUR_CONNECTION_STRING>";

    public static void Main(string[] args)
    {   
        // Establishes the connection to MongoDB and accesses the bookstore database
        var mongoClient = new MongoClient(MongoConnectionString);
        var database = mongoClient.GetDatabase("bookstore");

        // Cleans up the books collection we'll be using
        Setup(database);

        // begin-transaction
        var books = database.GetCollection<Book>("books");
        
        // Begins transaction
        using (var session = mongoClient.StartSession()) {
            session.StartTransaction();

            try {
                // Creates sample data
                var book1 = new Book {
                    Title = "Beloved",
                    Author = "Toni Morrison",
                    InStock = true
                };

                var book2 = new Book {
                    Title = "Sula",
                    Author = "Toni Morrison",
                    InStock = true
                };

                // Inserts sample data
                books.InsertOne(session, book1);
                books.InsertOne(session, book2);

                // Fetches and prints the sample books we added
                var initialBooks = books.Find<Book>(session, Builders<Book>.Filter.Empty)
                                        .ToList();
                Console.WriteLine("Initial Books:");
                foreach (Book b in initialBooks) {
                    Console.WriteLine(
                        String.Format("Title: {0}\tAuthor: {1}\tIn Stock: {2}", 
                                        b.Title, b.Author, b.InStock));
                }
                Console.WriteLine();

                // Updates our "Sula" book to no longer be in stock
                var filter = Builders<Book>.Filter.Eq(b => b.Title, "Sula");
                var update = Builders<Book>.Update.Set(b => b.InStock, false);
                books.UpdateOne(session, filter, update);

                // Commits our transaction
                session.CommitTransaction();
            } catch (Exception e) {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                return;
            }

            // Fetches and prints the books after the updates we made
            var booksAfterCommit = books.Find<Book>(session, Builders<Book>.Filter.Empty)
                                    .ToList();
            Console.WriteLine("Books after Committing Transaction:");
            foreach (Book b in booksAfterCommit) {
                Console.WriteLine(
                    String.Format("Title: {0}\tAuthor: {1}\tIn Stock: {2}", 
                                    b.Title, b.Author, b.InStock));
            }
        }
        // end-transaction
    }

    public static void Setup(IMongoDatabase database) {
        database.DropCollection("books");
        database.CreateCollection("books");
    }
}

public class Book
{
    public ObjectId Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("inStock")]
    public bool InStock { get; set; }
}
