using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Pages
{
	public class ViewRecipeModel : PageModel
	{
		public RKContext _db;

		public ViewRecipeModel(RKContext db) { 
			_db = db;
		}

		public void OnGet(int RecipeId)
		{
			Recipe rec = (from r in _db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes).Include(r => r.RecipeCategory)
						  where r.Id == RecipeId
						  select r).FirstOrDefault();

			ViewData["thisRecipe"] = rec;
			ViewData["IngredientList"] = _db.Ingredient.ToList();
			ViewData["UOMList"] = _db.UOM.ToList();
			ViewData["RecipeList"] = _db.Recipe.ToList();
		}
	}
}
