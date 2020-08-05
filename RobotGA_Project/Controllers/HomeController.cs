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
            /*
            Robot testbot = new Robot();
            Robot robota = new Robot();
            Console.WriteLine(testbot.ToString());
            Console.WriteLine(robota.ToString());

            Robot robito = new Robot(testbot, robota, 10);
            Console.WriteLine(robito.ToString());
            
            robito = new Robot(robota, testbot, 10);
            Console.WriteLine(robito.ToString());
            robito.CalculateFitness();
            
            Console.WriteLine(robito.Fitness);
            //*/
            
            Generation gen0 = new Generation();
            
            foreach (var robot in gen0.Population)
            {
                Console.WriteLine(robot);
            }
            Console.WriteLine("*********************************");
            Generation gen1 = new Generation(gen0.Population);

            foreach (var robot in gen1.Population)
            {
                Console.WriteLine(robot);
            }
            Console.WriteLine("*********************************");
            Generation gen2 = new Generation(gen1.Population);

            foreach (var robot in gen2.Population)
            {
                Console.WriteLine(robot);
            }
            
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