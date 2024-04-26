namespace Gostopolis.Accommodations.Services.AccommodationType;

using System.Net.Mime;
using Data.Models;
using Gostopolis.Services;
using Models.AccommodationTypes;

public interface IAccommodationTypeService : IDataService<AccommodationType>
{
    Task<int> Create(AccommodationTypeInputModel input);

    Task<IEnumerable<AccommodationTypeOutputModel>> GetAll();

    Task<AccommodationType> FindById(int id);

    Task<bool> Edit(int id, AccommodationTypeInputModel input);

    Task<bool> Delete(int id);
}
