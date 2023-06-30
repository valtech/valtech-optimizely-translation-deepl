using DeepL.Model;
using DeepL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Labs.LanguageManager.Configuration;
using EPiServer.Logging;
using Valtech.Optimizely.Translation.DeepL.Presentation.Interface;
using Valtech.Optimizely.Translation.DeepL.Presentation.ViewModels;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Valtech.Optimizely.Translation.DeepL.Presentation
{
    public class DeepLDetailViewModelBuilder : IDeepLDetailViewModelBuilder
    {
        
        private readonly ILogger _logger = LogManager.GetLogger(typeof(DeepLMachineTranslatorProvider));
        public readonly ILanguageManagerConfig LanguageManagerConfig;

        public DeepLDetailViewModelBuilder(ILanguageManagerConfig languageManagerConfig)
        {
            LanguageManagerConfig = languageManagerConfig;
        }


        public DeepLDetailViewModel Build()
        {
            var characterData = CheckCharacterUsageAndLimitsAsync().Result;
            var documentData = CheckDocumentUsageAndLimitsAsync().Result;
            var sourecList = GetSourceLanguageList();
            var targetList = GetTargetLanguageList();


           return new DeepLDetailViewModel{
               CharacterCount = characterData.CountData,
               CharacterLimit = characterData.Limit,
               DocumentCount = documentData.CountData ,
               DocumentLimit= documentData.Limit,
               SourceLanguageList = sourecList,
               TargetLanguageList = targetList
           };
        }

        private Task<UsageLimits> CheckCharacterUsageAndLimitsAsync()
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");

                return Task.FromResult(new UsageLimits { ErrorMsg = "DeepL Authentication Key is required." });
            }


            var translator = new Translator(subscriptionKey);

            Usage usageResult;
            try
            {
                usageResult = Task.Run(() => translator.GetUsageAsync()).Result;
            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.Flatten().InnerExceptions)
                {
                    _logger.Error($"Unable to get account usage  with DeepL", exception);
                }

                return Task.FromResult(new UsageLimits { IsSuccess = false });
            }
            catch (Exception exception)
            {
                _logger.Error($"Unable to get account usage  with DeepL", exception);

                return Task.FromResult(new UsageLimits { IsSuccess = false });
            }

            return Task.FromResult(new UsageLimits
            {
                IsSuccess = true,
                CountData = usageResult.Character.Count.ToString(),
                Limit = usageResult.Character.Limit.ToString(),
            });

        }

        //Check account usage Method
        private Task<UsageLimits> CheckDocumentUsageAndLimitsAsync()
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");

                return Task.FromResult(new UsageLimits { ErrorMsg = "DeepL Authentication Key is required." });
            }

            var translator = new Translator(subscriptionKey);

            Usage usageResult;
            try
            {
                usageResult = Task.Run(() => translator.GetUsageAsync()).Result;
            }
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.Flatten().InnerExceptions)
                {
                    _logger.Error($"Unable to get account usage  with DeepL", exception);
                }

                return Task.FromResult(new UsageLimits { IsSuccess = false });
            }
            catch (Exception exception)
            {
                _logger.Error($"Unable to get account usage  with DeepL", exception);

                return Task.FromResult(new UsageLimits { IsSuccess = false });
            }

            return Task.FromResult(new UsageLimits
            {
                IsSuccess = true,
                CountData = usageResult.Document != null ? usageResult.Document.Count.ToString() : "no docs data found" ,
                Limit = usageResult.Document != null ? usageResult.Document.Limit.ToString() : "no docs limit found",
            });

        }

        ////Listing available languages
        private List<CustomLanguageList> GetTargetLanguageList()
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");

                return new List<CustomLanguageList>();
            }

            var translator = new Translator(subscriptionKey);
            

            var targetLanguages = translator.GetTargetLanguagesAsync().Result;
            var listData = new List<CustomLanguageList>();
            foreach (var lang in targetLanguages)
            {
                if (!lang.SupportsFormality) continue;
                listData.Add(new CustomLanguageList()
                {
                    Code = lang.Code,
                    Name = lang.Name
                });
                Console.WriteLine($"{lang.Name} ({lang.Code}) supports formality");
                // Example: "German (DE) supports formality"
            }

            return listData;
        }

        private List<CustomLanguageList> GetSourceLanguageList()
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");

                return new List<CustomLanguageList>();
            }

            var translator = new Translator(subscriptionKey);


            var sourceLanguages = translator.GetSourceLanguagesAsync().Result;
            var listData = new List<CustomLanguageList>();

            foreach (var lang in sourceLanguages)
            {
                listData.Add(new CustomLanguageList()
                {
                    Code = lang.Code,
                    Name = lang.Name
                });
                Console.WriteLine($"{lang.Name} ({lang.Code})"); // Example: "English (EN)"
            }


            return listData;
        }
        ////Glossaries 

        //private void GlossariesLanguageList()
        //{
        //    if (string.IsNullOrEmpty(Config?.SubscriptionKey))
        //    {
        //        _logger.Error("DeepL Authentication Key is required.");

        //        //return new CheckUsageDetails { IsSuccess = false };
        //    }

        //    var translator = new Translator(Config.SubscriptionKey);
        //    var listGlossaries = translator.ListGlossariesAsync(CancellationToken.None);


        //}

        //private void CreateGlossaries(string displayName, string sourceLanguageCode, string targetLanguageCode, GlossaryEntries entries)
        //{
        //    if (string.IsNullOrEmpty(Config?.SubscriptionKey))
        //    {
        //        _logger.Error("DeepL Authentication Key is required.");

        //        //return new CheckUsageDetails { IsSuccess = false };
        //    }

        //    var translator = new Translator(Config.SubscriptionKey);

        //    var newGlossaries = translator.CreateGlossaryAsync(displayName, sourceLanguageCode, targetLanguageCode, entries);
        //    //var deleteGlossaries = translator.DeleteGlossaryAsync("ID");
        //    //var deleteGlossaries2 = translator.DeleteGlossaryAsync(GlossaryInfo );


        //}
    }
}
