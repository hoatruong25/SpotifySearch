namespace SpotifySearchAPI.Registrations;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistrations(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
    
        // Register all services (interfaces ending with 'Service' and their implementations)
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Service"))
            .ToList();
    
        foreach (var serviceType in serviceTypes)
        {
            var implementationType = assembly.GetTypes()
                .FirstOrDefault(t => !t.IsInterface && !t.IsAbstract && serviceType.IsAssignableFrom(t));
        
            if (implementationType != null)
            {
                services.AddScoped(serviceType, implementationType);
            }
        }
    
        // Register all repositories (interfaces ending with 'Repository' and their implementations)
        var repositoryTypes = assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
            .ToList();
    
        foreach (var repositoryType in repositoryTypes)
        {
            var implementationType = assembly.GetTypes()
                .FirstOrDefault(t => !t.IsInterface && !t.IsAbstract && repositoryType.IsAssignableFrom(t));
        
            if (implementationType != null)
            {
                services.AddScoped(repositoryType, implementationType);
            }
        }
        return services;
    }
}