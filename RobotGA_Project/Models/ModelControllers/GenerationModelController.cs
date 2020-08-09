using System;
using System.Collections.Generic;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models.ModelControllers
{
    public static class GenerationModelController
    {
        public static List<GenerationModel> GenerationModels  = new List<GenerationModel>();
        
        public static void SetListOfGenerationModels(List<Generation> pGenerations)
        {
            for (int index = 0; index < pGenerations.Count; index++)
            {
                var generationModel = GenerateGenerationModel(pGenerations[index], index);
                GenerationModels.Add(generationModel);
            }
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
            for (int index = 0; index < pGeneration.Population.Count; index++)
            {
                try
                {
                    var parentAId = pGeneration.Population[index].ParentA.Id;
                    var parentBId = pGeneration.Population[index].ParentB.Id;
                    var newModel = GenerateRobotModel(pGeneration.Population[index], pGenerationId, parentAId,
                        parentBId);
                    generationOfModels.Add(newModel);
                }
                catch (NullReferenceException nullRef)
                {
                    var newModel = GenerateRobotModel(pGeneration.Population[index], pGenerationId, Constants.GEN_ZERO_PARENT_ID,
                        Constants.GEN_ZERO_PARENT_ID);
                    generationOfModels.Add(newModel);
                }
                
            }
            return generationOfModels;
        }
        
        private static RobotModel GenerateRobotModel(Robot pRobot, int pGenerationId, int pParentAId, int pParentBId)
        {

            RobotModel parentA = null;
            RobotModel parentB = null;

            if (pParentAId != Constants.GEN_ZERO_PARENT_ID)
            {
                foreach (var robotModel in GenerationModels[pGenerationId-1].Population) // Checks the previous gen to get the parents
                {
                    if (robotModel.Id == pParentAId) parentA = robotModel;
                    if (robotModel.Id == pParentBId) parentB = robotModel;
                }
            }
            
            RobotModel model = new RobotModel()
            {
                Id = pRobot.Id,
                GenerationId = pGenerationId,
                ParentA = parentA,
                ParentB = parentB,
                Fitness = pRobot.Fitness,
                ReproductionProbability = pRobot.ReproductionProbability,
                Cost = pRobot.TotalCost,
                Hardware = GenerateHardwareModel(pRobot.Hardware),
                Software = GenerateSoftwareModel(pRobot.Software),
                Route = pRobot.Route
            };
            model.Route.Add((10,10));
           
            model.Route.Add((10,11));
            model.Route.Add((10,11));
            
            model.Route.Add((10,12));
            model.Route.Add((10,12));
            model.Route.Add((10,12));
            
            model.Route.Add((10,13));
            model.Route.Add((10,13));
            model.Route.Add((10,13));
            model.Route.Add((10,13));
            
            model.Route.Add((10,14));
            model.Route.Add((10,14));
            model.Route.Add((10,14));
            model.Route.Add((10,14));
            model.Route.Add((10,14));
            
            model.Route.Add((10,15));
            model.Route.Add((10,15));
            model.Route.Add((10,15));
            model.Route.Add((10,15));
            model.Route.Add((10,15));
            model.Route.Add((10,15));
            
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