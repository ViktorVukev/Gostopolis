namespace Gostopolis.Accommodations.Controllers;

using Gostopolis.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Meals;
using Services.Meal;

public class MealsController(
        IMealService meals)
    : ApiController
{
    [HttpPost]
    [Authorize(Roles = "Partner")]
    public async Task<ActionResult<int>> Create(CreateMealInputModel input)
        => await meals.Create(input);

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<MealDetailsOutputModel>> Details(int id)
        => await meals.FindById(id);

    [HttpGet]
    [Route(nameof(ByAccommodationId))]
    public IEnumerable<MealDetailsOutputModel> ByAccommodationId(int id)
        => meals.ByAccommodationId(id);

    //[HttpPut]
    //[Route(Id)]
    //[Authorize(Roles = "Partner")]
    //public async Task<ActionResult> Edit(int id, EditUserInputModel input)
    //{
    //    var restaurant = await restaurants.FindById(id);

    //    if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
    //    {
    //        return BadRequest("You are not allowed to edit this property.");
    //    }

    //    return Ok(await restaurants.Edit(id, input));
    //}

    [HttpDelete]
    [Route(Id)]
    [Authorize(Roles = "Partner,Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await meals.Delete(id));
    }
}
