namespace DefaultNamespace;

public interface IEntity
{
    string Id { get; set; }
}

public abstract class EntityBase : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
}

public class Customer : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

public class Order : EntityBase
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}