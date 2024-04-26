namespace Gostopolis.Restaurants.Models.Menus;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;
using Products;

public class MenuDetailsOutputModel : IMapFrom<Menu>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int RestaurantId { get; set; }

    public ICollection<ProductDetailsOutputModel> Products { get; set; } = new List<ProductDetailsOutputModel>();

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Menu, MenuDetailsOutputModel>();
}
