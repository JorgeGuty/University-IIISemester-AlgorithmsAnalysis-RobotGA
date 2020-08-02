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
        
        public int BatteryGenotype { get; }
        
        public int CameraGenotype { get; }
        
        public int EngineGenotype { get; }
        
        public int Fitness { get; set; }
        
        public Robot ParentA { get; set; }
        
        public Robot ParentB { get; set; }
        
        public int TotalCost { get; set; }
        
        public List<Terrain> Route { get; set; }

        public Robot(Robot pParentA, Robot pParentB)
        {
            /*
             * Method that initializes a Robot with two predecessors.
             * Uses genetic material mixing.
             */
            
            ParentA = pParentA;
            ParentB = pParentB;

            //  Sets genotypes with the given genetic material.
            CameraGenotype = MixGeneticMaterial(pParentA.CameraGenotype, pParentB.CameraGenotype);
            BatteryGenotype = MixGeneticMaterial(pParentA.BatteryGenotype, pParentB.BatteryGenotype);;
            EngineGenotype = MixGeneticMaterial(pParentA.EngineGenotype, pParentB.EngineGenotype);;

            InitializeFields();
            
        }

        public Robot()
        {
            /*
             * Method that initializes a Robot with random values. 
             */
            
            ParentA = null;  // Diosito
            ParentB = null;  // La quinceañera
            
            // Sets random initial genotypes.
            CameraGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);
            BatteryGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;
            EngineGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;

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

        public int CalculateFitness()
        {
            /*
             * Function set to calculate the fitness of an individual
             */
            
            return 0;
        }
        
        
        public int MixGeneticMaterial(int pGenotypeA, int pGenotypeB)
        {
            
            string chromosomeA = MathematicalOperations.ConvertIntToBinaryString(pGenotypeA);
            string chromosomeB = MathematicalOperations.ConvertIntToBinaryString(pGenotypeB);
            
            int partitionIndex = MathematicalOperations.RandomIntegerInRange(1, 
                Constants.ChromosomeSize);
            
            string chromosomeAPart = chromosomeA.Substring(0, partitionIndex);
            Console.WriteLine("ChromosomeA Part");
            Console.WriteLine(chromosomeAPart);
            Console.WriteLine();
            
            string chromosomeBPart = chromosomeB.Substring(partitionIndex, Constants.ChromosomeSize - partitionIndex);
            Console.WriteLine("ChromosomeB Part");
            Console.WriteLine(chromosomeBPart);
            Console.WriteLine();

            string childChromosome = chromosomeAPart + chromosomeBPart;

            int mutationChance = MathematicalOperations.RandomIntegerInRange(0, 100);
            if (mutationChance <= Constants.MutationProbability)
            {
                Console.WriteLine("Mutated");
                childChromosome = Mutate(childChromosome);
                Console.WriteLine();
            }
            Console.WriteLine("Child Chromosome:");
            Console.WriteLine(childChromosome);
            Console.WriteLine();
            
            int childGenotype = MathematicalOperations.ConvertBinaryStringToInt(childChromosome);
            
            return childGenotype;

        }
        
        public string Mutate(string pChromosome)
        {
            /*
             * Function that mutates a bit from de genotype 
             */

            Console.WriteLine(pChromosome);
            
            int mutationIndex = 
                MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, 
                    Constants.ChromosomeSize);
        
            StringBuilder mutator = new StringBuilder(pChromosome);
            if (pChromosome[mutationIndex].Equals('1'))
            {
                mutator[mutationIndex] = '0';
            }
            else
            {
                mutator[mutationIndex] = '1';
            }
        
            pChromosome = mutator.ToString();
            
            Console.WriteLine(pChromosome);
            return pChromosome;
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