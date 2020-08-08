using System;
using System.Web.Mvc;
using RobotGA_Project.GASolution;
using RobotGA_Project.GASolution.Data_Structures.MapStructures;

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
            
            // Generation gen0 = new Generation();
            //
            // foreach (var robot in gen0.Population)
            // {
            //     Console.WriteLine(robot);
            //     Console.WriteLine(1);
            // }
            //
            // Console.WriteLine("*********************************");
            //
            // Generation gen1 = new Generation(gen0.Population);
            //
            // foreach (var robot in gen1.Population) {
            //     Console.WriteLine(robot);
            //     Console.WriteLine(2);
            // }

            var map = MapLoader.LoadMap(@"C:\GitHub_Projects\University_RobotGA\RobotGA_Project\GASolution\Data Structures\MapStructures\MapFiles\map1.txt");

            Console.Out.WriteLine(map.TerrainMap[0,19].Id);
            
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