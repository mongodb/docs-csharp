using System;

class BulkWrite
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        BulkWrite
    }

    static void InsertOne()
    {
        // start-bulk-insert-one
        var insertOneModel = new BulkWriteInsertOneModel<BsonDocument>(
            "sample_restaurants.restaurants",
            new BsonDocument{
                { "name", "Mongo's Deli" },
                { "cuisine", "Sandwiches" },
                { "borough", "Manhattan" },
                { "restaurant_id", "1234" }
            }
        );
        // end-bulk-insert-one
    }

    static void UpdateOne()
    {
        // start-bulk-update-one
        var updateOneModel = new BulkWriteUpdateOneModel<BsonDocument>(
            "sample_restaurants.restaurants",
            Builders<BsonDocument>.Filter.Eq("name", "Mongo's Deli"),
            Builders<BsonDocument>.Update.Set("cuisine", "Sandwiches and Salads")
        );
        // end-bulk-update-one
    }

    static void UpdateMany()
    {
        // start-bulk-update-many
        var updateManyModel = new BulkWriteUpdateManyModel<BsonDocument>(
            "sample_restaurants.restaurants",
            Builders<BsonDocument>.Filter.Eq("name", "Mongo's Deli"),
            Builders<BsonDocument>.Update.Set("cuisine", "Sandwiches and Salads")
        );
        // end-bulk-update-many
    }


# start-bulk-replace-one
    operation = pymongo.ReplaceOne(
    { "restaurant_id": "1234" },
    {
    "name": "Mongo's Pizza",
        "cuisine": "Pizza",
        "borough": "Brooklyn",
        "restaurant_id": "5678"
    }
)
# end-bulk-replace-one

# start-bulk-delete-one
operation = pymongo.DeleteOne({ "restaurant_id": "5678" })
# end-bulk-delete-one

# start-bulk-delete-many
operation = pymongo.DeleteMany({ "name": "Mongo's Deli" })
# end-bulk-delete-many

# start-bulk-write-mixed
operations = [
    pymongo.InsertOne(
        {
    "name": "Mongo's Deli",
            "cuisine": "Sandwiches",
            "borough": "Manhattan",
            "restaurant_id": "1234"
        }
    ),
    pymongo.InsertOne(
        {
    "name": "Mongo's Deli",
            "cuisine": "Sandwiches",
            "borough": "Brooklyn",
            "restaurant_id": "5678"
        }
    ),
    pymongo.UpdateMany(
        { "name": "Mongo's Deli" },
        { "$set": { "cuisine": "Sandwiches and Salads" } },
    ),
    pymongo.DeleteOne(
        { "restaurant_id": "1234" }
    )
]

results = restaurants.bulk_write(operations)

print(results)
# end-bulk-write-mixed

# start-bulk-write-unordered
results = restaurants.bulk_write(operations, ordered = False)
# end-bulk-write-unordered