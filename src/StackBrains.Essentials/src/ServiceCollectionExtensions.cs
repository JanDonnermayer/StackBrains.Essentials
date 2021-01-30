
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static Lazy<T> GetRequiredServiceLazy<T>(this IServiceProvider serviceProvider) where T : notnull
        {
            if (serviceProvider is null)
                throw new ArgumentNullException(nameof(serviceProvider));

            return new Lazy<T>(serviceProvider.GetRequiredService<T>);
        }
    }
}
