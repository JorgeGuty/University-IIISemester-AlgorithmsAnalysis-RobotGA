namespace RobotGA_Project.GASolution
{
    public class Software
    {
        // Moving module
        public int MoveTowardsEnd { get; set; }
        public int MoveAwayFromEnd { get; set; }
        
        // Terrain module

        public int MoveToPassableTerrain { get; set; }
        public int MoveToNonPassableTerrain { get; set; }

        
        // Energy Module

        public int SpendTheLessEnergy { get; set; }
        public int SpendTheMostEnergy { get; set; }
        public int SpendNormalEnergy { get; set; }

        public string CompleteChromosome { get; set; }
        
        public Software(string pChromosomeA, string pChromosomeB, int pPartitionIndex)
        {
            CompleteChromosome = GeneticOperations.MixGeneticMaterial(pChromosomeA, pChromosomeB, pPartitionIndex);
            SetGenotypes();
        }

        private void SetGenotypes()
        {
            var moveTowardsEndChromosome = 
                CompleteChromosome.Substring(0, Constants.ChromosomeSize);
            
            var moveToPassableTerrainChromosome =
                CompleteChromosome.Substring(1 * Constants.ChromosomeSize, Constants.ChromosomeSize);

            var spendTheLessEnergyChromosome =
                CompleteChromosome.Substring(2 * Constants.ChromosomeSize, Constants.ChromosomeSize);
            
            var spendNormalEnergyChromosome = 
                CompleteChromosome.Substring(3 * Constants.ChromosomeSize, Constants.ChromosomeSize);

            MoveTowardsEnd = MathematicalOperations.ConvertBinaryStringToInt(moveTowardsEndChromosome);
            MoveAwayFromEnd = (Constants.GenotypeMaxValue-1) - MoveTowardsEnd;
            MoveToPassableTerrain = MathematicalOperations.ConvertBinaryStringToInt(moveToPassableTerrainChromosome);
            MoveToNonPassableTerrain = (Constants.GenotypeMaxValue-1) - MoveToPassableTerrain;
            SpendTheLessEnergy = MathematicalOperations.ConvertBinaryStringToInt(spendTheLessEnergyChromosome);
            SpendTheMostEnergy = (Constants.GenotypeMaxValue-1) - SpendTheLessEnergy;
            SpendNormalEnergy = MathematicalOperations.ConvertBinaryStringToInt(spendNormalEnergyChromosome);
        }


        public Software() {
            
            /*
             * Initializes a software system with random values.
             * The order of the CompleteChromosome matters.
             * It is the following:
             * 1) MoveTowardsEnd chromosome
             * 2) MoveAwayFromEnd chromosome
             * 3) MoveToPassableTerrain chromosome
             * 4) MoveToNonPassableTerrain chromosome
             * 5) SpendTheLessEnergy chromosome
             * 6) SpendTheMostEnergy chromosome
             * 7) SpendNormalEnergy chromosome
             */
            var minValue = Constants.GenotypeMinvalue;
            var maxValue = Constants.GenotypeMaxValue;
            
            MoveTowardsEnd = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            var moveTowardsEndChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveTowardsEnd);
            MoveAwayFromEnd = (Constants.GenotypeMaxValue-1) - MoveTowardsEnd;
            MoveToPassableTerrain = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            var moveToPassableTerrainChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveToPassableTerrain);
            MoveToNonPassableTerrain = (Constants.GenotypeMaxValue-1) - MoveToPassableTerrain;
            SpendTheLessEnergy = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            var spendTheLessEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(SpendTheLessEnergy);
            SpendTheMostEnergy = (Constants.GenotypeMaxValue-1) - SpendTheLessEnergy;
            SpendNormalEnergy = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            var spendNormalEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(SpendNormalEnergy);
            
            CompleteChromosome = moveTowardsEndChromosome + moveToPassableTerrainChromosome + spendTheLessEnergyChromosome + spendNormalEnergyChromosome;

        }
        
        public void Mutate(string pMutatedChromosome)
        {
            
            CompleteChromosome = pMutatedChromosome;
            
            SetGenotypes();
            
        }
        
    }
}