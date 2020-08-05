using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotGA_Project.GASolution
{
    public static class GeneticOperations
    {
        public static string MixGeneticMaterial(string pChromosomeA, string pChromosomeB, int pPartitionIndex)
        {
            
            string chromosomeAPart = pChromosomeA.Substring(0, pPartitionIndex);

            string chromosomeBPart = pChromosomeB.Substring(pPartitionIndex, Constants.CompleteChromosomeSize - pPartitionIndex);

            string childChromosome = chromosomeAPart + chromosomeBPart;
            
            return childChromosome;
            
        }
        
        public static string Mutate(int pMutationIndex, string pChromosome)
        {
            /*
             * Function that mutates a bit from the genotype 
             */
            
            StringBuilder mutator = new StringBuilder(pChromosome);
            if (pChromosome[pMutationIndex].Equals('1'))
            {
                mutator[pMutationIndex] = '0';
            }
            else
            {
                mutator[pMutationIndex] = '1';
            }
        
            pChromosome = mutator.ToString();

            return pChromosome;
            
        }

        public static int NormalizeFitnessScore(int pFitnessScore, int pMaxInThatCategory)
        {
            var portionOfEachCriteria = 100 / Constants.FitnessCriteriaQuantity;
            var normalizedScore = pFitnessScore * portionOfEachCriteria / pMaxInThatCategory;
            
            return normalizedScore;
        }

        public static void SetGenerationReproductionProbabilities(List<Robot> pGeneration)
        {
            var fitnessTotal = AddGenerationFitnessScores(pGeneration);
            foreach (var robot in pGeneration)
            {
                robot.ReproductionProbability = (float) robot.Fitness / fitnessTotal;
            }
        }

        private static int AddGenerationFitnessScores(List<Robot> pGeneration)
        {
            /*
             *  Adds the fitness of all the individuals together
             */

            return pGeneration.Sum(robot => robot.Fitness);
            
        }
    }
}






















