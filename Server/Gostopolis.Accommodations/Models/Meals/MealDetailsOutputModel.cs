namespace Gostopolis.Accommodations.Models.Meals;

using AutoMapper;
using Data.Models;
using Gostopolis.Models;

public class MealDetailsOutputModel : IMapFrom<Meal>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AccommodationId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Meal, MealDetailsOutputModel>();
}
