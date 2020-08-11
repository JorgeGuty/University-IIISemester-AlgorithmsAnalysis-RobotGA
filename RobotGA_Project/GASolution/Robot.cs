using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RobotGA_Project.GASolution.Data_Structures.Graph;
using RobotGA_Project.GASolution.Data_Structures.MapStructures;

namespace RobotGA_Project.GASolution
{
    public class Robot
    {

        public int Id { get; set; }
        
        public bool Won { get; set; }
        
        public float ReproductionProbability { get; set; }
        
        public Robot ParentA { get; set; }
        
        public Robot ParentB { get; set; }
        
        public int TotalCost { get; set; }

        public int Fitness { get; set; }
        public Hardware Hardware { get; set; }
        
        public Software Software { get; set; }
        
        public List<(int, int)> BestTry { get; set; }
        
        private int BestDistance { get; set; }
        
        private List<(int,int)> LastTry { get; set; }
        
        public int EnergyLeft { get; set; }

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
            BestTry = new List<(int, int)>();
            LastTry = new List<(int, int)>();
            TotalCost = Hardware.Cost;
            BestDistance = Constants.MaxFinalDistancePossible;
            Position = Constants.StartIndex;
            Won = false;
            SetVisionRange();
        }
        
        public void CalculateFitness()
        {
            
            /*
             * Function set to calculate the fitness of an individual
             */
            
            var distancesToEnd = new List<int>();
            var energiesPerStep = new List<int>();
            var stepsForward = new List<int>();
            var nonRepeatedStepsQuantities = new List<int>();
            
            for (int times = 0; times < Constants.TRIES_TOTAL; times++)
            {
                EnergyLeft = Hardware.Battery.Energy;
                Position = Constants.StartIndex;
                LastTry.Clear();
                SetVisionRange();
                var life = Live();

                stepsForward.Add(life.Item1);
                nonRepeatedStepsQuantities.Add(life.Item2);
                distancesToEnd.Add(life.Item3);
                energiesPerStep.Add(life.Item4);
                
                if (life.Item3 <= BestDistance)
                {
                    BestTry.Clear();
                    foreach ((int, int) tuple in LastTry)
                    {
                        BestTry.Add(tuple);
                    }
                    BestDistance = life.Item3;
                }
                
            }
            
            int averageDistanceToGoal =
                (int)MathematicalOperations.Average(distancesToEnd, distancesToEnd.Count);

            int averageStepsForward = 
                (int)MathematicalOperations.Average(stepsForward, stepsForward.Count);

            int averageEnergyPerStep =
                (int)MathematicalOperations.Average(energiesPerStep, energiesPerStep.Count);

            int averageNonRepeatedSteps = 
                (int)MathematicalOperations.Average(nonRepeatedStepsQuantities, nonRepeatedStepsQuantities.Count);

            int distanceScore = Constants.MaxFinalDistancePossible - averageDistanceToGoal;
            int stepsScore = Constants.MaxEnergyPossible - averageStepsForward;
            int energyPerStepScore = Constants.MaxEnergyPerStepPossible - averageEnergyPerStep;
            int costScore = Constants.MaxCostPossible - TotalCost;
            int repeatedStepsScore = averageNonRepeatedSteps;
            
            int distanceFit = 
                Constants.FIRST_PRIORITY_VALUE * GeneticOperations.NormalizeFitnessScore(distanceScore, Constants.MaxFinalDistancePossible);
            int nonRepeatedStepsFit =
                Constants.SECOND_PRIORITY_VALUE *
                GeneticOperations.NormalizeFitnessScore(repeatedStepsScore, Constants.MaxNonRepeatedStepsPossible);
            int stepsFit = 
                Constants.SECOND_PRIORITY_VALUE * GeneticOperations.NormalizeFitnessScore(stepsScore, Constants.MaxEnergyPossible);
            int energyFit = 
                Constants.THIRD_PRIORITY_VALUE * GeneticOperations.NormalizeFitnessScore(energyPerStepScore, Constants.MaxEnergyPerStepPossible);
            int costFit = 
                Constants.LAST_PRIORITY_VALUE * GeneticOperations.NormalizeFitnessScore(costScore, Constants.MaxCostPossible);
            
            int wonFit = Constants.WinBonus;
            
            Fitness = distanceFit + nonRepeatedStepsFit + stepsFit + energyFit + costFit;
            if (Won) Fitness += wonFit;
            
        }

        public (int, int, int, int) Live()
        {
            /*
             *  Returns: (totalStepsForward, totalRepeatedSteps, distanceToGoal, energyPerStep)
             */
            
            var totalStepsForward = 0;
            var totalNonRepeatedSteps = 0;
            var totalSteps = 0;
            var totalEnergyConsume = 0;

            while (EnergyLeft > 0 && !Won)
            {
                var (stepsForward, nonRepeatedSteps, steps, energyConsumed) = MovementDecision();
                totalStepsForward += stepsForward;
                totalNonRepeatedSteps += nonRepeatedSteps;
                totalSteps += steps;
                totalEnergyConsume += energyConsumed;
                if (Position.Item1 == Constants.GoalIndex.Item1 && Position.Item2 == Constants.GoalIndex.Item2)
                {
                    Won = true;
                }
            }

            var distanceToGoal = (int)MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex);

            if (totalSteps == 0) totalSteps = 1;
            var energyPerStep = totalEnergyConsume / totalSteps;
            
