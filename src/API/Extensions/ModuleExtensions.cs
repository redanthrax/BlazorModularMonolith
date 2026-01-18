using System.Reflection;
using Common.Application.Abstractions;
using Wolverine;

namespace API.Extensions;

public static class ModuleExtensions {
    public static IServiceCollection AddModules(this IServiceCollection services,
        IConfiguration configuration) {
        var moduleType = typeof(IModuleInitializer);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName?.Contains("API.Modules") == true);

        foreach (var assembly in assemblies) {
            var modules = assembly.GetTypes()
                .Where(t => moduleType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var module in modules) {
                var instance = Activator.CreateInstance(module) as IModuleInitializer;
                instance?.RegisterServices(services);
            }
        }

        return services;
    }

    public static void ConfigureModules(this WolverineOptions options) {
        var moduleType = typeof(IModuleInitializer);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName?.Contains("API.Modules") == true);

        foreach (var assembly in assemblies) {
            var modules = assembly.GetTypes()
                .Where(t => moduleType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var module in modules) {
                var instance = Activator.CreateInstance(module) as IModuleInitializer;
                instance?.ConfigureWolverine(options);
            }
        }
    }
}
