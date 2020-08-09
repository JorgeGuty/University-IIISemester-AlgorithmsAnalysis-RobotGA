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

            var models = GenerationModelController.GenerationModels;
            
            ViewData["Models"] = models;
            
            return View(models);
            
        }
    }
}