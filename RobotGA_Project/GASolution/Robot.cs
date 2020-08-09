using System.Collections.Generic;
using System.Linq;
using RobotGA_Project.GASolution.Data_Structures.Graph;
using RobotGA_Project.GASolution.Data_Structures.MapStructures;

namespace RobotGA_Project.GASolution
{
    public class Robot
    {

        public int Id { get; set; }
        
        public float ReproductionProbability { get; set; }
        
        public Robot ParentA { get; set; }
        
        public Robot ParentB { get; set; }
        
        public int TotalCost { get; set; }

        public int Fitness { get; set; }
        public Hardware Hardware { get; set; }
        
        public Software Software { get; set; }
        
        public List<(int, int)> Route { get; set; }

        public NonDirectedGraph<(int, int)> VisionRange; // Vision Range of the robot, depends on the camera type. Represented by a graph.
        
        public (int,int) Position { get; set; } // (Rows,Columns)

        public Robot(Robot pParentA, Robot pParentB, int pHardwarePartitionIndex, int pSoftwarePartitionIndex, int pId)
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

            InitializeFields(pId);
            
        }

        public Robot(int pId)
        {
            /*
             * Method that initializes a Robot with random values. 
             */
            
            ParentA = null;  // Diosito
            ParentB = null;  // La virgencita
            
            Hardware = new Hardware();
            Software = new Software();
            
            InitializeFields(pId);
            
        }

