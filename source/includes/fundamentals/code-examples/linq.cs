
// start-restaurant-model
public class Restaurant
{
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    [BsonElement("restaurant_id")]
    public string RestaurantId { get; set; }

    public string Cuisine { get; set; }

    public Address Address { get; set; }

    public string Borough { get; set; }

    public List<GradeEntry> Grades { get; set; }

}
// end-restaurant-model

// start-address-model
public class Address
{
    public string Building { get; set; }

    [BsonElement("coord")]
    public double[] Coordinates { get; set; }

    public string Street { get; set; }

    [BsonElement("zipcode")]
    public string ZipCode { get; set; }
}
// end-address-model

// start-grade-model
public class GradeEntry
{
    public DateTime Date { get; set; }

    public string Grade { get; set; }

    public float Score { get; set; }
}
// end-grade-model


// start-review-model

public class Review
{
    public ObjectId Id { get; set; }

    [BsonElement("restaurant_name")]
    public string RestaurantName { get; set; }

    public string Reviewer { get; set; }

    [BsonElement("review_text")]
    public string ReviewText { get; set; }
}

// end-review-model

// start-ingredient-model

public class Ingredient 
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Price { get; set; }

    public int Count { get; set; }
}

// end-ingredient-model

// start-bitAnd-example

var query = queryableCollection
    .Where(i => i.Name == "eggs")
    .Select(i => i.Price & i.Count);

// end-bitAnd-example

// start-bitAnd-collection-example

var query = queryableCollection
    .Select(i => i.Price & i.Count);

// end-bitAnd-collection-example

// start-bitOr-example

var query = queryableCollection
    .Where(i => i.Name == "eggs")
    .Select(i => i.Price | i.Count);

// end-bitOr-example

// start-bitNot-example

var queryable = collection
    .Select(i => ~i.Count);

// end-bitNot-example

// start-bitXor-example

var query = queryableCollection
    .Where(i => i.Name == "eggs" || i.Name == "watermelon")
    .Select(i => i.Price ^ i.Count);

// end-bitXor-example