namespace RecipeKeeper
{
	public class SqlConnector
	{
		public string connectionString = "";

		public SqlConnector()
		{
			var builder = WebApplication.CreateBuilder();
			IConfiguration configuration = builder.Configuration;
			this.connectionString = configuration.GetConnectionString("DefaultConnection");
		}
	}
}
