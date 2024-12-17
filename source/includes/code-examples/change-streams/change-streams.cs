// start-open-change-stream
var database = client.GetDatabase("sample_restaurants");
var collection = database.GetCollection<Restaurant>("restaurants");

// Opens a change stream and prints the changes as they're received
using (var cursor = collection.Watch())
{
    foreach (var change in cursor.ToEnumerable())
    {
        Console.WriteLine("Received the following type of change: " + change.BackingDocument);
    }
}
// end-open-change-stream

// start-open-change-stream-async
var database = client.GetDatabase("sample_restaurants");
var collection = database.GetCollection<Restaurant>("restaurants");

// Opens a change streams and print the changes as they're received
using var cursor = await collection.WatchAsync();
await cursor.ForEachAsync(change =>
{
    Console.WriteLine("Received the following type of change: " + change.BackingDocument);
});
// end-open-change-stream-async

// start-modify-document
var database = client.GetDatabase("sample_restaurants");
var collection = database.GetCollection<Restaurant>("restaurants");

var filter = Builders<Restaurant>.Filter
    .Eq(restaurant => restaurant.Name, "Blarney Castle");

var update = Builders<Restaurant>.Update
    .Set(restaurant => restaurant.Cuisine, "Irish");

var result = collection.UpdateOne(filter, update);
// end-modify-document

// start-change-stream-pipeline-async
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Update);

// Opens a change stream and prints the changes as they're received
using (var cursor = await collection.WatchAsync(pipeline))
{
    await cursor.ForEachAsync(change =>
    {
        Console.WriteLine("Received the following change: " + change);
    });
}
// end-change-stream-pipeline-async

// start-change-stream-pipeline
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Update);

// Opens a change streams and print the changes as they're received
using (var cursor = collection.Watch(pipeline))
{
    foreach (var change in cursor.ToEnumerable())
    {
        Console.WriteLine("Received the following change: " + change);
    }
}
// end-change-stream-pipeline

// start-split-event-helpers-sync
// Fetches the next complete change stream event
private static ChangeStreamDocument<TDocument> GetNextChangeStreamEvent<TDocument>(
    IEnumerator<ChangeStreamDocument<TDocument>> changeStreamEnumerator)
{
    var changeStreamEvent = changeStreamEnumerator.Current;

    // Reassembles change event fragments if the event is split
    if (changeStreamEvent.SplitEvent != null)
    {
        var fragment = changeStreamEvent;
        while (fragment.SplitEvent.Fragment < fragment.SplitEvent.Of)
        {
            changeStreamEnumerator.MoveNext();
            fragment = changeStreamEnumerator.Current;
            MergeFragment(changeStreamEvent, fragment);
        }
    }
    return changeStreamEvent;
}

// Merges a fragment into the base event
private static void MergeFragment<TDocument>(
    ChangeStreamDocument<TDocument> changeStreamEvent,
    ChangeStreamDocument<TDocument> fragment)
{
    foreach (var element in fragment.BackingDocument)
    {
        if (element.Name != "_id" && element.Name != "splitEvent")
        {
            changeStreamEvent.BackingDocument[element.Name] = element.Value; 
        }
    }
}
// end-split-event-helpers-sync

// start-split-event-helpers-async
// Fetches the next complete change stream event
private static async Task<ChangeStreamDocument<TDocument>> GetNextChangeStreamEvent<TDocument>(
    IAsyncCursor<ChangeStreamDocument<TDocument>> changeStreamCursor)
{
    var changeStreamEvent = changeStreamCursor.Current.First();

    // Reassembles change event fragments if the event is split
    if (changeStreamEvent.SplitEvent != null)
    {
        var fragment = changeStreamEvent;
        while (fragment.SplitEvent.Fragment < fragment.SplitEvent.Of)
        {
            if (!await changeStreamCursor.MoveNextAsync())
            {
                throw new InvalidOperationException("Incomplete split event fragments.");
            }
            fragment = changeStreamCursor.Current.First();
            MergeFragment(changeStreamEvent, fragment);
        }
    }
    return changeStreamEvent;
}

// Merges a fragment into the base event
private static void MergeFragment<TDocument>(
    ChangeStreamDocument<TDocument> changeStreamEvent,
    ChangeStreamDocument<TDocument> fragment)
{
    foreach (var element in fragment.BackingDocument)
    {
        if (element.Name != "_id" && element.Name != "splitEvent")
        {
            changeStreamEvent.BackingDocument[element.Name] = element.Value;
        }
    }
}
// end-split-event-helpers-async

// start-split-change-event-async
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
    .ChangeStreamSplitLargeEvent();

using (var cursor = await collection.WatchAsync(pipeline))
{
    while (await cursor.MoveNextAsync())
    {
        foreach (var changeStreamEvent in cursor.Current)
        {
            var completeEvent = await GetNextChangeStreamEvent(cursor);
            Console.WriteLine("Received the following change: " + completeEvent.BackingDocument);
        }
    }
}
// end-split-change-event-async

// start-split-change-event-sync
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
    .ChangeStreamSplitLargeEvent();

using (var cursor = collection.Watch(pipeline))
{
    using (var enumerator = cursor.ToEnumerable().GetEnumerator())
    {
        while (enumerator.MoveNext())
        {
            var completeEvent = GetNextChangeStreamEvent(enumerator);
            Console.WriteLine("Received the following change: " + completeEvent.BackingDocument);
        }
    }
}
// end-split-change-event-sync

// start-change-stream-post-image
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
    .Match(change => change.OperationType == ChangeStreamOperationType.Update);

var options = new ChangeStreamOptions
{
    FullDocument = ChangeStreamFullDocumentOption.UpdateLookup,
};

using (var cursor = collection.Watch(pipeline, options))
{
    foreach (var change in cursor.ToEnumerable())
    {
        Console.WriteLine(change.FullDocument.ToBsonDocument());
    }
}
// end-change-stream-post-image

// start-change-stream-post-image-async
var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Restaurant>>()
    .Match(change => change.OperationType == ChangeStreamOperationType.Update);

var options = new ChangeStreamOptions
{
    FullDocument = ChangeStreamFullDocumentOption.UpdateLookup,
};

using var cursor = await collection.WatchAsync(pipeline, options);
await cursor.ForEachAsync(change =>
{
    Console.WriteLine(change.FullDocument.ToBsonDocument());
});
// end-change-stream-post-image-async