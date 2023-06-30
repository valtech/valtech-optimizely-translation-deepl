using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valtech.Optimizely.Translation.DeepL.Presentation.ViewModels
{
    public class HelperClasses
    {
    }
    public class CustomLanguageList
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class UsageLimits
    {
        public string CountData { get; set; }
        public string Limit { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsSuccess { get; set; }
    }

}
