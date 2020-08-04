using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace RobotGA_Project.GASolution
{
    public class Robot
    {
        
        public Battery Battery { get; set; }
        public Camera Camera { get; set; }
        public Engine Engine { get; set; }
        
        public int BatteryGenotype { get; set; }
        public int CameraGenotype { get; set; }
        public int EngineGenotype { get; set; }
        
        public string CompleteHardwareChromosome { get; set; }
        
        public int Fitness { get; set; }
        
        public Robot ParentA { get; set; }
        
        public Robot ParentB { get; set; }
        
        public int TotalCost { get; set; }
        
        public List<Terrain> Route { get; set; }

        public Robot(Robot pParentA, Robot pParentB, int pPartitionIndex)
        {
            
            /*
             * Method that initializes a Robot with two predecessors.
             * Uses genetic material mixing.
             */
            
            ParentA = pParentA;
            ParentB = pParentB;

            //  Sets genotypes with the given genetic material.
            CompleteHardwareChromosome = GeneticOperations.MixGeneticMaterial(pParentA.CompleteHardwareChromosome, pParentB.CompleteHardwareChromosome, pPartitionIndex);

            SetHardwareGenotypes();
            
            InitializeFields();
            
        }

        public Robot()
        {
            /*
             * Method that initializes a Robot with random values. 
             */
            
            ParentA = null;  // Diosito
            ParentB = null;  // La virgencita
            
            // Sets random initial genotypes.
            CameraGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);
            BatteryGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;
            EngineGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;

            string batteryChromosome = MathematicalOperations.ConvertIntToBinaryString(BatteryGenotype);
            string cameraChromosome = MathematicalOperations.ConvertIntToBinaryString(CameraGenotype);
            string engineChromosome = MathematicalOperations.ConvertIntToBinaryString(EngineGenotype);

            // IMPORTANT: The order of the chromosomes in the complete chromosome must stay the same !!!
            CompleteHardwareChromosome = batteryChromosome + cameraChromosome + engineChromosome;
            
            InitializeFields();
            
        }

        private void InitializeFields()
        {
            Fitness = 0;
            
            int minValue = Constants.GenotypeMinvalue;
            int maxValue = Constants.GenotypeMaxValue;
            
            // With the given genotypes, gets the phenotype represented by each of them.
            SetBattery(minValue,maxValue);
            SetCamera(minValue,maxValue);
            SetEngine(minValue,maxValue);

            // Add the costs of all pieces of hardware to get the total cost of the robot.
            TotalCost = Battery.Cost + Engine.Cost + Camera.Cost;
            
            Route = new List<Terrain>();
        }
        
        private void SetHardwareGenotypes()
        {
            string batteryChromosome = CompleteHardwareChromosome.Substring(0, Constants.ChromosomeSize);
            string cameraChromosome =
                CompleteHardwareChromosome.Substring(Constants.ChromosomeSize, Constants.ChromosomeSize);
            string engineChromosome =
                CompleteHardwareChromosome.Substring(2 * Constants.ChromosomeSize, Constants.ChromosomeSize);

            BatteryGenotype = MathematicalOperations.ConvertBinaryStringToInt(batteryChromosome);
            CameraGenotype = MathematicalOperations.ConvertBinaryStringToInt(cameraChromosome);
            EngineGenotype = MathematicalOperations.ConvertBinaryStringToInt(engineChromosome);

        }


        private void SetEngine(int pMinValue, int pMaxValue)
        {
            
            int interval = pMaxValue / Constants.EngineTypeQuantity;

            if (pMinValue <= EngineGenotype && EngineGenotype < interval)  // MinValue-interval
            {
                Engine = Constants.SmallEngine;
            }
            else if (interval <= EngineGenotype && EngineGenotype < interval * 2)  // interval-2(interval)
            {
                Engine = Constants.MediumEngine;
            }
            else if (interval * 2 <= EngineGenotype && EngineGenotype < pMaxValue) // 2(interval)-MaxValue
            {
                Engine = Constants.BigEngine;
            }
        }
        private void SetCamera(int pMinValue, int pMaxValue)
        {

            int interval = pMaxValue / Constants.CameraTypeQuantity;

            if (pMinValue <= CameraGenotype && CameraGenotype < interval)
            {
                Camera = Constants.SmallCamera;
            }
            else if (interval <= CameraGenotype && CameraGenotype < interval * 2)
            {
                Camera = Constants.MediumCamera;
            }
            else if (interval * 2 <= CameraGenotype && CameraGenotype < pMaxValue)
            {
                Camera = Constants.BigCamera;
            }
        }
        private void SetBattery(int pMinValue, int pMaxValue)
        {
            int interval = pMaxValue / Constants.BatteryTypeQuantity;

            if (pMinValue <= BatteryGenotype && BatteryGenotype < interval)
            {
                Battery = Constants.CommonBattery;
            }
            else if (interval <= BatteryGenotype && BatteryGenotype < interval * 2)
            {
                Battery = Constants.MediumBattery;
            }
            else if (interval * 2 <= BatteryGenotype && BatteryGenotype < pMaxValue)
            {
                Battery = Constants.SuperBattery;
            }
        }

        public void CalculateFitness()
        {
            
            /*
             * Function set to calculate the fitness of an individual
             */

            int randomDistanceToGoal =
                MathematicalOperations.RandomIntegerInRange(0, Constants.MaxFinalDistancePossible);
            //Console.WriteLine(randomDistanceToGoal);
            int distanceScore = 
                GeneticOperations.NormalizeFitnessScore(randomDistanceToGoal, Constants.MaxFinalDistancePossible);
            //Console.WriteLine(distanceScore);
            int randomStepsForward = 
                MathematicalOperations.RandomIntegerInRange(0, Constants.MaxEnergyPossible);
            //Console.WriteLine(randomStepsForward);
            int stepsScore = GeneticOperations.NormalizeFitnessScore(randomStepsForward, Constants.MaxEnergyPossible);
            //Console.WriteLine(stepsScore);
            int randomEnergyPerStep =
                MathematicalOperations.RandomIntegerInRange(0, Constants.MaxEnergyPerStepPossible);
            //Console.WriteLine(randomEnergyPerStep);
            int energyScore =
                GeneticOperations.NormalizeFitnessScore(randomEnergyPerStep, Constants.MaxEnergyPerStepPossible);
            //Console.WriteLine(energyScore);
            int costScore = GeneticOperations.NormalizeFitnessScore(TotalCost, Constants.MaxCostPossible);
            //Console.WriteLine(costScore);

            Fitness = distanceScore + stepsScore + energyScore + costScore;
            
        }
        
        public void Mutate()
        {
            int minValue = Constants.GenotypeMinvalue;
            int maxValue = Constants.GenotypeMaxValue;
            
            SetHardwareGenotypes();
            
            SetBattery(minValue,maxValue);
            SetCamera(minValue,maxValue);
            SetEngine(minValue,maxValue);
        }

        public override string ToString()
        {
            string stringObject;

            stringObject = "Rango de Cámara: " + Camera.Range + "    ";
            stringObject += "Carga de Batería: " + Battery.Energy + "    ";
            stringObject += "Capacidad de Motor: " + Engine.MaxTerrainDifficulty+ "    ";
            
            return stringObject;

        }
    }
}