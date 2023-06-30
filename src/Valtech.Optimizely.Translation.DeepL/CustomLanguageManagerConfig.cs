using EPiServer.Labs.LanguageManager;
using EPiServer.Labs.LanguageManager.Configuration;
using EPiServer.Labs.LanguageManager.Models;

namespace Valtech.Optimizely.Translation.DeepL;

public class CustomLanguageManagerConfig : LanguageManagerConfig
{
    public CustomLanguageManagerConfig(LanguageManagerOptions languageManagerOptions) : base(languageManagerOptions)
    {
    }
        
    public override ITranslatorProviderConfig TranslatorProviderConfig
    {
        get
        {
            // ActiveTranslatorProvider.ProviderTypeName == $"{typeof(DeepLMachineTranslatorProvider).FullName},{typeof(DeepLMachineTranslatorProvider).Assembly.GetName().Name}"
            if (ActiveTranslatorProvider.ProviderTypeName.StartsWith(typeof(DeepLMachineTranslatorProvider).FullName!, StringComparison.InvariantCulture)) 
            {
                return new DeepLTranslatorProviderConfig
                {
                    SubscriptionKey = ActiveTranslatorProvider.SubscriptionKey
                   
                };
            }
                
            return base.TranslatorProviderConfig;
        }
    }

   
}