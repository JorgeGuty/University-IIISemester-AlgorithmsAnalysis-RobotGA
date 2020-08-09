using System.Collections.Generic;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models.ModelControllers
{
    public static class GenerationModelController
    {
        public static List<GenerationModel> GenerationModels { get; set; }
        
        public static void SetListOfGenerationModels(List<Generation> pGenerations)
        {
            List<GenerationModel> generations = new List<GenerationModel>();
            for (int index = 0; index < pGenerations.Count; index++)
            {
                var generationModel = GenerateGenerationModel(pGenerations[index], index);
                generations.Add(generationModel);
            }
            GenerationModels = generations;
        }
        
        private static GenerationModel GenerateGenerationModel(Generation pGeneration, int pGenerationId)
        {
            GenerationModel model = new GenerationModel()
            {
                Id = pGenerationId,
                Population = GenerateGenerationOfModels(pGeneration,pGenerationId),
                FitnessStandardDeviation = pGeneration.FitnessStandardDeviation
            };
            return model;
        }
        
        private static List<RobotModel> GenerateGenerationOfModels(Generation pGeneration, int pGenerationId)
        {
            List<RobotModel> generationOfModels = new List<RobotModel>();
            for (int index = 0; index < generationOfModels.Count; index++)
            {
                RobotModel newModel = GenerateRobotModel(pGeneration.Population[index], index, pGenerationId);
                generationOfModels.Add(newModel);
            }
            return generationOfModels;
        }
        
        private static RobotModel GenerateRobotModel(Robot pRobot, int pId, int pGenerationId)
        {
            RobotModel model = new RobotModel()
            {
                Id = pId,
                GenerationId = pGenerationId,
                Fitness = pRobot.Fitness,
                ReproductionProbability = pRobot.ReproductionProbability,
                Cost = pRobot.TotalCost,
                Hardware = GenerateHardwareModel(pRobot.Hardware),
                Software = GenerateSoftwareModel(pRobot.Software),
                Route = pRobot.Route
            };
            return model;
        }
        
        private static HardwareModel GenerateHardwareModel(Hardware pHardware)
        {
            HardwareModel model = new HardwareModel
            {
                CameraRange = pHardware.Camera.Range,
                CameraGenotype = pHardware.CameraGenotype,
                CameraChromosome = MathematicalOperations.ConvertIntToBinaryString(pHardware.CameraGenotype),
                EngineCapacity = pHardware.Engine.MaxTerrainDifficulty,
                EngineGenotype = pHardware.EngineGenotype,
                EngineChromosome = MathematicalOperations.ConvertIntToBinaryString(pHardware.EngineGenotype),
                BatteryEnergy = pHardware.Battery.Energy,
                BatteryGenotype = pHardware.BatteryGenotype,
                BatteryChromosome = MathematicalOperations.ConvertIntToBinaryString(pHardware.BatteryGenotype)
            };
            return model;
        }
        
        private static SoftwareModel GenerateSoftwareModel(Software pSoftware)
        {
            SoftwareModel model = new SoftwareModel
            {
                MoveTowardsEnd = pSoftware.MoveTowardsEnd,
                MoveAwayFromEnd = pSoftware.MoveAwayFromEnd,
                MoveToPassableTerrain = pSoftware.MoveToPassableTerrain,
                MoveToNonPassableTerrain = pSoftware.MoveToNonPassableTerrain,
                SpendTheLessEnergy = pSoftware.SpendTheLessEnergy,
                SpendTheMostEnergy = pSoftware.SpendTheMostEnergy,
                SpendNormalEnergy = pSoftware.SpendNormalEnergy,
                MoveTowardsEndChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.MoveTowardsEnd),
                MoveAwayFromEndChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.MoveAwayFromEnd),
                MoveToPassableTerrainChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.MoveToPassableTerrain),
                MoveToNonPassableTerrainChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.MoveToNonPassableTerrain),
                SpendTheLessEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.SpendTheLessEnergy),
                SpendTheMostEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.SpendTheMostEnergy),
                SpendNormalEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(pSoftware.SpendNormalEnergy)
            };
            return model;
        }
        
    }
}