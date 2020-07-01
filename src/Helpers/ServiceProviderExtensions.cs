using Microsoft.Extensions.DependencyInjection;
using Uption.Services;

namespace Uption.Helpers
{
    public static class ServiceProviderExtensions
    {
        public static void AddTelegramService(this IServiceCollection services)
        {
            services.AddSingleton<TelegramService>();
        }
    }
}
