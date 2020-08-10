using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using RobotGA_Project.GASolution.Data_Structures.MapStructures;

namespace RobotGA_Project.GASolution
{
    public static class EvolutionEnvironment
    {
        
        public static Map Map = MapLoader.LoadMap(@"C:\Users\jguty\OneDrive\GitHub\University_RobotGA\RobotGA_Project\GASolution\Data Structures\MapStructures\MapFiles\map1.txt");
        
        public static List<Generation> Generations = new List<Generation>()
        {
            new Generation()
        };


        public static void SimulateEvolution()
        {
            
        }


    }
}