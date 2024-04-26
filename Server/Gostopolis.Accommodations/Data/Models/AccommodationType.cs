namespace Gostopolis.Accommodations.Data.Models;

public class AccommodationType
{
    public AccommodationType(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public AccommodationType(int id, string name, string description) 
        : this(name, description)
    {
        this.Id = id;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
}