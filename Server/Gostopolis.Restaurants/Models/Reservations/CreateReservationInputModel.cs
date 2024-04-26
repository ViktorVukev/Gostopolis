namespace Gostopolis.Restaurants.Models.Reservations;

public class CreateReservationInputModel
{
    public DateTime StartDate { get; set; }
    
    public string PartnerEmail { get; set; }

    public int Guests { get; set; }

    public string TableIds { get; set; }

    public int RestaurantId { get; set; }
}
