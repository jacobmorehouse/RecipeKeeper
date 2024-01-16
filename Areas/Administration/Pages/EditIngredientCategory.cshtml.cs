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
		public RKContext _db;


		public EditIngredientCategoryModel(RKContext db) { 
			_db = db;
		}

		public void OnGet(int IngredientCategoryID)
		{
			var IngredientCategory = (from a in _db.IngredientCategory
									where a.Id == IngredientCategoryID
									select a).FirstOrDefault();
									
			//TODO if IC not found, instead redirect the user to the IC menu page.

			this.thisCategory = IngredientCategory;
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid)
			{
				//TODO if ID not found, do nothing and just go back to the IC menu page. 
				IngredientCategory icInDB = (from ic in _db.IngredientCategory
											 where ic.Id == thisCategory.Id
											 select ic).FirstOrDefault();

				icInDB.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				icInDB.UpdatedDateUTC = DateTime.UtcNow;

				icInDB.Name = thisCategory.Name;
				_db.SaveChanges();
				return RedirectToPage("IngredientCategories");
			}
			else { 
				return Page();
			}
		}
	}
}
