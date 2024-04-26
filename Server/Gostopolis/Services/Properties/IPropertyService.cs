namespace Gostopolis.Services.Properties;

using Data.Interfaces;

public interface IPropertyService
{
    public List<string> GetAccommodatingCombinations(IEnumerable<IIterable> objects, int people, int objCount);
}
