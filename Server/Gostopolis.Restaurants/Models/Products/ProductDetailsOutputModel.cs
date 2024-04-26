namespace Gostopolis.Restaurants.Models.Products;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;

public class ProductDetailsOutputModel : IMapFrom<Product>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public double Weight { get; set; }

    public decimal Price { get; set; }

    public int RestaurantId { get; set; }

    public int? MenuId { get; set; }

    public ProductType Type { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Product, ProductDetailsOutputModel>();
}
