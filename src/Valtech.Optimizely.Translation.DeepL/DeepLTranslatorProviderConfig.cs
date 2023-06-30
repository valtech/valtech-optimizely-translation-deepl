using EPiServer.Labs.LanguageManager.Models;

namespace Valtech.Optimizely.Translation.DeepL;

internal class DeepLTranslatorProviderConfig : ITranslatorProviderConfig
{
    public string SubscriptionKey { get; set; }

}