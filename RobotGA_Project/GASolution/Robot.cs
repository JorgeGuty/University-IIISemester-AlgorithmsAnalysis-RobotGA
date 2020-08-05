using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace RobotGA_Project.GASolution
{
    public class Robot
    {
        
        public float ReproductionProbability { get; set; }
        
        public Robot ParentA { get; set; }
        
        public Robot ParentB { get; set; }
        
        public int TotalCost { get; set; }

        public int Fitness { get; set; }
        public Hardware Hardware { get; set; }
        
        public List<Terrain> Route { get; set; }

        public Robot(Robot pParentA, Robot pParentB, int pPartitionIndex)
        {
            
            /*
             * Method that initializes a Robot with two predecessors.
             * Uses genetic material mixing.
             */
            
            ParentA = pParentA;
            ParentB = pParentB;

            Hardware =
                new Hardware(pParentA.Hardware.CompleteChromosome, pParentB.Hardware.CompleteChromosome,
                    pPartitionIndex);

            InitializeFields();
            
        }

        public Robot()
        {
            /*
             * Method that initializes a Robot with random values. 
             */
            
            ParentA = null;  // Diosito
            ParentB = null;  // La virgencita
            
            Hardware = new Hardware();
            
            InitializeFields();
            
        }

        private void InitializeFields()
        {
            Fitness = 0;
            ReproductionProbability = 0f;
            Route = new List<Terrain>();
            TotalCost = Hardware.Cost;
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

        public override string ToString()
        {
            string stringObject;

            stringObject = "Rango de Cámara: " + Hardware.Camera.Range + "    ";
            stringObject += "Carga de Batería: " + Hardware.Battery.Energy + "    ";
            stringObject += "Capacidad de Motor: " + Hardware.Engine.MaxTerrainDifficulty+ "    ";
            
            return stringObject;

        }
    }
}