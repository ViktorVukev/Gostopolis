namespace Gostopolis.Restaurants.Data.Models;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int TableId { get; set; }

    public Table Table { get; set; }

    public decimal TotalPrice { get; set; }

    public bool IsConfirmed { get; set; }

    //public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}