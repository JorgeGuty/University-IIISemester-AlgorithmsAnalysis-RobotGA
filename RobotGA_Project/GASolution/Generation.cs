using System;
using System.Collections.Generic;

namespace RobotGA_Project.GASolution
{
    public class Generation
    {
        /*
         * Class that stores and manages the reproduction, mutation and fitness of a population.
         */
        public List<Robot> Population { get; set; }

        public Generation(List<Robot> pLastGeneration)
        {
            /*
             *  Initializes a population by breeding and mutating the last generation.
             */
            
            Population = Breed(pLastGeneration);
            GenerationalMutation();
            FitGeneration();
        }

        public Generation()
        {
            /*
             *  Initializes a random generation.
             */
            Population = new List<Robot>();
            for (int size = 0; size < Constants.PopulationSize; size++)
            {
                Population.Add(new Robot());
            }
            FitGeneration();
        }
        
        private List<Robot> Breed(List<Robot> pLastGeneration)
        {
            List<Robot> newGeneration = new List<Robot>();
            
            GeneticOperations.SetGenerationReproductionProbabilities(pLastGeneration);

            while (newGeneration.Count < Constants.PopulationSize)
            {
                Robot parentA = SelectMatingPartner(pLastGeneration);
                Robot parentB = SelectMatingPartner(pLastGeneration);

                var (child1, child2) = ReproduceIndividuals(parentA, parentB);

                newGeneration.Add(child1);
                newGeneration.Add(child2);
            }

            return newGeneration;
        }

        private void FitGeneration()
        {
            foreach (var robot in Population)
            {
                robot.CalculateFitness();
            }
        }

        private void GenerationalMutation()
        {
            /*
             *  Mutates a bit from random individuals from the population.
             *  Number of mutations dictated in Constants.cs
             */
            int individualIndex;
            int bitIndex;

            for (int times = 0; times < Constants.MutationProbability; times++)
            {
                
                individualIndex = 
                    MathematicalOperations.RandomIntegerInRange(0, Constants.PopulationSize);
                bitIndex = 
                    MathematicalOperations.RandomIntegerInRange(0, Constants.CompleteChromosomeSize);

                string completeHardwareChromosome = Population[individualIndex].Hardware.CompleteChromosome;
                
                completeHardwareChromosome =
                    GeneticOperations.Mutate(bitIndex, completeHardwareChromosome);
                
                Population[individualIndex].Hardware.Mutate(completeHardwareChromosome);
                
            }
            
        }
        
        private Robot SelectMatingPartner(List<Robot> pGeneration)
        {
            var random0To1Number = MathematicalOperations.Random0to1Float();
            float accumulatedProbability = 0;
            var selectedIndex = -1;
            
            for (var index = 0; index < pGeneration.Count; index++)
            {
                accumulatedProbability += pGeneration[index].ReproductionProbability;
                if (!(random0To1Number <= accumulatedProbability)) continue;
                selectedIndex = index;
                break;
            }
            return pGeneration[selectedIndex];
        }

        private (Robot, Robot) ReproduceIndividuals(Robot pParentA, Robot pParentB)
        {
            int partitionIndex =
                MathematicalOperations.RandomIntegerInRange(0, Constants.CompleteChromosomeSize);
            
            Robot child1 = new Robot(pParentA, pParentB, partitionIndex);
            Robot child2 = new Robot(pParentB, pParentA, partitionIndex);

            return (child1, child2);
        }
        
    }
}