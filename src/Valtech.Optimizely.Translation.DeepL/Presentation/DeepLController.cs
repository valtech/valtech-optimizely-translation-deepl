using EPiServer.Labs.LanguageManager.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeepL;
using EPiServer.Logging;
using Valtech.Optimizely.Translation.DeepL.Presentation.Interface;
using Valtech.Optimizely.Translation.DeepL.Presentation.ViewModels;

namespace Valtech.Optimizely.Translation.DeepL.Presentation
{
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Policy = RobotsConstants.AuthorizationPolicy)]
    public class DeepLController : Controller
    {
        public readonly IDeepLDetailViewModelBuilder _builder;
        private readonly ILogger _logger = LogManager.GetLogger(typeof(DeepLMachineTranslatorProvider));
        public readonly ILanguageManagerConfig LanguageManagerConfig;


        public DeepLController(IDeepLDetailViewModelBuilder builder, ILanguageManagerConfig languageManagerConfig)
        {
            _builder = builder;
            LanguageManagerConfig = languageManagerConfig;
        }

        [HttpPost]
        [Route("/admin/deepLmanager/[action]")]
        public ActionResult SaveGlossary(string displayName, string sourceLanguageCode, string targetLanguageCode /*GlossaryEntries entries*/)
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;
            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");
            }

            var translator = new Translator(subscriptionKey);

            var entriesDictionary = new Dictionary<string, string> { { "artist", "Maler" }, { "prize", "Gewinn" } };

            var newGlossaries = translator.CreateGlossaryAsync(displayName, sourceLanguageCode, targetLanguageCode, 
                new GlossaryEntries(entriesDictionary)).Result;

            return RedirectToAction("DetailResult");

        }

        [HttpGet]
        [Route("/admin/deepLmanager")]
        public IActionResult DetailResult()
        {
            var model = _builder.Build();

            // post man test 
            //return new JsonResult(model);

            return View("~/Views/DeepLAdmin/DeepLDetails.cshtml", model);
        }

        [HttpGet]
        [Route("/admin/deepLmanager/[action]",Name = "deleteGlossary")]
        public ActionResult DeleteGlossaries(Guid glossaryId)
        {
            var subscriptionKey = LanguageManagerConfig.ActiveTranslatorProvider.SubscriptionKey;

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                _logger.Error("DeepL Authentication Key is required.");
            }

            var translator = new Translator(subscriptionKey);
            var deleteGlossaries = translator.DeleteGlossaryAsync(glossaryId.ToString());
            ViewBag.Messsage = "Delete Successfully";

            return RedirectToAction("DetailResult");

        }


    }
}
