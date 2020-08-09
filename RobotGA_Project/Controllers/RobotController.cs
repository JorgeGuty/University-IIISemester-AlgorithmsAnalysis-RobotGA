using System.Web.Mvc;
using RobotGA_Project.Models;
using RobotGA_Project.Models.ModelControllers;

namespace RobotGA_Project.Controllers
{
    public class RobotController : Controller
    {
        // GET
        public ActionResult Index(int pRobotId, int pGenerationId)
        {
            foreach (var generationModel in GenerationModelController.GenerationModels)
            {
                if (generationModel.Id != pGenerationId) continue;
                foreach (var robotModel in generationModel.Population)
                {
                    if (robotModel.Id == pRobotId)
                    {
                        var selectedRobot = robotModel;
                        return View(selectedRobot);                        
                    }
                }
            }
            return RedirectToAction("Index","Generation");
        }
    }
}