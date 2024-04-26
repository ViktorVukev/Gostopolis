namespace Gostopolis.Restaurants.Models.Restaurants;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;
using Images;
using Menus;
using Products;
using RestaurantTypes;
using Tables;
using WorkingTimes;

public class RestaurantDetailsOutputModel : IMapFrom<Restaurant>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PartnerId { get; set; }

    public int TypeId { get; set; }

    public RestaurantTypeOutputModel Type { get; set; }

    public WorkingTimeOutputModel? WorkingTime { get; set; }

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

    public IEnumerable<ImageOutputModel> Images { get; set; }

    public IEnumerable<ProductDetailsOutputModel> Products { get; set; }

    public IEnumerable<MenuDetailsOutputModel> Menus { get; set; }

    public IEnumerable<TableDetailsOutputModel> Tables { get; set; }

    public IEnumerable<IEnumerable<TableDetailsOutputModel>> TableConfigurations { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Restaurant, RestaurantDetailsOutputModel>()
            .ForMember(a => a.TableConfigurations, cfg => cfg.Ignore());
}
