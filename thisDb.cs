using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Data.Models;

namespace RecipeKeeper
{
	public class thisDb : DbContext
	{
		public DbSet<Ingredient> Ingredient { get; set; }
		public DbSet<IngredientCategory> IngredientCategory { get; set; }
		public DbSet<Recipe_Ingredient> Recipe_Ingredient { get; set; }
		public DbSet<Recipe> Recipe { get; set; }
		public DbSet<UOM> UOM { get; set; }
		public DbSet<RelatedRecipe> RelatedRecipe { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = WebApplication.CreateBuilder();
			IConfiguration configuration = builder.Configuration;

			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
