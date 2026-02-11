using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnClickInvest.Api.Shared.Extensions;

namespace OnClickInvest.Api.Shared.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtAuth(configuration);
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
