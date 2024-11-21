using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using WriteData.Models;
using static System.Console;

namespace CSharpExamples.WriteData;

public static class UpdateArrays
{
    private static IMongoCollection<Restaurant> _restaurantsCollection;
    //private static string _mongoConnectionString = "<Your MongoDB URI>";
    private static string _mongoConnectionString =
        "mongodb+srv://mikewoofter:mikewoofter@cluster0.pw0q4.mongodb.net/?retryWrites=true&w=majority";

    public static UpdateResult UpdateOneArrayPush()
    {
        // start-update-one-push
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .Push(restaurant => restaurant.Grades, new GradeEntry()
            {
                Date = DateTime.Now,
                Grade = "A",
                Score = 96
            });

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-push
    }

    public static async Task<UpdateResult> UpdateOneArrayPushAsync()
    {
        // start-update-one-push-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .Push(restaurant => restaurant.Grades, new GradeEntry()
            {
                Date = DateTime.Now,
                Grade = "A",
                Score = 96
            });

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-push-async
    }

    public static UpdateResult UpdateOneArrayAddToSet()
    {
        // start-update-one-addtoset
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-addtoset
    }

    public static async Task<UpdateResult> UpdateOneArrayAddToSetAsync()
    {
        // start-update-one-addtoset-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-addtoset-async
    }

    public static UpdateResult UpdateManyArrayAddToSet()
    {
        // start-update-many-addtoset
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-addtoset
    }

    public static async Task<UpdateResult> UpdateManyArrayAddToSetAsync()
    {
        // start-update-many-addtoset-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-many-addtoset-async
    }

    public static UpdateResult UpdateManyArrayPushEach()
    {
        // start-update-many-pusheach
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.Now, Grade = "B+", Score = 89,}
        };

        var scoreSort = Builders<GradeEntry>.Sort.Descending(g => g.Score);

        var update = Builders<Restaurant>.Update.PushEach(
            "Grades",
            newGrades,
            position: 0,
            sort: scoreSort);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-pusheach
    }

    public static UpdateResult UpdateManyArrayPushEach()
    {
        // start-update-many-pusheach
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.Now, Grade = "B+", Score = 89,}
        };

        var scoreSort = Builders<GradeEntry>.Sort.Descending(g => g.Score);

        var update = Builders<Restaurant>.Update.PushEach(
            "Grades",
            newGrades,
            position: 0,
            sort: scoreSort);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-pusheach
    }

    public static async Task<UpdateResult> UpdateManyArrayPushEachAsync()
    {
        // start-update-many-pusheach-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-many-pusheach-async
    }

    public static UpdateResult UpdateOneArrayPushEach()
    {
        // start-update-one-pusheach
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.Now, Grade = "B+", Score = 89,}
        };

        var scoreSort = Builders<GradeEntry>.Sort.Descending(g => g.Score);

        var update = Builders<Restaurant>.Update.PushEach(
            "Grades",
            newGrades,
            position: 0,
            sort: scoreSort);

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-pusheach
    }
    public static async Task<UpdateResult> UpdateOneArrayPushEachAsync()
    {
        // start-update-one-pusheach-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.Now, Grade = "B+", Score = 89,}
        };

        var scoreSort = Builders<GradeEntry>.Sort.Descending(g => g.Score);

        var update = Builders<Restaurant>.Update.PushEach(
            "Grades",
            newGrades,
            position: 0,
            sort: scoreSort);

        var result = _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-pusheach-async
    }

    public static async Task<UpdateResult> UpdateOneArrayPushEachAsync()
    {
        // start-update-one-addtoset-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-addtoset-async
    }


    // private static void LinqTest()
    // {
    //     var x = _restaurantsCollection.UpdateOne(l => l.Id == another.Id && l.AnArrayMember.Any(l => l.Id == anArrayId),
    //         Builders<Restaurant>.Update.Set(l => l.AnArrayMember.ElementAt(-1).Deleted, true));
    //     
    //     Builders<ReplaceOne.Restaurant>.Update.
    // }

    public static void Setup()
    {
        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        var mongoClient = new MongoClient(_mongoConnectionString);
        var restaurantsDatabase = mongoClient.GetDatabase("sample_restaurants");
        _restaurantsCollection = restaurantsDatabase.GetCollection<Restaurant>("restaurants");
    }

    public static void ResetSampleData()
    {
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "2 Bagels 2 Buns");

        var update = Builders<Restaurant>.Update
            .Set(restaurant => restaurant.Name, "Bagels N Buns");

        _restaurantsCollection.UpdateOne(filter, update);
    }
}