namespace Gostopolis.Admin.Services.Accommodations;

using Models.Accommodations;
using Models.Restaurants;
using Refit;

public interface IAccommodationService
{
    [Get("/Accommodations/{id}")]
    Task<AccommodationDetailsOutputModel> Details(int id);

    [Get("/Accommodations/All")]
    Task<IEnumerable<AccommodationDetailsOutputModel>> All();

    [Post("/AccommodationTypes")]
    Task<int> CreateAccommodationType(AccommodationTypeInputModel model);

    [Get("/AccommodationTypes")]
    Task<IEnumerable<AccommodationTypeOutputModel>> AccommodationTypes();

    [Put("/AccommodationTypes/{id}")]
    Task<bool> EditAccommodationType(int id, AccommodationTypeInputModel model);

    [Delete("/AccommodationTypes/{id}")]
    Task DeleteAccommodationType(int id);

    [Put("/Accommodations/Approve/{id}")]
    Task Approve(int id);

    [Delete("/Accommodations/{id}")]
    Task Delete(int id);
}
