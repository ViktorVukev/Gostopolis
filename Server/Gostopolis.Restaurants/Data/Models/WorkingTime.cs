namespace Gostopolis.Restaurants.Data.Models;

public class WorkingTime
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }
    
    public TimeOnly? MondayOpenTime { get; set; }

    public TimeOnly? MondayCloseTime { get; set; }

    public TimeOnly? TuesdayOpenTime { get; set; }

    public TimeOnly? TuesdayCloseTime { get; set; }
    
    public TimeOnly? WednesdayOpenTime { get; set; }

    public TimeOnly? WednesdayCloseTime { get; set; }
    
    public TimeOnly? ThursdayOpenTime { get; set; }

    public TimeOnly? ThursdayCloseTime { get; set; }
    
    public TimeOnly? FridayOpenTime { get; set; }

    public TimeOnly? FridayCloseTime { get; set; }
    
    public TimeOnly? SaturdayOpenTime { get; set; }

    public TimeOnly? SaturdayCloseTime { get; set; }
    
    public TimeOnly? SundayOpenTime { get; set; }

    public TimeOnly? SundayCloseTime { get; set; }
}
