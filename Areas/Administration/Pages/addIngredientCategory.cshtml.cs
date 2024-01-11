using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecipeKeeper.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class addIngredientCategoryModel : PageModel
	{
		[BindProperty]
		public IngredientCategory newIngredientCategory { get; set; }
		public RKContext _db;

		public addIngredientCategoryModel(RKContext db) { 
			_db = db;
		}

		public void OnGet()
		{
		}

		public IActionResult OnPost() {
			if (ModelState.IsValid) {

				newIngredientCategory.AddedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
				newIngredientCategory.AddedDateTimeUTC = DateTime.UtcNow;

				_db.IngredientCategory.Add(newIngredientCategory);
				_db.SaveChanges();

				return RedirectToPage("IngredientCategories");
			} else {
				return Page();
			}

		}
	}
}
