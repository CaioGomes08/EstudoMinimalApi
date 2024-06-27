using Dapper;
using System.Data.SqlClient;

namespace MinimalAPI.Package.Module
{
	public class PackageModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/package", async (SqlConnection conn) =>
				await conn.QueryAsync("SELECT * FROM package"));

			app.MapGet("/package/{code}", async (SqlConnection conn, string code) =>
				await conn.QueryFirstAsync($"SELECT * FROM package WHERE code = @code", new {code}));

			app.MapPost("/package", async (SqlConnection conn, RegisterPackageDTO packageDto) =>
			{
				var newPackage = await conn.QueryFirstOrDefaultAsync<RegisterPackageDTO>(
						@"INSERT INTO package(code, country, description)
                          OUTPUT INSERTED.*
						  VALUES(@code, @country, @description)", packageDto
					);

				return Results.Created($"/package/{newPackage.PackageId}", newPackage);
			});
		}
	}
}
