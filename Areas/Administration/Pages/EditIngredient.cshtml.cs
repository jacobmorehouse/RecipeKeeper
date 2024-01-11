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
		public RKContext _db;

		public EditIngredientModel(RKContext db) { 
			_db = db;
		}


		public void OnGet(int IngredientID)
		{
			Ingredient ing = (from a in _db.Ingredient
									  where a.Id == IngredientID
									  select a).FirstOrDefault();

			//TODO if IC not found, instead redirect the user to the ingredient menu page.

			this.thisIngredient = ing;

			ViewData["IngredientCategoryList"] = _db.IngredientCategory.ToList(); 
		}


		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				//TODO if ID not found, do nothing and just go back to the Ingredient menu page. 
				Ingredient ingInDB = (from ic in _db.Ingredient
										where ic.Id == thisIngredient.Id
										select ic).FirstOrDefault();

				ingInDB.Name = thisIngredient.Name;
				ingInDB.Description = thisIngredient.Description;
				ingInDB.IngredientCategoryId = thisIngredient.IngredientCategoryId;
				ingInDB.Vegetarian = thisIngredient.Vegetarian;
				ingInDB.Vegan = thisIngredient.Vegan;

				ingInDB.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				ingInDB.UpdatedDateUTC = DateTime.UtcNow;

				_db.SaveChanges();
			}

			return RedirectToPage("Ingredients");
		}
	}
}
