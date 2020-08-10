using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using RobotGA_Project.GASolution.Data_Structures.MapStructures;
using RobotGA_Project.Models;

namespace RobotGA_Project.GASolution
{
    public static class EvolutionEnvironment
    {
        
        public static Map Map = MapLoader.LoadMap(@"C:\Users\jguty\OneDrive\GitHub\University_RobotGA\RobotGA_Project\GASolution\Data Structures\MapStructures\MapFiles\map1.txt");

        public static List<Generation> Generations = new List<Generation>();

        public static void SimulateEvolution()
        {
            var gen0 = new Generation();
            Generations.Add(gen0);
            var wonFlag = false;
            var index = 1;
            while(!wonFlag)
            {
                Console.WriteLine("Generation"+index);
                Generation newGen = new Generation(Generations[index-1].Population);
                Generations.Add(newGen);
                wonFlag = isWinnerGeneration(newGen);
                index++;
            }
        }

        private static bool isWinnerGeneration(Generation pGeneration)
        {
            return pGeneration.Population.Any(robot => robot.Won);
        }


    }
}