        private void InitializeFields(int pId)
        {
            Id = pId;
            Fitness = 0;
            ReproductionProbability = 0f;
            Route = new List<(int, int)>();
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
           for (var range = 1; range <= robotRange; range++)
           {
               aboveIndex = (Position.Item1, Position.Item2 - range);
               if (aboveIndex.Item2 >= 0)
               {
                   aboveNode =  new Node<(int,int)>(aboveIndex);
                   VisionRange.AddNode(aboveNode)    ;
                   VisionRange.AddArc(0, positionNode, aboveNode);
               }
               
               belowIndex = (Position.Item1, Position.Item2 + range);
               if (belowIndex.Item2 < Constants.MapDimensions)
               {
                   belowNode =  new Node<(int,int)>(belowIndex);
                   VisionRange.AddNode(belowNode);
                   VisionRange.AddArc(0, positionNode, belowNode);
               }
               
               rightIndex = (Position.Item1 + range, Position.Item2);
               if (rightIndex.Item1 < Constants.MapDimensions)
               {
                   rightNode =  new Node<(int,int)>(rightIndex);
                   VisionRange.AddNode(rightNode);
                   VisionRange.AddArc(0, positionNode, rightNode);
               }
               
               leftIndex = (Position.Item1 - range, Position.Item2);
               if (leftIndex.Item1 >= 0)
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


        private int ExecuteTraceOfPositions(int variableIndex, int constantIndex, string direction, Map map, List<int> energiesSpent) {
            var returnSum = 0;
            var energy = 0;
            switch (direction) {
                case "U":

                    while (variableIndex<Position.Item1) {
                        var terrain = map.TerrainMap[variableIndex, constantIndex];
                        if (terrain.DifficultyLevel>Hardware.Engine.MaxTerrainDifficulty) {
                            returnSum += Software.MoveToNonPassableTerrain;
                        }
                        else {
                            returnSum += Software.MoveToPassableTerrain;
                        }

                        if (MathematicalOperations.DistanceBetweenPoints((variableIndex, constantIndex), Constants.GoalIndex) < 
                            MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex)) {

                            returnSum += Software.MoveTowardsEnd;
                        }
                        else {
                            returnSum += Software.MoveAwayFromEnd;
                        }

                        energy += terrain.EnergyConsumption + Hardware.Camera.EnergyConsumption;
                        
                        variableIndex++;
                    }
                    energiesSpent.Add(energy);
                    return returnSum;
                
                case "D":
                    
                    while (variableIndex>Position.Item1) {
                        var terrain = map.TerrainMap[variableIndex, constantIndex];
                        if (terrain.DifficultyLevel>Hardware.Engine.MaxTerrainDifficulty) {
                            returnSum += Software.MoveToNonPassableTerrain;
                        }
                        else {
                            returnSum += Software.MoveToPassableTerrain;
                        }

                        if (MathematicalOperations.DistanceBetweenPoints((variableIndex, constantIndex), Constants.GoalIndex) < 
                            MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex)) {

                            returnSum += Software.MoveTowardsEnd;
                        }
                        else {
                            returnSum += Software.MoveAwayFromEnd;
                        }

                        energy += terrain.EnergyConsumption + Hardware.Camera.EnergyConsumption;
                        
                        variableIndex--;
                    }
                    energiesSpent.Add(energy);
                    return returnSum;
                
                case "R":
                    
                    while (variableIndex>Position.Item2) {
                        var terrain = map.TerrainMap[constantIndex, variableIndex];
                        if (terrain.DifficultyLevel>Hardware.Engine.MaxTerrainDifficulty) {
                            returnSum += Software.MoveToNonPassableTerrain;
                        }
                        else {
                            returnSum += Software.MoveToPassableTerrain;
                        }

                        if (MathematicalOperations.DistanceBetweenPoints((constantIndex, variableIndex), Constants.GoalIndex) < 
                            MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex)) {

                            returnSum += Software.MoveTowardsEnd;
                        }
                        else {
                            returnSum += Software.MoveAwayFromEnd;
                        }

                        energy += terrain.EnergyConsumption + Hardware.Camera.EnergyConsumption;
                        
                        variableIndex--;
                    }
                    energiesSpent.Add(energy);
                    return returnSum;
                
                case "L":

                    while (variableIndex<Position.Item2) {
                        var terrain = map.TerrainMap[constantIndex, variableIndex];
                        if (terrain.DifficultyLevel>Hardware.Engine.MaxTerrainDifficulty) {
                            returnSum += Software.MoveToNonPassableTerrain;
                        }
                        else {
                            returnSum += Software.MoveToPassableTerrain;
                        }

                        if (MathematicalOperations.DistanceBetweenPoints((constantIndex, variableIndex), Constants.GoalIndex) < 
                            MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex)) {

                            returnSum += Software.MoveTowardsEnd;
                        }
                        else {
                            returnSum += Software.MoveAwayFromEnd;
                        }

                        energy += terrain.EnergyConsumption + Hardware.Camera.EnergyConsumption;
                        
                        variableIndex++;
                    }
                    energiesSpent.Add(energy);
                    return returnSum;
            }
                
            
            return 0;
        }

        public void CalculateArchesWeights(Map map) {
            
            var energiesSpent = new List<int>();
            var sums = new List<int>();
            Node<(int, int)> centralNode = null;
            
            foreach (var visionRangeNode in VisionRange.Nodes) {

                var totalForArch = 0;
                
                if (visionRangeNode.Object.Item1==Position.Item1+1 &&
                    visionRangeNode.Object.Item2==Position.Item2 ) {
                    //1 Down: (i+1, j) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item1+1, Position.Item2, "D", map, energiesSpent);


                } else if (visionRangeNode.Object.Item1==Position.Item1+2 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //2 Down: (i+2, j) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item1+2, Position.Item2, "D", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1+3 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //3 Down: (i+3, j)
                    totalForArch = ExecuteTraceOfPositions(Position.Item1+3, Position.Item2, "D", map, energiesSpent);

                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2+1 ) {
                    //1 Right: (i, j+1) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item2+1, Position.Item1, "R", map, energiesSpent);

                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2+2 ) {
                    //2 Right: (i, j+2)
                    totalForArch = ExecuteTraceOfPositions(Position.Item2+2, Position.Item1, "R", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2+3 ) {
                    //3 Right: (i, j+3) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item2+3, Position.Item1, "R", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1-1 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //1 Up: (i-1, j) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item1-1, Position.Item2, "U", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1-2 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //2 Up: (i-2, j) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item1-2, Position.Item2, "U", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1-3 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //3 Up: (i-3, j)
                    totalForArch = ExecuteTraceOfPositions(Position.Item1-3, Position.Item2, "U", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2-1 ) {
                    //1 Left: (i, j-1) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item2-1, Position.Item1, "L", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2-2 ) {
                    //2 Left: (i, j-2)
                    totalForArch = ExecuteTraceOfPositions(Position.Item2-2, Position.Item1, "L", map, energiesSpent);
                    
                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2-3 ) {
                    //3 Left: (i, j-3) 
                    totalForArch = ExecuteTraceOfPositions(Position.Item2-3, Position.Item1, "L", map, energiesSpent);
                } else if (visionRangeNode.Object.Item1==Position.Item1 &&
                           visionRangeNode.Object.Item2==Position.Item2 ) {
                    //Position
                    centralNode = visionRangeNode;
                } 


                sums.Add(totalForArch);
            }

            var highestCompare = 0;
            var highestIndex = 0;
            for (var i = 0; i < energiesSpent.Count; i++) {
                if (energiesSpent[i]>highestCompare) {
                    highestCompare = energiesSpent[i];
                    highestIndex = i;
                }
            }

            var lowestCompare = highestCompare;
            var lowestIndex = 0; 
            
            for (var i = 0; i < energiesSpent.Count; i++) {
                if (energiesSpent[i]<lowestCompare) {
                    lowestCompare = energiesSpent[i];
                    lowestIndex = i;
                }
            }

            
            for (var i = 0; i < sums.Count; i++) {
                if (i==highestIndex) {
                    sums[i] += Software.SpendTheMostEnergy;        
                }
                else if (i==lowestIndex) {
                    sums[i] += Software.SpendTheLessEnergy;
                }
                else {
                    sums[i] += Software.SpendNormalEnergy;
                }
            }

            float totalSumOfEverything = sums.Sum();
            
            for (var i = 0; i < VisionRange.Nodes.Count; i++) {
                if (VisionRange.Nodes[i].Object!=centralNode.Object) {
                    VisionRange.SetWeightOfArc(sums[i] / totalSumOfEverything, centralNode, VisionRange.Nodes[i]);    
                }
            }
        }
    }
}