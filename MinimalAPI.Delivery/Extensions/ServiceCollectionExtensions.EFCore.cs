using Microsoft.EntityFrameworkCore;

namespace MinimalAPI.Delivery.Extensions
{
	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<>(opt => opt.UseSqlServer(configuration.GetConnectionString("sql")));
			return services;
		}
	}
}
