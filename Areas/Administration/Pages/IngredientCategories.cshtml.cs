using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper.Areas.Administration.Pages
{
	public class IngredientCategoriesModel : PageModel
	{
		public RKContext _db;
		
		public IngredientCategoriesModel(RKContext db)
		{
			_db = db;
		}
		
		public void OnGet()
		{
			ViewData["IngredientCategoryList"] = _db.IngredientCategory.ToList();

			//get list of categories that are in use...
			ViewData["IngredientCategoryIDsInUse"] = (from n in _db.Ingredient select n.IngredientCategoryId).Distinct().ToList();
		}

		public IActionResult OnPostDelete() {
			
			var icid = int.Parse(Request.Form["icid"]);

			var thisIC = (from n in _db.IngredientCategory
							where n.Id == icid
							select n).FirstOrDefault();

			if (thisIC != null) {
				_db.IngredientCategory.Remove(thisIC);
				_db.SaveChanges();
			}
				
			return RedirectToPage("IngredientCategories");
		}
	}
}
