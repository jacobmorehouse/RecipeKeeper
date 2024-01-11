using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecipeKeeper.Data.Models;
using System.Data;
using System.Xml.Linq;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class IngredientsModel : PageModel
	{
		//public thisDb db = new thisDb();

		public RKContext _db;

		public IngredientsModel(RKContext db)
		{
			_db = db;
		}

		public void OnGet()
		{
			ViewData["IngredientList"] = _db.Ingredient.ToList();
			ViewData["IngredientIdsUsed"] = (from iu in _db.Recipe_Ingredient select iu.IngredientId).Distinct().ToList();

		}

		public IActionResult OnPostDelete()
		{
			var ingredientId = int.Parse(Request.Form["ingredientId"]);

			var thisIngredient = (from i in _db.Ingredient
								  where i.Id == ingredientId
								  select i).FirstOrDefault();

			if (thisIngredient != null)
			{
				_db.Ingredient.Remove(thisIngredient);
				_db.SaveChanges();
			}

			return RedirectToPage("Ingredients");
		}
	}
}
