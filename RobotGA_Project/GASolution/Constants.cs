using System;

namespace RobotGA_Project.GASolution
{
    public abstract class Constants
    {
        /*
         * Abstract class to stock all constant values throughout the project.
         * All constant quantities open for changes.
         */

        public static int ChromosomeSize = 8; // Size measured in bits quantity 

        public static int GenotypeMinvalue = 0;
        public static int GenotypeMaxValue = (int)Math.Pow(2,ChromosomeSize);
        
        // Terrain types

        public static int BasicEnergyConsumption = 1;
        
        public static Terrain BlockedTerrain = new Terrain(Int32.MaxValue, Int32.MaxValue);
        public static Terrain EasyTerrain = new Terrain(1,BasicEnergyConsumption);
        public static Terrain MediumTerrain = new Terrain(2, BasicEnergyConsumption + 1);
        public static Terrain DifficultTerrain = new Terrain(3,BasicEnergyConsumption + 2);
        
        // Engine types

        public static  int EngineTypeQuantity = 3;
        
        public static Engine SmallEngine = new Engine(EasyTerrain);
        public static Engine MediumEngine = new Engine(MediumTerrain);
        public static Engine BigEngine = new Engine(DifficultTerrain);
        
        // Camera types
        
        public static  int CameraTypeQuantity = 3;
        
        public static Camera SmallCamera = new Camera(1);
        public static Camera MediumCamera = new Camera(2);
        public static Camera BigCamera = new Camera(3);
        
        // Battery types
        
        public static  int BatteryTypeQuantity = 3;
        
        public static Battery CommonBattery = new Battery(30); 
        public static Battery MediumBattery = new Battery(40);
        public static Battery SuperBattery = new Battery(50);
        
    }
}