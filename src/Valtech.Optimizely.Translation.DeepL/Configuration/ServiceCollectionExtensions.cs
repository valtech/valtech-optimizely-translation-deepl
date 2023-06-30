using EPiServer.Labs.LanguageManager.Business.Providers;
using EPiServer.Labs.LanguageManager.Configuration;
using EPiServer.Labs.LanguageManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Valtech.Optimizely.Translation.DeepL.Presentation;
using Valtech.Optimizely.Translation.DeepL.Presentation.Interface;

namespace Valtech.Optimizely.Translation.DeepL.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeepLTranslation(this IServiceCollection services)
    {
        services.AddSingleton<IMachineTranslatorProvider, DeepLMachineTranslatorProvider>();
        services.AddSingleton<ILanguageManagerConfig, CustomLanguageManagerConfig>();
        services.AddSingleton<IDeepLDetailViewModelBuilder, DeepLDetailViewModelBuilder>();

        return services;
    }
}