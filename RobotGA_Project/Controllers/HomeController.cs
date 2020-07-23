using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            Robot testbot = new Robot();
            Robot robota = new Robot();

            Robot robito = new Robot(testbot, robota);
            Console.WriteLine(robito.ToString());
            
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}