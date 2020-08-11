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
        
        public static Map Map =
            MapLoader.LoadMap(@"C:\Users\jguty\OneDrive\GitHub\University_RobotGA\RobotGA_Project\GASolution\Data Structures\MapStructures\MapFiles\map2.txt");

        public static List<Generation> Generations = new List<Generation>();

        public static Generation BestGeneration;
        public static Robot BestRun;
        
        public static void SimulateEvolution()
        {
            var gen0 = new Generation(0);
            BestGeneration = gen0;
            BestRun = gen0.BestRun;
            Generations.Add(gen0);
            var wonFlag = false;
            var index = 1;    
            while(!wonFlag)
            {
                Console.WriteLine("Generation: "+index);
                Generation newGen = new Generation(Generations[index-1].Population, index);
                if (newGen.FitnessAverage > BestGeneration.FitnessAverage) BestGeneration = newGen;
                if (newGen.BestRun.Fitness > BestRun.Fitness) BestRun = newGen.BestRun;
                
                Generations.Add(newGen);
                wonFlag = isWinnerGeneration(newGen);
                index++;
            }
        }

        private static bool isWinnerGeneration(Generation pGeneration)
        {
            var winnerCount = 0;

            foreach (var robot in pGeneration.Population)
            {
                if (robot.Won)
                {
                    winnerCount++;
                }
            }
            Console.WriteLine("COUNT:"+winnerCount);
            return winnerCount >= Constants.PopulationSize / 5 || Generations.Count > 100;
        }
        
    }
}