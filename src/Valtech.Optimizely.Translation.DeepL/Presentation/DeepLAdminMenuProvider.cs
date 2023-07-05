using EPiServer.Labs.LanguageManager.Configuration;
using EPiServer.Shell.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polly;

namespace Valtech.Optimizely.Translation.DeepL.Presentation
{
    [MenuProvider]
    public class DeepLAdminMenuProvider : IMenuProvider
    {
        public readonly ILanguageManagerConfig LanguageManagerConfig;

        public DeepLAdminMenuProvider(ILanguageManagerConfig languageManagerConfig)
        {
            LanguageManagerConfig = languageManagerConfig;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            var listMenuItem = new UrlMenuItem("DeepL Manager", "/global/cms/admin/Valtech.Optimizely.Translation.DeepL", "/admin/deepLmanager")
            {
                IsAvailable = context => IsDeepLManagerVisible(),
                SortIndex = SortIndex.Last + 1,
                //AuthorizationPolicy = RobotsConstants.AuthorizationPolicy
            };


            return new List<MenuItem> { listMenuItem };
        }

        private bool IsDeepLManagerVisible()
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;
            return !string.IsNullOrEmpty(subscriptionKey);
        }
    }
}
