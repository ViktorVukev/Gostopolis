namespace Gostopolis.Accommodations.Models.Accommodations;

public class CreateAccommodationInputModel
{
    public string Name { get; set; }

    public int TypeId { get; set; }

    public string IdentificationNumber { get; set; }

    public string OwnershipDocumentBase64 { get; set; }

    public string CoverPhotoBase64 { get; set; }

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
}
