namespace Gostopolis.Admin.Models.Accommodations;

using Images;
using Users;

public class AccommodationDetailsOutputModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PartnerId { get; set; }

    public UserDetailsOutputModel Partner { get; set; }

    public int TypeId { get; set; }

    public AccommodationTypeOutputModel Type { get; set; }

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

    public bool IsVerified { get; set; }

    public IEnumerable<ImageOutputModel> Images { get; set; }

    //public IEnumerable<RoomDetailsOutputModel> Rooms { get; set; }
}
