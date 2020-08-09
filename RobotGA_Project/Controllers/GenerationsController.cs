using System.Collections.Generic;
using System.Web.Mvc;
using RobotGA_Project.GASolution;
using RobotGA_Project.Models.ModelControllers;

namespace RobotGA_Project.Controllers
{
    public class GenerationsController : Controller
    {
        // GET
        public ActionResult Index()
        {
            
            var gen0 = new Generation();
            var gen1 = new Generation(gen0.Population);
            var gen2 = new Generation(gen1.Population);

            var generations = new List<Generation> {gen0, gen1, gen2};

            GenerationModelController.SetListOfGenerationModels(generations);
            
            var models = GenerationModelController.GenerationModels;
            
            ViewData["Models"] = models;
            
            return View(models);
            
        }
    }
}