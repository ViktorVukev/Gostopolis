namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Services.Ingredients;

using static Constants;

/// <summary>
/// Used to display ingredients on forms.
/// </summary>
/// <param name="ingredients">Service interface that keeps ingredients business logic.</param>
public class IngredientsController(
        IIngredientService ingredients) 
    : ApiController
{
    /// <summary>
    /// Gives a list of all ingredients used in products.
    /// </summary>
    /// <returns>List of all ingredients.</returns>
    [HttpGet]
    [Authorize(Roles = PartnerRoleName)]
    public Result<IEnumerable<string>> All()
        => Result<IEnumerable<string>>.SuccessWith(ingredients.GetAll());
}
