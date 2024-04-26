namespace Gostopolis.Restaurants.Models.Images;

public class UploadImageInputModel
{
    public string ImageBase64 { get; set; }

    public int RestaurantId { get; set; }
}
