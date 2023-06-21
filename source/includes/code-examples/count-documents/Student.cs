//start-count-guide
var filter = Builders<Student>.Filter.Lt("finalGrade", 80);
      var count = coll.CountDocuments(filter);
      
      Console.WriteLine("Number of documents with a final grade less
      than 80: " + count);
//end-count-guide


//start-student-struct
public class Student {
    public int Id { get; set; }
    public string Name { get; set; }
    public double FinalGrade { get; set; }
}
//end-student-struct

