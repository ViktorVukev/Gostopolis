namespace Gostopolis.Restaurants.Data.Models;

public class Table
{
    public int Id { get; set; }

    public string Number { get; set; }

    public int Capacity { get; set; }

    public bool IsSmokingAllowed { get; set; }

    public bool IsOutdoor { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }

    public IList<ReservationTable> ReservationTables { get; set; } = new List<ReservationTable>();

    //public ICollection<Order> Orders { get; set; } = new List<Order>();
}
