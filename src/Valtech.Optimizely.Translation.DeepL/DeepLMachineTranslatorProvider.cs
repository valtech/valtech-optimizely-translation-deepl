using DeepL;
using DeepL.Model;
using EPiServer.Labs.LanguageManager.Business.Providers;
using EPiServer.Labs.LanguageManager.Models;
using EPiServer.Logging;

namespace Valtech.Optimizely.Translation.DeepL;

internal class DeepLMachineTranslatorProvider : IMachineTranslatorProvider
{
    private readonly ILogger _logger = LogManager.GetLogger(typeof(DeepLMachineTranslatorProvider));
    private DeepLTranslatorProviderConfig? Config { get; set; }
    
    public bool Initialize(ITranslatorProviderConfig config)
    {
        if (config is not DeepLTranslatorProviderConfig deepLConfig)
        {
            return false;
        }
        
        Config = deepLConfig;
        return true;
    }

    public TranslateTextResult Translate(string inputText, string fromLang, string toLang)
    {
        if (string.IsNullOrEmpty(Config?.SubscriptionKey))
        {
            _logger.Error("DeepL Authentication Key is required.");
            
            return new TranslateTextResult { IsSuccess = false };
        }
        
        var translator = new Translator(Config.SubscriptionKey);

        TextResult textResult;

        try
        {
            textResult = Task.Run(() => translator.TranslateTextAsync(inputText, fromLang, toLang)).Result;
        }
        catch (AggregateException aggregateException)
        {
            foreach (var exception in aggregateException.Flatten().InnerExceptions)
            {
                _logger.Error($"Cannot translate with DeepL, input={inputText}, fromLang={fromLang}, toLang={toLang}",
                    exception);
            }
            
            return new TranslateTextResult { IsSuccess = false };
        }
        catch (Exception exception)
        {
            _logger.Error($"Cannot translate with DeepL, input={inputText}, fromLang={fromLang}, toLang={toLang}",
                exception);

            return new TranslateTextResult { IsSuccess = false };
        }

        return new TranslateTextResult { IsSuccess = true, Text = textResult.Text };
    }

    public string DisplayName => "DeepL";
}
