using System;

namespace RobotGA_Project.GASolution.Data_Structures.MapStructures {
    
    public static class MapLoader {
        
        public static Map LoadMap(string path) {
            var map = new Map();
            var lines = System.IO.File.ReadAllLines(path);
            var i = 0;
            var j = 0;
            
            foreach (var line in lines) {

                foreach (var c in line) {
                    map.TerrainMap[i, j] = GetTerrainFromChar(c);
                    j++;
                }
                j = 0;
                i++;
            }

            Console.Out.WriteLine("Successfully loaded file");
            return map;
        }

        private static Terrain GetTerrainFromChar(char terrainId) {
            switch (terrainId) {
                case 'M':
                    return Constants.BlockedTerrain;
                case 'B':
                    return Constants.MediumTerrain;
                case 'C':
                    return Constants.DifficultTerrain;
                default:
                    return Constants.EasyTerrain;
            }
        }
    }
}