using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepL.Model;

namespace Valtech.Optimizely.Translation.DeepL.Presentation.ViewModels
{
    public class DeepLDetailViewModel
    {
        public string? CharacterCount { get; internal set; }
        public string CharacterLimit { get; internal set; }
        public string DocumentCount { get; internal set; }
        public string DocumentLimit { get; internal set; }
        public List<CustomLanguageList> SourceLanguageList { get; internal set; }
        public List<CustomLanguageList> TargetLanguageList { get; internal set; }
        public List<GlossaryInfo> GlossaryInfoList { get; internal set; }
        public CustomGlossaries NewGlossary { get; internal set; }

    }
}
