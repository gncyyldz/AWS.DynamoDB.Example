using Amazon.DynamoDBv2.DataModel;

namespace AWS.DynamoDB.Example.Models
{
    [DynamoDBTable("employees")]
    public class Employee
    {
        [DynamoDBHashKey("id")]
        public int? Id { get; set; }

        [DynamoDBProperty("name")]
        public string? Name { get; set; }

        [DynamoDBProperty("surname")]
        public string? Surname { get; set; }

        [DynamoDBProperty("birth_date")]
        public DateTime? BirthDate { get; set; }
    }
}
