namespace Gostopolis.Restaurants.Data.Models;

public class Reservation
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public string ClientId { get; set; }

    public DateTime StartDate { get; set; }

    public int Guests { get; set; }

    public bool IsCancelled { get; set; } = false;

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }

    public IList<ReservationTable> ReservationTables { get; set; } = new List<ReservationTable>();
}