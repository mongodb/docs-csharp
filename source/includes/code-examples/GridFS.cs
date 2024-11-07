using System;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

class Program
{
    static async Task Main(string[] args)
    { }

    static void CreateBucket()
    {
        // Initialize MongoDB client
        // start-create-bucket
        var client = new MongoClient("<connection string>");
        var database = client.GetDatabase("db");

        // Creates a GridFS bucket or references an existing one
        var bucket = new GridFSBucket(database);
        // end-create-bucket
    }

    static void CreateCustomBucket()
    {
        // Initialize MongoDB client
        var client = new MongoClient("<connection string>");
        var database = client.GetDatabase("db");

        // Creates or references a GridFS bucket with a custom name
        // start-create-custom-bucket
        var options = new GridFSBucketOptions { BucketName = "myCustomBucket" };
        var customBucket = new GridFSBucket(database, options);
        // end-create-custom-bucket
    }

    static void UploadFile()
    {
        // Uploads a file called "my_file" to the GridFS bucket and writes data to it
        // start-open-upload-stream
        using (var uploader = bucket.OpenUploadStream("my_file", options))
        {
            // ASCII for "HelloWorld"
            byte[] bytes = { 72, 101, 108, 108, 111, 87, 111, 114, 108, 100 };

            for (int i = 0; i < 5; i++)
            {
                uploader.Write(bytes, 0, bytes.Length);
            }

            uploader.Close();
        }
        // end-open-upload-stream
    }

    static void UploadFileWithOptions()
    {
        // Uploads a file called "my_file" to the GridFS bucket and writes data to it
        // start-open-upload-stream-with-options
        var options = new GridFSUploadOptions
        {
            ChunkSizeBytes = 1048576 // 1 MB
        };

        using (var uploader = bucket.OpenUploadStream("my_file", options))
        {
            // ASCII for "HelloWorld"
            byte[] bytes = { 72, 101, 108, 108, 111, 87, 111, 114, 108, 100 };

            for (int i = 0; i < 5; i++)
            {
                uploader.Write(bytes, 0, bytes.Length);
            }

            uploader.Close();
        }
        // end-open-upload-stream-with-options
    }

    static void UploadStream()
    {
        // Uploads data to a stream, then writes the stream to a GridFS file
        using (var fileStream = new FileStream("/path/to/input_file", FileMode.Open, FileAccess.Read))
        {
            bucket.UploadFromStream("new_file", fileStream);
        }
    }

// Prints information about each file in the bucket
{
    var filter = Builders<GridFSFileInfo>.Filter.Empty;
    var files = await bucket.FindAsync(filter);
    await files.ForEachAsync(file =>
    {
        Console.WriteLine(file.ToJson());
    });
}

// Downloads a file from the GridFS bucket by referencing its ObjectId value
{
    var filter = Builders<BsonDocument>.Filter.Eq("filename", "new_file");
    var doc = await database.GetCollection<BsonDocument>("fs.files").Find(filter).FirstOrDefaultAsync();
    var id = doc["_id"].AsObjectId;

    using (var downloader = bucket.OpenDownloadStream(id))
    {
        var buffer = new byte[downloader.FileLength];
        await downloader.ReadAsync(buffer, 0, buffer.Length);
        // Process the buffer as needed
    }
}

// Downloads an entire GridFS file to a download stream
{
    using (var outputFile = new FileStream("/path/to/output_file", FileMode.Create, FileAccess.Write))
    {
        var filter = Builders<BsonDocument>.Filter.Eq("filename", "new_file");
        var doc = await database.GetCollection<BsonDocument>("fs.files").Find(filter).FirstOrDefaultAsync();
        var id = doc["_id"].AsObjectId;

        await bucket.DownloadToStreamAsync(id, outputFile);
    }
}

// Deletes a file from the GridFS bucket with the specified ObjectId
{
    var filter = Builders<BsonDocument>.Filter.Eq("filename", "my_file");
    var doc = await database.GetCollection<BsonDocument>("fs.files").Find(filter).FirstOrDefaultAsync();
    var id = doc["_id"].AsObjectId;

    await bucket.DeleteAsync(id);
}
    }
}
