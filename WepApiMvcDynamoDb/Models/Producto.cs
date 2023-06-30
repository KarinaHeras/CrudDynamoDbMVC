using Amazon.DynamoDBv2.DataModel;

namespace WepApiMvcDynamoDb.Models
{
    [DynamoDBTable("Product")]
    public class Producto
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("Name")]
        public string Name { get; set; }

        [DynamoDBProperty("Price")]
        public decimal Price { get; set; }

        [DynamoDBProperty("Stock")]
        public int Stock { get; set; }

        [DynamoDBProperty("Providers")]
        public string Providers { get; set; }
    }
}
