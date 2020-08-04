using System.Text;

namespace RobotGA_Project.GASolution
{
    public static class GeneticOperations
    {
        public static string MixGeneticMaterial(string pChromosomeA, string pChromosomeB, int pPartitionIndex)
        {

            int completeChromosomeSize = Constants.ChromosomeSize * Constants.ChromosomeQuantity;

            string chromosomeAPart = pChromosomeA.Substring(0, pPartitionIndex);

            string chromosomeBPart = pChromosomeB.Substring(pPartitionIndex, completeChromosomeSize - pPartitionIndex);

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
            int portionOfEachCriteria = 100 / Constants.FitnessCriteriaQuantity;
            int normalizedScore = pFitnessScore * portionOfEachCriteria / pMaxInThatCategory;
            
            return normalizedScore;
            
        }
        
    }
}