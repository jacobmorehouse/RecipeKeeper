using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;
using System.Collections.Immutable;

namespace RecipeKeeper.Areas.Administration.Pages
{
    public class RecipesModel : PageModel
    {
		public RKContext _db;

		public RecipesModel(RKContext db)
		{
			_db = db;
		}

		public void OnGet()
        {
			ViewData["Recipes"] = _db.Recipe.ToList();
        }

		public IActionResult OnPostDelete()
		{
			var recipeId = int.Parse(Request.Form["recipeId"]);

			var thisRecipe = (from i in _db.Recipe
								  where i.Id == recipeId
								  select i).FirstOrDefault();

			if (thisRecipe != null)
			{
				_db.Recipe.Remove(thisRecipe);
				var listOfRecipeIngredients = _db.Recipe_Ingredient.ToList();
				List<Recipe_Ingredient> theseRecipeIngredients = (from ri in listOfRecipeIngredients
																  where ri.RecipeId == recipeId
																  select ri).ToList();

				foreach (Recipe_Ingredient ri in theseRecipeIngredients)
				{
					_db.Recipe_Ingredient.Remove(ri);
				}

				_db.SaveChanges();
			}
			return RedirectToPage("Recipes");
		}
	}
}
