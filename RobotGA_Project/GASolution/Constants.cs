using System;

namespace RobotGA_Project.GASolution
{
    public abstract class Constants
    {
        /*
         * Abstract class to stock all constant values throughout the project.
         * All constant quantities open for changes.
         */

        public static readonly int ChromosomeSize = 8; // Size measured in bits quantity 

        public static readonly int GenotypeMinvalue = 0;
        public static readonly int GenotypeMaxValue = (int)Math.Pow(2,ChromosomeSize);

        public static readonly int MutationProbability = 10;
        
        public static readonly int BasicEnergyConsumption = 1;
        public static readonly int BasicCost = 1;

        public static readonly int MapDimensions = 20; // Height and width of the squared map
        
        // Terrain Constants

        public static readonly int PassableTerrainTypeQuantity = 3;
        public static readonly int TerrainEnergyConsumptionIncrement = 1;
        
        // Terrain types
        
        public static readonly Terrain BlockedTerrain = 
            new Terrain(Int32.MaxValue, Int32.MaxValue);
        
        public static readonly Terrain EasyTerrain = 
            new Terrain(1,BasicEnergyConsumption);
        
        public static readonly Terrain MediumTerrain = 
            new Terrain(2, BasicEnergyConsumption + TerrainEnergyConsumptionIncrement);
        
        public static readonly Terrain DifficultTerrain = 
            new Terrain(3,BasicEnergyConsumption + (2 * TerrainEnergyConsumptionIncrement));
        
        // Engine Constants

        public static readonly int EngineTypeQuantity = 3;
        public static readonly int EngineCostIncrement = 1;
        
        public static readonly int EngineMaxCost = (EngineTypeQuantity * EngineCostIncrement) + BasicCost;
        
        // Engine types
        
        public static readonly Engine SmallEngine  = new Engine(EasyTerrain, BasicCost);
        public static readonly Engine MediumEngine = new Engine(MediumTerrain, BasicCost + EngineCostIncrement);
        public static readonly Engine BigEngine    = new Engine(DifficultTerrain, BasicCost + 2 * EngineCostIncrement);
        
        
        
        // Camera Constants
        
        public static readonly int CameraTypeQuantity = 3;
        public static readonly int BasicRange = 1;
        public static readonly int CameraCostIncrement = 1;
        
        public static readonly int CameraMaxCost = (CameraTypeQuantity * CameraCostIncrement) + BasicCost;
        
        // Camera types
        
        public static readonly Camera SmallCamera =
            new Camera(BasicRange, BasicEnergyConsumption, BasicCost);
        
        public static readonly Camera MediumCamera = 
            new Camera(BasicRange+1, BasicEnergyConsumption + 1, BasicCost + CameraCostIncrement);
        
        public static readonly Camera BigCamera = 
            new Camera(BasicRange+2, BasicEnergyConsumption + 2, BasicCost + 2 * CameraCostIncrement);
        
        
        // Battery Constants
        
        public static readonly int BatteryTypeQuantity = 3;
        public static readonly int BasicEnergy = 50;
        public static readonly int EnergyIncrement = 20;
        public static readonly int BatteryCostIncrement = 1;
        
        public static readonly int BatteryMaxCost = (BatteryTypeQuantity * BatteryCostIncrement) + BasicCost;
        
        // Battery types
        
        public static readonly Battery CommonBattery = 
            new Battery(BasicEnergy, BasicCost);  
        
        public static readonly Battery MediumBattery = 
            new Battery(BasicEnergy + EnergyIncrement, BasicCost + BatteryCostIncrement);
        
        public static readonly Battery SuperBattery  = 
            new Battery(BasicEnergy +  (2 * EnergyIncrement), BasicCost + 2 * BatteryCostIncrement);

        
        // Fitness Constants

        public static int MaxFinalDistancePossible = MapDimensions * (int) Math.Sqrt(2);
        public static int MaxEnergyPossible = (BatteryTypeQuantity * EnergyIncrement) + BasicEnergy;
        public static int MaxEnergyPerStepPossible = PassableTerrainTypeQuantity * TerrainEnergyConsumptionIncrement;
        public static int MaxPossibleCost = BatteryMaxCost + CameraMaxCost + EngineMaxCost;
        
    }
}