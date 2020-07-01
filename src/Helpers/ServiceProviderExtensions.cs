using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
