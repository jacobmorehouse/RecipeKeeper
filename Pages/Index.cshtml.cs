using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;
using System.Collections.Generic;

namespace RecipeKeeper.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		[BindProperty]
		public int? recipeType { get; set; }

		[BindProperty]
		public string? searchText { get; set; }



		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
		}

		public IActionResult OnPost() {
			var db = new thisDb();
			List<Recipe> AllRecipes = (db.Recipe.Include(r => r.Ingredients).Include(r => r.RelatedRecipes).Include(r => r.RecipeCategory)).ToList();
			List<Recipe> RecipesFilteredByName = new List<Recipe>();
			List<Recipe> RecipesFilteredByIngredients = new List<Recipe>();
			List<Recipe> RecipesFilteredByType = new List<Recipe>();
			List<Recipe> ReturnRecipes = new List<Recipe>();


			//if type was defined, filter for it.
			if (AllRecipes.Count > 0 && recipeType != null) {
				RecipesFilteredByType = (from r in AllRecipes
										 where r.RecipeCategory.Id == recipeType
						   select r).ToList();
			}


			//here is the name search logic
			if (RecipesFilteredByType.Count > 0 && searchText != null && searchText.Length > 0)
			{
				RecipesFilteredByName = (from r in RecipesFilteredByType
										 where r.Name.ToLower().Contains(searchText.ToLower())
						   select r).ToList();
			}

			//Here is the ingredient search logic
			if (RecipesFilteredByType.Count > 0 && searchText != null && searchText.Length > 0) {

				//ingredients we should be looking out for: 
				var matchingIngredients = (from i in db.Ingredient
										   where i.Name.ToLower().Contains(searchText.ToLower())
										   select i.Id).ToList();

				//If we had any ingredients match AND still had some recipes, we need to initiate that search as well...
				if (matchingIngredients.Count > 0)
				{
					List<int> recipeMatches = new List<int>();
					foreach (int mi in matchingIngredients)
					{
						foreach (var r in RecipesFilteredByType)
						{
							foreach (var ri in r.Ingredients)
							{
								if (ri.IngredientId == mi)
								{
									//check if recipe in recipeMatches list. if it is not, add it.
									if (recipeMatches.Contains(r.Id) == false) { recipeMatches.Add(r.Id); }
								}
							}
						}
					}

					//if we had any recipeMatches, add them to the main recipes list that we'll display if they aren't already there...
					if (recipeMatches.Count > 0)
					{
						foreach (int rm in recipeMatches)
						{
							//if recipeMatch is not in recipes, add it. 
							Recipe thisRecipe = (from tr in RecipesFilteredByType
												 where tr.Id == rm
												 select tr).FirstOrDefault();

							if (RecipesFilteredByIngredients.Contains(thisRecipe) == false)
							{
								RecipesFilteredByIngredients.Add(thisRecipe);
							}
						}
					}
				}
			}

			//Combine the hits into one response...
			if (searchText != null && searchText.Length > 0)
			{
				if (RecipesFilteredByName.Count > 0)
				{
					foreach (var r in RecipesFilteredByName)
					{
						if (ReturnRecipes.Contains(r) == false)
						{
							ReturnRecipes.AddRange(RecipesFilteredByName);
						}
					}
				}

				if (RecipesFilteredByIngredients.Count > 0)
				{
					foreach (var r in RecipesFilteredByIngredients)
					{
						if (ReturnRecipes.Contains(r) == false)
						{
							ReturnRecipes.Add(r);
						}
					}
				}
			}
			else {
				ReturnRecipes.AddRange(RecipesFilteredByType);
			}



			ViewData["RecipeSearchResults"] = ReturnRecipes;
			return Page();
		}
	}
}