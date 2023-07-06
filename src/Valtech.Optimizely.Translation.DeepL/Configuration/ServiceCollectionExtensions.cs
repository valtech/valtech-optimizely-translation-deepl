using EPiServer.Labs.LanguageManager.Business.Providers;
using EPiServer.Labs.LanguageManager.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Valtech.Optimizely.Translation.DeepL.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeepLTranslation(this IServiceCollection services)
    {
        services.AddSingleton<IMachineTranslatorProvider, DeepLMachineTranslatorProvider>();
        services.AddSingleton<ILanguageManagerConfig, CustomLanguageManagerConfig>();

        return services;
    }
}