namespace BACampusApp.DataAccess.Extentesions;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BACampusAppDbContext>(options =>
        {
			options.UseLazyLoadingProxies();
            options.UseSqlServer(configuration.GetConnectionString(BACampusAppDbContext.ConnectionName));
        });
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<BACampusAppDbContext>().AddDefaultTokenProviders();




        return services;
    }
}
