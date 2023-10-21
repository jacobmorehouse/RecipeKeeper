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
			//var dt = new DataTable();

            //List<Ingredient> ingredients = new List<Ingredient>();
			//using (var conn = new SqlConnection(new SqlConnector().connectionString))
			//{
			//	string sqlStatement = @"select i.Id, i.Name, i.Description, i.Vegetarian, i.Vegan, ic.name as IngredientCategoryName, 
			//							case 
			//								when IngredientUsed.ingredientId is null then 0
			//								else 1
			//							end as IsUsed

			//							from Ingredient i
			//							left join ingredientCategory ic on i.IngredientCategoryId = ic.Id
			//							left join (
			//								select distinct ingredientId 
			//								from Recipe_Ingredient
			//							) IngredientUsed on i.Id = IngredientUsed.ingredientId
			//							";

			//	var cmd = new SqlCommand(sqlStatement, conn);
			//	conn.Open();
				
			//	SqlDataAdapter da = new SqlDataAdapter(cmd);
			//	da.Fill(dt);
			//	ViewData["dt"] = dt;

			//	conn.Close();
			//}



		}
    }
}