            return (totalStepsForward, totalNonRepeatedSteps, distanceToGoal, energyPerStep);

        }
        
        public (int, int, int, int) MovementDecision()
        {
            /*
             * Returns: (stepsForward, repeatedSteps totalSteps, energyConsumed)
             */
            var randomDecision = MathematicalOperations.Random0To1Float();
            var accumulatedPercentage = 0f;
            var selectedPosition = Position;
            
            foreach (Arc<(int, int)> arc in VisionRange.Arcs)
            {
                accumulatedPercentage += arc.Weight;
                if (randomDecision <= accumulatedPercentage)
                {
                    selectedPosition = arc.PointB.Object;
                    break;
                }
            }

            var movement = Move(selectedPosition);
            SetVisionRange();
            return movement;
            
        }

        public (int, int, int, int) Move((int, int) pPosition)
        {
            var stepsForward = 0;
            var nonRepeatedSteps = 0;
            var energyConsumed = 0;
            var totalSteps = 0;
            
            if (EnergyLeft > 0)
            {
                var (row, column) = Position;
                
                if(pPosition.Item1 != row && pPosition.Item2 != column) Console.Write(pPosition);
                
                while (row != pPosition.Item1 || column != pPosition.Item2)
                {
                    if (row < pPosition.Item1) row++;
                    if (row > pPosition.Item1) row--;
                    if (column < pPosition.Item2) column++;
                    if (column > pPosition.Item2) column--;
                    
                
                    var terrainInPosition = EvolutionEnvironment.Map.TerrainMap[row,column];
                    
                    if (terrainInPosition.DifficultyLevel > Hardware.Engine.MaxTerrainDifficulty)
                    {
                        EnergyLeft = 0;
                        break;
                    }
                    
                    var currentDistanceToGoal =
                        (int)MathematicalOperations.DistanceBetweenPoints(Position, Constants.GoalIndex);
                    var futureDistanceToGoal =
                        (int) MathematicalOperations.DistanceBetweenPoints((row, column), Constants.GoalIndex);

                    if (currentDistanceToGoal < futureDistanceToGoal) stepsForward++;
                    
                    if (!LastTry.Contains((row, column))) nonRepeatedSteps++;
                    
                    var energyConsume = Hardware.Camera.EnergyConsumption + terrainInPosition.EnergyConsumption;
                    
                    EnergyLeft -= energyConsume;
                    
                    totalSteps++;
                    energyConsumed += energyConsume;
                    
                    Position = (row,column);
                    LastTry.Add(Position);
                    
                }
            }
            
            return (stepsForward, nonRepeatedSteps, totalSteps, energyConsumed);

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

           bool blockedAbove = false;
           bool blockedBelow = false;
           bool blockedRight = false;
           bool blockedLeft = false;

           var robotRange = Hardware.Camera.Range;
           for (var range = 1; range <= robotRange; range++)
           {
               aboveIndex = (Position.Item1, Position.Item2 - range);
               if (aboveIndex.Item2 >= 0 && !blockedAbove)
               {
                   if (EvolutionEnvironment.Map.TerrainMap[aboveIndex.Item1,aboveIndex.Item2] != Constants.BlockedTerrain)
                   {
                       aboveNode =  new Node<(int,int)>(aboveIndex);
                       VisionRange.AddNode(aboveNode);
                       VisionRange.AddArc(0, positionNode, aboveNode);
                   }
                   else
                   {
                       blockedAbove = true;
                   }

               }
               
               belowIndex = (Position.Item1, Position.Item2 + range);
               if (belowIndex.Item2 < Constants.MapDimensions && !blockedBelow)
               {
                   if (EvolutionEnvironment.Map.TerrainMap[belowIndex.Item1,belowIndex.Item2] != Constants.BlockedTerrain)
                   {
                       belowNode =  new Node<(int,int)>(belowIndex);
                       VisionRange.AddNode(belowNode);
                       VisionRange.AddArc(0, positionNode, belowNode);
                   }
                   else
                   {
                       blockedBelow = true;
                   }
               }
               
               rightIndex = (Position.Item1 + range, Position.Item2);
               if (rightIndex.Item1 < Constants.MapDimensions && !blockedRight)
               {
                   if (EvolutionEnvironment.Map.TerrainMap[rightIndex.Item1,rightIndex.Item2] != Constants.BlockedTerrain)
                   {
                       rightNode =  new Node<(int,int)>(rightIndex);
                       VisionRange.AddNode(rightNode);
                       VisionRange.AddArc(0, positionNode, rightNode);
                   }
                   else
                   {
                       blockedRight = true;
                   }
               }
               
               leftIndex = (Position.Item1 - range, Position.Item2);
               if (leftIndex.Item1 >= 0 && !blockedLeft)
               {
                   if (EvolutionEnvironment.Map.TerrainMap[leftIndex.Item1,leftIndex.Item2] != Constants.BlockedTerrain)
                   {
                       leftNode =  new Node<(int,int)>(leftIndex);
                       VisionRange.AddNode(leftNode);
                       VisionRange.AddArc(0, positionNode, leftNode);
                   }
                   else
                   {
                       blockedLeft = true;
                   }
               }
           }
           CalculateArchesWeights();
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

        public void CalculateArchesWeights()
        {
            var map = EvolutionEnvironment.Map;
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