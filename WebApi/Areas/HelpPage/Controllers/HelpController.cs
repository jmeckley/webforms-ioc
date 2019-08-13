using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebApi.Areas.HelpPage.ModelDescriptions;
using WebApi.Areas.HelpPage.Models;


namespace WebApi.Areas.HelpPage.Controllers
{
    public class HelpController 
        : Controller
    {
        private const string ErrorViewName = "Error";

        private readonly IModelDocumentationProvider _documentationProvider;
        private readonly IApiExplorer _apiExplorer;
        private readonly ModelDescriptionGenerator _modelDescriptionGenerator;
        private readonly Func<string, HelpPageApiModel> _getHelpPageApiModel;

        public HelpController(IModelDocumentationProvider documentationProvider, IApiExplorer apiExplorer, ModelDescriptionGenerator modelDescriptionGenerator, Func<string, HelpPageApiModel> getHelpPageApiModel)
        {
            _documentationProvider = documentationProvider;
            _apiExplorer = apiExplorer;
            _getHelpPageApiModel = getHelpPageApiModel;
        }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = _documentationProvider;
            return View(_apiExplorer.ApiDescriptions);
        }

        public ActionResult Api([Required, MaxLength(256)]string apiId)
        {
            if (ModelState.IsValid)
            {
                var apiModel = _getHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel([Required, MaxLength(256)]string modelName)
        {
            if(ModelState.IsValid)
            {
                if (_modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out var modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}