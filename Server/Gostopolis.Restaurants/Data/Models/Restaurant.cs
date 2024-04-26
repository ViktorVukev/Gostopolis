namespace Gostopolis.Restaurants.Data.Models;
public class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PartnerId { get; set; }

    public int TypeId { get; set; }

    public RestaurantType Type { get; set; }
    
    public WorkingTime? WorkingTime { get; set; }

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

    public ICollection<Image> Images { get; set; } = new List<Image>();

    public ICollection<Table> Tables { get; set; } = new List<Table>();

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

    //public ICollection<Review> Reviews { get; set; } = new List<Review>();


    //public ICollection<Order> Orders { get; set; } = new List<Order>();


}