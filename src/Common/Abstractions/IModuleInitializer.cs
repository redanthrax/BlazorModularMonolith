using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace Common.Abstractions;

public interface IModuleInitializer {
    void RegisterServices(IServiceCollection services);
    void ConfigureWolverine(WolverineOptions options);
}
