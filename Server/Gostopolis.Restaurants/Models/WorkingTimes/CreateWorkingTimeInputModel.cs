namespace Gostopolis.Restaurants.Models.WorkingTimes;

public class CreateWorkingTimeInputModel
{
    public int RestaurantId { get; set; }

    public string? MondayOpenTime { get; set; }

    public string? MondayCloseTime { get; set; }

    public string? TuesdayOpenTime { get; set; }

    public string? TuesdayCloseTime { get; set; }

    public string? WednesdayOpenTime { get; set; }

    public string? WednesdayCloseTime { get; set; }

    public string? ThursdayOpenTime { get; set; }

    public string? ThursdayCloseTime { get; set; }

    public string? FridayOpenTime { get; set; }

    public string? FridayCloseTime { get; set; }

    public string? SaturdayOpenTime { get; set; }

    public string? SaturdayCloseTime { get; set; }

    public string? SundayOpenTime { get; set; }

    public string? SundayCloseTime { get; set; }
}
