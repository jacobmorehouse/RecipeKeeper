using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class EditIngredientModel : PageModel
	{
		[BindProperty]
		public Ingredient thisIngredient { get; set; }

		public void OnGet(int IngredientID)
		{
			var db = new thisDb();
			Ingredient ing = (from a in db.Ingredient
									  where a.Id == IngredientID
									  select a).FirstOrDefault();

			//TODO if IC not found, instead redirect the user to the ingredient menu page.

			this.thisIngredient = ing;
		}


		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				//TODO if ID not found, do nothing and just go back to the Ingredient menu page. 
				var db = new thisDb();
				Ingredient ingInDB = (from ic in db.Ingredient
										where ic.Id == thisIngredient.Id
										select ic).FirstOrDefault();


				ingInDB.Name = thisIngredient.Name;
				ingInDB.Description = thisIngredient.Description;
				ingInDB.IngredientCategoryId = thisIngredient.IngredientCategoryId;
				ingInDB.Vegetarian = thisIngredient.Vegetarian;
				ingInDB.Vegan = thisIngredient.Vegan;

				ingInDB.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				ingInDB.UpdatedDateUTC = DateTime.UtcNow;

				db.SaveChanges();
			}


			return RedirectToPage("Ingredients");
		}

	}
}
