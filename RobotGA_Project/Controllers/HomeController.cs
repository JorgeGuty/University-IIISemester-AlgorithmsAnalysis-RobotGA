using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RobotGA_Project.GASolution;
using RobotGA_Project.Models.ModelControllers;

namespace RobotGA_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*
             Robot testbot = new Robot();
             Console.WriteLine(testbot);
             Console.WriteLine(testbot.VisionRange.Nodes.Count);
             
             
            Robot robota = new Robot();
            Console.WriteLine(testbot.ToString());
            Console.WriteLine(robota.ToString());
 
            Robot robito = new Robot(testbot, robota, 10);
            Console.WriteLine(robito.ToString());
            
            robito = new Robot(robota, testbot, 10);
            Console.WriteLine(robito.ToString());
            robito.CalculateFitness();
            
            Console.WriteLine(robito.Fitness);
            
             
             Generation gen0 = new Generation();
             
             foreach (var robot in gen0.Population)
             {
                 Console.WriteLine(robot);
                 Console.WriteLine(1);
             }
             
             Console.WriteLine("*********************************");
             
             Generation gen1 = new Generation(gen0.Population);
 
             foreach (var robot in gen1.Population)
             {
                 Console.WriteLine(robot);
                 Console.WriteLine(2);
             }
             //*/
            return View();
        }

        public ActionResult RunGA()
        {
            var gen0 = new Generation();
            var gen1 = new Generation(gen0.Population);
            var gen2 = new Generation(gen1.Population);

            var generations = new List<Generation> {gen0, gen1, gen2};

            GenerationModelController.SetListOfGenerationModels(generations);
            return RedirectToAction("Index", "Generations");
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