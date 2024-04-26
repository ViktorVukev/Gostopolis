namespace Gostopolis.Accommodations.Data.Models;

public class Accommodation
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PartnerId { get; set; }

    public int TypeId { get; set; }

    public AccommodationType Type { get; set; }
     
    public string IdentificationNumber { get; set; }

    public string OwnershipDocumentUrl { get; set; }

    public string CoverPhotoUrl { get; set; }

    public string Address { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Town { get; set; }

    public string LocationUrl { get; set; }

    public int Stars { get; set; }

    public bool HasParking { get; set; }

    public bool HasPosTerminal { get; set; }

    public bool AcceptOnlinePayments { get; set; }

    public bool AcceptPets { get; set; }

    public string SpokenLanguages { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public bool IsVerified { get; set; } = false;

    public bool IsTemporaryClosed { get; set; } = true;

    public string? Facilities { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public IList<Image> Images { get; set; } = new List<Image>();

    public IList<Meal> Meals { get; set; } = new List<Meal>();

    public IList<Room> Rooms { get; set; } = new List<Room>();

    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();
}