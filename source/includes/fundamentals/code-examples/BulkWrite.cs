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

    static void ReplaceOne()
    {
        // start-bulk-replace-one
        var replaceOneModel = new BulkWriteReplaceOneModel<BsonDocument>(
            "sample_restaurants.restaurants",
            Builders<BsonDocument>.Filter.Eq("restaurant_id", "1234"),
            new BsonDocument{
                { "name", "Mongo's Pizza" },
                { "cuisine", "Pizza" },
                { "borough", "Brooklyn" },
                { "restaurant_id", "5678" }
            }
        );
        // end-bulk-replace-one
    }

    static void DeleteOne()
    {
        // start-bulk-delete-one
        var deleteOneModel = new BulkWriteDeleteOneModel<BsonDocument>(
            "sample_restaurants.restaurants",
            Builders<BsonDocument>.Filter.Eq("restaurant_id", "5678")
        );
        // end-bulk-delete-one
    }

    static void DeleteMany()
    {
        // start-bulk-delete-many
        var deleteManyModel = new BulkWriteDeleteManyModel<BsonDocument>(
            "sample_restaurants.restaurants",
            Builders<BsonDocument>.Filter.Eq("name", "Mongo's Deli")
        );
        // end-bulk-delete-many
    }

    static void BulkWrite()
    {
        // start-bulk-write-mixed
        var bulkWriteModels = new WriteModel<BsonDocument>[]
        {
            new BulkWriteInsertOneModel<BsonDocument>(
                new BsonDocument{
                    { "name", "Mongo's Deli" },
                    { "cuisine", "Sandwiches" },
                    { "borough", "Manhattan" },
                    { "restaurant_id", "1234" }
                }
            ),
            new BulkWriteInsertOneModel<BsonDocument>(
                new BsonDocument{
                    { "name", "Mongo's Deli" },
                    { "cuisine", "Sandwiches" },
                    { "borough", "Brooklyn" },
                    { "restaurant_id", "5678" }
                }
            ),
            new BulkWriteUpdateManyModel<BsonDocument>(
                Builders<BsonDocument>.Filter.Eq("name", "Mongo's Deli"),
                Builders<BsonDocument>.Update.Set("cuisine", "Sandwiches and Salads")
            ),
            new BulkWriteDeleteOneModel<BsonDocument>(
                Builders<BsonDocument>.Filter.Eq("restaurant_id", "1234")
            )
            // end-bulk-write-mixed
        };

        //results = restaurants.bulk_write(operations)
        Console.WriteLine("Bulk write results: " + results);
    }

}




# start-bulk-write-unordered
results = restaurants.bulk_write(operations, ordered = False)
# end-bulk-write-unordered