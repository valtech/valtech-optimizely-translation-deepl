using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

    public class CustomGlossaries
    {
        public string GlossaryId { get; set; }
        public string Name { get; set; }
        public bool Ready { get; set; }
        public string SourceLanguageCode { get; set; }
        public string TargetLanguageCode { get; set; }
        public DateTime CreationTime { get; set; }
        public int EntryCount { get; set; }
        public string ErrorMsg { get; set; }
    }

}
