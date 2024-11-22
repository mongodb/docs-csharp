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

    public static UpdateResult UpdateOnePush()
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

    public static async Task<UpdateResult> UpdateOnePushAsync()
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

    public static UpdateResult UpdateManyPush()
    {
        // start-update-many-push
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .Push(restaurant => restaurant.Grades, new GradeEntry()
            {
                Date = DateTime.Now,
                Grade = "A",
                Score = 96
            });

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-push
    }

    public static async Task<UpdateResult> UpdateManyPushAsync()
    {
        // start-update-many-push-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .Push(restaurant => restaurant.Grades, new GradeEntry()
            {
                Date = DateTime.Now,
                Grade = "A",
                Score = 96
            });

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-push-async
    }

    public static UpdateResult UpdateOneAddToSet()
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

    public static async Task<UpdateResult> UpdateOneAddToSetAsync()
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

    public static UpdateResult UpdateManyAddToSet()
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

    public static async Task<UpdateResult> UpdateManyAddToSetAsync()
    {
        // start-update-many-addtoset-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var firstGradeEntry = _restaurantsCollection.Find(filter).FirstOrDefault().Grades[0];

        var update = Builders<Restaurant>.Update
            .AddToSet(restaurant => restaurant.Grades, firstGradeEntry);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-addtoset-async
    }

    public static UpdateResult UpdateManyPushEach()
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

    public static async Task<UpdateResult> UpdateManyPushEachAsync()
    {
        // start-update-many-pusheach-async
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

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-pusheach-async
    }

    public static UpdateResult UpdateOnePushEach()
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

    public static async Task<UpdateResult> UpdateOnePushEachAsync()
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

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-pusheach-async
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

    public static UpdateResult UpdateOneAddToSetEach()
    {
        // start-update-one-addtoseteach
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var doc = _restaurantsCollection.Find(filter).FirstOrDefault();
        var firstGradeEntries = new List<GradeEntry> { doc.Grades[0], doc.Grades[1] };

        var update = Builders<Restaurant>.Update
            .AddToSetEach(restaurant => restaurant.Grades, firstGradeEntries);

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-addtoseteach
    }

    public static async Task<UpdateResult> UpdateOneAddToSetEachAsync()
    {
        // start-update-one-addtoseteach-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var doc = _restaurantsCollection.Find(filter).FirstOrDefault();
        var firstGradeEntries = new List<GradeEntry> { doc.Grades[0], doc.Grades[1] };

        var update = Builders<Restaurant>.Update
            .AddToSetEach(restaurant => restaurant.Grades, firstGradeEntries);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-addtoseteach-async
    }

    public static UpdateResult UpdateManyAddToSetEach()
    {
        // start-update-many-addtoseteach
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var doc = _restaurantsCollection.Find(filter).FirstOrDefault();
        var firstGradeEntries = new List<GradeEntry> { doc.Grades[0], doc.Grades[1] };

        var update = Builders<Restaurant>.Update
            .AddToSetEach(restaurant => restaurant.Grades, firstGradeEntries);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-addtoseteach
    }

    public static async Task<UpdateResult> UpdateManyAddToSetEachAsync()
    {
        // start-update-many-addtoseteach-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var doc = _restaurantsCollection.Find(filter).FirstOrDefault();
        var firstGradeEntries = new List<GradeEntry> { doc.Grades[0], doc.Grades[1] };

        var update = Builders<Restaurant>.Update
            .AddToSetEach(restaurant => restaurant.Grades, firstGradeEntries);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-addtoseteach-async
    }

    public static UpdateResult UpdateOnePopFirst()
    {
        // start-update-one-popfirst
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopFirst(restaurant => restaurant.Grades);

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-popfirst
    }

    public static async Task<UpdateResult> UpdateOnePopFirstAsync()
    {
        // start-update-one-popfirst-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopFirst(restaurant => restaurant.Grades);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-popfirst-async
    }

    public static UpdateResult UpdateManyPopFirst()
    {
        // start-update-many-popfirst
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopFirst(restaurant => restaurant.Grades);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-popfirst
    }

    public static async Task<UpdateResult> UpdateManyPopFirstAsync()
    {
        // start-update-many-popfirst-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopFirst(restaurant => restaurant.Grades);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-popfirst-async
    }

    public static UpdateResult UpdateOnePopLast()
    {
        // start-update-one-poplast
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopLast(restaurant => restaurant.Grades);

        var result = _restaurantsCollection.UpdateOne(filter, update);

        return result;
        // end-update-one-poplast
    }

    public static async Task<UpdateResult> UpdateOnePopLastAsync()
    {
        // start-update-one-poplast-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopLast(restaurant => restaurant.Grades);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, update);

        return result;
        // end-update-one-poplast-async
    }

    public static UpdateResult UpdateManyPopLast()
    {
        // start-update-many-poplast
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopLast(restaurant => restaurant.Grades);

        var result = _restaurantsCollection.UpdateMany(filter, update);

        return result;
        // end-update-many-poplast
    }

    public static async Task<UpdateResult> UpdateManyPopLastAsync()
    {
        // start-update-many-poplast-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        var update = Builders<Restaurant>.Update
            .PopLast(restaurant => restaurant.Grades);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, update);

        return result;
        // end-update-many-poplast-async
    }

    public static UpdateResult UpdateOnePull()
    {
        // start-update-one-pull
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateOne(filter, addUpdate);

        // Remove duplicates from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .Pull(restaurant => restaurant.Grades, newGrades[0]);

        var result = _restaurantsCollection.UpdateOne(filter, pullUpdate);

        return result;
        // end-update-one-pull
    }

    public static async Task<UpdateResult> UpdateOnePullAsync()
    {
        // start-update-one-pull-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateOneAsync(filter, addUpdate);

        // Remove duplicates from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .Pull(restaurant => restaurant.Grades, newGrades[0]);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, pullUpdate);

        return result;
        // end-update-one-pull-async
    }

    public static UpdateResult UpdateManyPull()
    {
        // start-update-many-pull
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateMany(filter, addUpdate);

        // Remove duplicates from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .Pull(restaurant => restaurant.Grades, newGrades[0]);

        var result = _restaurantsCollection.UpdateMany(filter, pullUpdate);

        return result;
        // end-update-many-pull
    }

    public static async Task<UpdateResult> UpdateManyPullAsync()
    {
        // start-update-many-pull-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateManyAsync(filter, addUpdate);

        // Remove duplicates from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .Pull(restaurant => restaurant.Grades, newGrades[0]);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, pullUpdate);

        return result;
        // end-update-many-pull-async
    }

    public static UpdateResult UpdateOnePullAll()
    {
        // start-update-one-pullall
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,},
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateOne(filter, addUpdate);

        // Remove duplicates from Grades array
        var gradesToRemove = new List<GradeEntry> { newGrades[0], newGrades[2] };
        var pullUpdate = Builders<Restaurant>.Update
            .PullAll(restaurant => restaurant.Grades, gradesToRemove);

        var result = _restaurantsCollection.UpdateOne(filter, pullUpdate);

        return result;
        // end-update-one-pullall
    }

    public static async Task<UpdateResult> UpdateOnePullAllAsync()
    {
        // start-update-one-pullall-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,},
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateOneAsync(filter, addUpdate);

        // Remove duplicates from Grades array
        var gradesToRemove = new List<GradeEntry> { newGrades[0], newGrades[2] };
        var pullUpdate = Builders<Restaurant>.Update
            .PullAll(restaurant => restaurant.Grades, gradesToRemove);

        var result = await _restaurantsCollection.UpdateOneAsync(filter, pullUpdate);

        return result;
        // end-update-one-pullall-async
    }

    public static UpdateResult UpdateManyPullAll()
    {
        // start-update-many-pullall
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,},
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateMany(filter, addUpdate);

        // Remove duplicates from Grades array
        var gradesToRemove = new List<GradeEntry> { newGrades[0], newGrades[2] };
        var pullUpdate = Builders<Restaurant>.Update
            .PullAll(restaurant => restaurant.Grades, gradesToRemove);

        var result = _restaurantsCollection.UpdateMany(filter, pullUpdate);

        return result;
        // end-update-many-pullall
    }

    public static async Task<UpdateResult> UpdateManyPullAllAsync()
    {
        // start-update-many-pullall-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add duplicate values to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "A", Score = 95,},
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85 },
            new GradeEntry { Date = DateTime.MinValue, Grade = "B", Score = 85,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateManyAsync(filter, addUpdate);

        // Remove duplicates from Grades array
        var gradesToRemove = new List<GradeEntry> { newGrades[0], newGrades[2] };
        var pullUpdate = Builders<Restaurant>.Update
            .PullAll(restaurant => restaurant.Grades, gradesToRemove);

        var result = await _restaurantsCollection.UpdateManyAsync(filter, pullUpdate);

        return result;
        // end-update-many-pullall-async
    }

    public static UpdateResult UpdateOnePullFilter()
    {
        // start-update-one-pullfilter
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add GradeEntry values with "Grade = F" to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 10 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 21,},
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 47 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 6,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateOne(filter, addUpdate);

        // Remove all "Grade = F" values from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .PullFilter(restaurant => restaurant.Grades, gradeEntry => gradeEntry.Grade == "F");

        var result = _restaurantsCollection.UpdateOne(filter, pullUpdate);

        return result;
        // end-update-one-pullfilter
    }

    public static async Task<UpdateResult> UpdateOnePullFilterAsync()
    {
        // start-update-one-pullfilter-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add GradeEntry values with "Grade = F" to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 10 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 21,},
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 47 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 6,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateOneAsync(filter, addUpdate);

        // Remove all "Grade = F" values from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .PullFilter(restaurant => restaurant.Grades, gradeEntry => gradeEntry.Grade == "F");

        var result = await _restaurantsCollection.UpdateOneAsync(filter, pullUpdate);

        return result;
        // end-update-one-pullfilter-async
    }

    public static UpdateResult UpdateManyPullFilter()
    {
        // start-update-many-pullfilter
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add GradeEntry values with "Grade = F" to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 10 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 21,},
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 47 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 6,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        _restaurantsCollection.UpdateMany(filter, addUpdate);

        // Remove all "Grade = F" values from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .PullFilter(restaurant => restaurant.Grades, gradeEntry => gradeEntry.Grade == "F");

        var result = _restaurantsCollection.UpdateMany(filter, pullUpdate);

        return result;
        // end-update-many-pullfilter
    }

    public static async Task<UpdateResult> UpdateManyPullFilterAsync()
    {
        // start-update-many-pullfilter-async
        var filter = Builders<Restaurant>.Filter
            .Eq("name", "Downtown Deli");

        // Add GradeEntry values with "Grade = F" to Grades array
        var newGrades = new List<GradeEntry>
        {
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 10 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 21,},
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 47 },
            new GradeEntry { Date = DateTime.Now, Grade = "F", Score = 6,}
        };
        var addUpdate = Builders<Restaurant>.Update
            .PushEach("Grades", newGrades);
        await _restaurantsCollection.UpdateManyAsync(filter, addUpdate);

        // Remove all "Grade = F" values from Grades array
        var pullUpdate = Builders<Restaurant>.Update
            .PullFilter(restaurant => restaurant.Grades, gradeEntry => gradeEntry.Grade == "F");

        var result = await _restaurantsCollection.UpdateManyAsync(filter, pullUpdate);

        return result;
        // end-update-many-pullfilter-async
    }
}