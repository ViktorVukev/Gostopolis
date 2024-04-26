namespace Gostopolis.Data.Interfaces;

/// <summary>
/// Used to unite room and table.
/// </summary>
public interface IIterable
{
    /// <summary>
    /// The capacity of table or room.
    /// </summary>
    public int Capacity { get; set; }
}
