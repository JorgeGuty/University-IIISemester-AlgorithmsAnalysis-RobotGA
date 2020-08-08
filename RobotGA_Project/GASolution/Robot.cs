using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using RobotGA_Project.GASolution.Data_Structures.Graph;

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
        
        public Software Software { get; set; }
        
        public List<Terrain> Route { get; set; }

        public NonDirectedGraph<(int, int)> VisionRange; // Vision Range of the robot, depends on the camera type. Represented by a graph.
        
        public (int,int) Position { get; set; } // (Rows,Columns)

        public Robot(Robot pParentA, Robot pParentB, int pHardwarePartitionIndex, int pSoftwarePartitionIndex)
        {
            
            /*
             * Method that initializes a Robot with two predecessors.
             * Uses genetic material mixing.
             */
            
            ParentA = pParentA;
            ParentB = pParentB;

            Hardware =
                new Hardware(pParentA.Hardware.CompleteChromosome, pParentB.Hardware.CompleteChromosome,
                    pHardwarePartitionIndex);
            Software = 
                new Software(pParentA.Software.CompleteChromosome, pParentB.Software.CompleteChromosome,
                    pSoftwarePartitionIndex);

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
            Software = new Software();
            
            InitializeFields();
            
        }

        private void InitializeFields()
        {
            Fitness = 0;
            ReproductionProbability = 0f;
            Route = new List<Terrain>();
            Position = Constants.StartIndex;
            TotalCost = Hardware.Cost;
            SetVisionRange();
        }
        
        private void Move((int, int) pNewPosition)
        {
            Position = pNewPosition;
            SetVisionRange();
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
        
        private void SetVisionRange()
        {
            VisionRange = new NonDirectedGraph<(int, int)>();
           
           var positionNode =  new Node<(int,int)>(Position);
           VisionRange.AddNode(positionNode);

           (int, int) aboveIndex;
           (int, int) belowIndex;
           (int, int) rightIndex;
           (int, int) leftIndex;
           
           Node<(int, int)> aboveNode;
           Node<(int, int)> belowNode;
           Node<(int, int)> rightNode;
           Node<(int, int)> leftNode;
           
           var robotRange = Hardware.Camera.Range;
           for (var range = 1; range > robotRange; range++)
           {
               aboveIndex = (Position.Item1, Position.Item2 - range);
               
               if (aboveIndex.Item1 <= Constants.MapDimensions || aboveIndex.Item2 <= Constants.MapDimensions)
               {
                   aboveNode =  new Node<(int,int)>(aboveIndex);
                   VisionRange.AddNode(aboveNode);
                   VisionRange.AddArc(0, positionNode, aboveNode);
               }
               
               belowIndex = (Position.Item1, Position.Item2 + range);

               if (belowIndex.Item1 <= Constants.MapDimensions || belowIndex.Item2 <= Constants.MapDimensions)
               {
                   belowNode =  new Node<(int,int)>(belowIndex);
                   VisionRange.AddNode(belowNode);
                   VisionRange.AddArc(0, positionNode, belowNode);
               }
               
               rightIndex = (Position.Item1 + range, Position.Item2);
               if (rightIndex.Item1 <= Constants.MapDimensions)
               {
                   rightNode =  new Node<(int,int)>(rightIndex);
                   VisionRange.AddNode(rightNode);
                   VisionRange.AddArc(0, positionNode, rightNode);
               }
               
               leftIndex = (Position.Item1 - range, Position.Item2);
               if (leftIndex.Item1 <= Constants.MapDimensions)
               {
                   leftNode =  new Node<(int,int)>(leftIndex);
                   VisionRange.AddNode(leftNode);
                   VisionRange.AddArc(0, positionNode, leftNode);
               }
           }
        }
        public override string ToString()
        {
            string stringObject;
            stringObject = "Rango de Cámara: " + Hardware.Camera.Range + "    ";
            stringObject += "Carga de Batería: " + Hardware.Battery.Energy + "    ";
            stringObject += "Capacidad de Motor: " + Hardware.Engine.MaxTerrainDifficulty+ "    ";
            stringObject += "\n";
            stringObject += "Valores de software:";
            stringObject += "\n";
            stringObject += "Puntaje Moverse hacia la meta:" + Software.MoveTowardsEnd;
            stringObject += "\n";
            stringObject += "Puntaje Moverse fuera de la meta:" + Software.MoveAwayFromEnd;
            stringObject += "\n";
            stringObject += "Puntaje Moverse a terreno pasable:" + Software.MoveToPassableTerrain;
            stringObject += "\n";
            stringObject += "Puntaje Moverse a terreno no pasable:" + Software.MoveToNonPassableTerrain;
            stringObject += "\n";
            stringObject += "Puntaje gastar menos energía:" + Software.SpendTheLessEnergy;
            stringObject += "\n";
            stringObject += "Puntaje gastar más energía:" + Software.SpendTheMostEnergy;
            stringObject += "\n";
            stringObject += "Puntaje ni más ni menos energía:" + Software.SpendNormalEnergy;
            return stringObject;

        }
    }
}