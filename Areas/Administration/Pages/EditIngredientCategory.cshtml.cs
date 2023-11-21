using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class EditIngredientCategoryModel : PageModel
	{
		//public List<IngredientCategory> thisCategory = new List<IngredientCategory>(); // used to pass the category for Get and Delete. If not found, count will be 0.
		//public IngredientCategory thisCategory = new IngredientCategory();

		[BindProperty]
		public IngredientCategory thisCategory { get; set; }

		public void OnGet(int IngredientCategoryID)
		{
			var db = new thisDb();
			var IngredientCategory = (from a in db.IngredientCategory
									where a.Id == IngredientCategoryID
									select a).FirstOrDefault();
									
			//TODO if IC not found, instead redirect the user to the IC menu page.

			this.thisCategory = IngredientCategory;
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				//TODO if ID not found, do nothing and just go back to the IC menu page. 
				var db = new thisDb();
				IngredientCategory icInDB = (from ic in db.IngredientCategory
											 where ic.Id == thisCategory.Id
											 select ic).FirstOrDefault();

				icInDB.Name = thisCategory.Name;
				db.SaveChanges();

			}
			return RedirectToPage("IngredientCategories");

		}

	}
}
