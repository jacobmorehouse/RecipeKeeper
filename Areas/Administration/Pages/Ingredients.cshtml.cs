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
		public void OnGet()
		{
		}

		public IActionResult OnPostDelete()
		{
			var db = new thisDb();
			var ingredientId = int.Parse(Request.Form["ingredientId"]);

			var thisIngredient = (from i in db.Ingredient
								  where i.Id == ingredientId
								  select i).FirstOrDefault();

			if (thisIngredient != null)
			{
				db.Ingredient.Remove(thisIngredient);
				db.SaveChanges();
			}

			return RedirectToPage("Ingredients");
		}
	}
}
