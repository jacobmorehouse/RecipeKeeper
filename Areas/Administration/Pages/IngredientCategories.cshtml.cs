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

		//Was using this for trying to implement dependancy injection on thisDB
		//public thisDb _db;
		//
		//public IngredientCategoriesModel(thisDb db)
		//{
		//	_db = db;
		//}


		public thisDb db = new thisDb();

		public void OnGet()
		{
		}

		public IActionResult OnPostDelete() {
			
			var icid = int.Parse(Request.Form["icid"]);



			var thisIC = (from n in db.IngredientCategory
							where n.Id == icid
							select n).FirstOrDefault();

			if (thisIC != null) {
				db.IngredientCategory.Remove(thisIC);
				db.SaveChanges();
			}
				
			return RedirectToPage("IngredientCategories");
		}

    }
}
