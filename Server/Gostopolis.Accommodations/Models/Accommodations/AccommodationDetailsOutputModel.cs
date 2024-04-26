namespace Gostopolis.Accommodations.Models.Accommodations;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;
using Images;
using Meals;
using Reservations;
using Rooms;

public class AccommodationDetailsOutputModel : IMapFrom<Accommodation>
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

    public bool IsVerified { get; set; }

    public bool IsTemporaryClosed { get; set; }

    public decimal? CheapestOption { get; set; }

    public string? Facilities { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public IEnumerable<ImageOutputModel> Images { get; set; }

    public IEnumerable<MealDetailsOutputModel> Meals { get; set; }

    public IEnumerable<RoomDetailsOutputModel> Rooms { get; set; }

    public IEnumerable<IEnumerable<RoomDetailsOutputModel>> RoomConfigurations { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Accommodation, AccommodationDetailsOutputModel>()
            .ForMember(a => a.CheapestOption, cfg => cfg.Ignore())
            .ForMember(a => a.RoomConfigurations, cfg => cfg.Ignore());
}
