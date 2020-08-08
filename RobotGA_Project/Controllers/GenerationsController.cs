using System.Collections.Generic;
using System.Web.Mvc;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Controllers
{
    public class GenerationsController : Controller
    {
        // GET
        public ActionResult Index()
        {
            var gen0 = new Generation();
            return View();
        }
    }
}