using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeKeeper.Data.Models;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class EditIngredientCategoryModel : PageModel
	{

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

				icInDB.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				icInDB.UpdatedDateUTC = DateTime.UtcNow;

				icInDB.Name = thisCategory.Name;
				db.SaveChanges();

			}
			return RedirectToPage("IngredientCategories");

		}

	}
}
