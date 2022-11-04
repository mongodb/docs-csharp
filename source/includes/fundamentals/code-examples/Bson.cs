using MongoDB.Driver;

namespace Fundamentals;

public class Bson
{
    public static void Main(string[] args)
    {
        //start-change
        //start-create
        var newRestaurant = new BsonDocument
        {
            { "address", new BsonDocument
                {
                    { "street", "Pizza St" },
                    { "zipcode", "10003" }
                }
            },
            { "coord", new BsonArray
                {-73.982419, 41.579505 }
            },
            new BsonElement("cuisine", "Pizza"),
            new BsonElement("name", "Mongo's Pizza")
        };
        //end-create
        newRestaurant.Add(new BsonElement("restaurant_id", "12345"));
        newRestaurant.Remove("cuisine");
        newRestaurant.Set("name", "Mongo's Pizza Palace");
        //end-change
    }
}