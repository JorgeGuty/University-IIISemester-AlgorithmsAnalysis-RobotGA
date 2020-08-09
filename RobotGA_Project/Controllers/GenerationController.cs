using System.Web.Mvc;
using RobotGA_Project.Models;
using RobotGA_Project.Models.ModelControllers;

namespace RobotGA_Project.Controllers
{
    public class GenerationController : Controller
    {
        // GET
        public ActionResult Index(int pGenerationId)
        {
            foreach (var generationModel in GenerationModelController.GenerationModels)
            {
                if (generationModel.Id == pGenerationId)
                {
                    GenerationModel selectedGeneration = generationModel;
                    return View(selectedGeneration);
                }
            }
            return RedirectToAction("Index","Generations");
        }
    }
}