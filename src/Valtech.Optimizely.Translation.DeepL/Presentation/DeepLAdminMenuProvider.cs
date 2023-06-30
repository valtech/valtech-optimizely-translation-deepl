using EPiServer.Shell.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valtech.Optimizely.Translation.DeepL.Presentation
{
    [MenuProvider]
    public class DeepLAdminMenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var listMenuItem = new UrlMenuItem("DeepL Manager", "/global/cms/admin/Valtech.Optimizely.Translation.DeepL",
                "/admin/deepLmanager")
            {
                IsAvailable = context => true,
                SortIndex = SortIndex.Last + 1,
                //AuthorizationPolicy = RobotsConstants.AuthorizationPolicy
            };

            return new List<MenuItem> { listMenuItem };
        }
    }
}
