using Microsoft.Extensions.Configuration;

namespace Community.Extensions.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Binds option class to configuration section
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}