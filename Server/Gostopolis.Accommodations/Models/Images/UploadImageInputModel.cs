namespace Gostopolis.Accommodations.Models.Images;

public class UploadImageInputModel
{
    public string ImageBase64 { get; set; }

    public int AccommodationId { get; set; }
}
