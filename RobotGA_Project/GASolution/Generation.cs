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
        public float FitnessStandardDeviation { get; set; }

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
                Population.Add(new Robot(size));
            }
            FitGeneration();
        }
        
        private List<Robot> Breed(List<Robot> pLastGeneration)
        {
            List<Robot> newGeneration = new List<Robot>();
            
            //GeneticOperations.SetGenerationReproductionProbabilities(pLastGeneration);

            while (newGeneration.Count < Constants.PopulationSize)
            {
                Robot parentA = SelectMatingPartner(pLastGeneration);
                Robot parentB = SelectMatingPartner(pLastGeneration);

                var (child1, child2) = ReproduceIndividuals(parentA, parentB, newGeneration.Count);

                newGeneration.Add(child1);
                newGeneration.Add(child2);
            }

            return newGeneration;
        }

        private void FitGeneration()
        {
            var fitnessList = new List<int>();
            foreach (var robot in Population)
            {
                robot.CalculateFitness();
                fitnessList.Add(robot.Fitness);
            }
            GeneticOperations.SetGenerationReproductionProbabilities(Population);
            FitnessStandardDeviation = MathematicalOperations.StandardDeviation(fitnessList);
        }

        private void GenerationalMutation()
        {
            /*
             *  Mutates a bit from random individuals from the population.
             *  Number of mutations dictated in Constants.cs
             */
            int individualIndex;
            int bitIndex;
            string completeChromosome;
            int softwareOrHardwareMutate;

            for (int times = 0; times < Constants.MutationProbability; times++)
            {
                individualIndex = 
                    MathematicalOperations.RandomIntegerInRange(0, Constants.PopulationSize);
                softwareOrHardwareMutate = MathematicalOperations.RandomIntegerInRange(0, 2);
                if (softwareOrHardwareMutate == 1)
                {
                    bitIndex = 
                        MathematicalOperations.RandomIntegerInRange(0, Constants.CompleteChromosomeSize);
                    completeChromosome = Population[individualIndex].Hardware.CompleteChromosome;
                    completeChromosome =
                        GeneticOperations.Mutate(bitIndex, completeChromosome);
                    Population[individualIndex].Hardware.Mutate(completeChromosome);
                }
                else
                {
                    bitIndex = 
                        MathematicalOperations.RandomIntegerInRange(0, Constants.SoftwareChromosomeSize);
                    completeChromosome = Population[individualIndex].Software.CompleteChromosome;
                    GeneticOperations.Mutate(bitIndex, completeChromosome);
                    Population[individualIndex].Software.Mutate(completeChromosome);
                }
            }
        }
        
        private Robot SelectMatingPartner(List<Robot> pGeneration)
        {
            var random0To1Number = MathematicalOperations.Random0To1Float();
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

        private (Robot, Robot) ReproduceIndividuals(Robot pParentA, Robot pParentB, int pId)
        {
            int hardwarePartitionIndex =
                MathematicalOperations.RandomIntegerInRange(1, Constants.CompleteChromosomeSize);
            int softwarePartitionIndex = 
                MathematicalOperations.RandomIntegerInRange(1, Constants.SoftwareChromosomeSize);
            
            Robot child1 = new Robot(pParentA, pParentB, hardwarePartitionIndex, softwarePartitionIndex, pId);
            Robot child2 = new Robot(pParentB, pParentA, hardwarePartitionIndex, softwarePartitionIndex,pId + 1);

            return (child1, child2);
        }
    }
}