namespace Gostopolis.Accommodations.Services.Accommodations;

using Models.Accommodations;

/// <summary>
/// Implements basic CRUD operations for accommodations
/// </summary>
public interface IAccommodationService
{
    Task<int> Create(CreateAccommodationInputModel input);

    IEnumerable<AccommodationDetailsOutputModel> GetAll();

    IEnumerable<AccommodationDetailsOutputModel> GetListing(SearchAccommodationsInputModel input);

    IEnumerable<AccommodationDetailsOutputModel> Filter(FilterAccommodationsInputModel input);

    IEnumerable<AccommodationDetailsOutputModel> ByCurrentUser();

    Task<AccommodationDetailsOutputModel> FindById(int id);

    Task<AccommodationDetailsOutputModel> FindById(int id, SearchAccommodationsInputModel input);

    Task<bool> Edit(int id, EditAccommodationInputModel input);

    Task<bool> Approve(int id);

    Task<bool> ChangeVisibility(int id);

    Task<bool> Delete(int id);
}

