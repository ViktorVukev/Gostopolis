using Gostopolis.Accommodations.Data.Models;

namespace Gostopolis.Accommodations.Models.Meals;

public class CreateMealInputModel
{
    public string Name { get; set; }

    public int AccommodationId { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }
}
