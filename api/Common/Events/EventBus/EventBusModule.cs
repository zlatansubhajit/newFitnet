using newFitnet.Common.Events.EventBus.InMemory;
using System.Reflection;

namespace newFitnet.Common.Events.EventBus
{
    internal static class EventBusModule
    {
        internal static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
