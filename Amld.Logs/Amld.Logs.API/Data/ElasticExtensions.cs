using Nest;

namespace Amld.Logs.API.Data
{

    public static class ElasticExtensions
    {
        public static IServiceCollection AddElastic(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IElasticClient>(x => {

                var url = configuration.GetValue<string>("ES.Url");
                var defaultIndex = configuration.GetValue<string>("DefaultIndex");

                var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);
                return new ElasticClient(settings);
            });
            return services;
        }
    }
}
