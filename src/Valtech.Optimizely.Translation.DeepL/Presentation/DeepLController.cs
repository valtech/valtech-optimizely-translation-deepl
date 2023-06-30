using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Valtech.Optimizely.Translation.DeepL.Presentation.Interface;

namespace Valtech.Optimizely.Translation.DeepL.Presentation
{
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Policy = RobotsConstants.AuthorizationPolicy)]
    public class DeepLController : Controller
    {
        public readonly IDeepLDetailViewModelBuilder _builder;

        public DeepLController(IDeepLDetailViewModelBuilder builder)
        {
            _builder = builder;
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

       
    }
}
