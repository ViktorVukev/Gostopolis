namespace Gostopolis.Admin.Models.Restaurants;

using Images;
using RestaurantTypes;
using Users;

public class RestaurantDetailsOutputModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PartnerId { get; set; }

    public UserDetailsOutputModel Partner { get; set; }

    public int TypeId { get; set; }

    public RestaurantTypeOutputModel Type { get; set; }

    public string IdentificationNumber { get; set; }

    public string OwnershipDocumentUrl { get; set; }

    public string CoverPhotoUrl { get; set; }

    public string Address { get; set; }

    public int Stars { get; set; }

    public bool HasParking { get; set; }

    public bool HasPosTerminal { get; set; }

    public bool AcceptOnlinePayments { get; set; }

    public bool AcceptPets { get; set; }

    public string SpokenLanguages { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public IEnumerable<ImageOutputModel> Images { get; set; }

    public bool IsVerified { get; set; }
}
