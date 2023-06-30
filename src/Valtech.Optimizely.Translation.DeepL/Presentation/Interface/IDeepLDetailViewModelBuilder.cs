using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valtech.Optimizely.Translation.DeepL.Presentation.ViewModels;

namespace Valtech.Optimizely.Translation.DeepL.Presentation.Interface
{
    public interface IDeepLDetailViewModelBuilder
    {
        DeepLDetailViewModel Build();
    }
}
