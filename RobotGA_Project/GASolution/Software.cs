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
            var moveAwayFromEndChromosome =
                CompleteChromosome.Substring(Constants.ChromosomeSize, Constants.ChromosomeSize);
            var moveToPassableTerrainChromosome =
                CompleteChromosome.Substring(2 * Constants.ChromosomeSize, Constants.ChromosomeSize);
            var moveToNonPassableTerrainChromosome = 
                CompleteChromosome.Substring(3 * Constants.ChromosomeSize, Constants.ChromosomeSize);
            var spendTheLessEnergyChromosome =
                CompleteChromosome.Substring(4 * Constants.ChromosomeSize, Constants.ChromosomeSize);
            var spendTheMostEnergyChromosome =
                CompleteChromosome.Substring(5 * Constants.ChromosomeSize, Constants.ChromosomeSize);
            var spendNormalEnergyChromosome = 
                CompleteChromosome.Substring(6 * Constants.ChromosomeSize, Constants.ChromosomeSize);

            MoveTowardsEnd = MathematicalOperations.ConvertBinaryStringToInt(moveTowardsEndChromosome);
            MoveAwayFromEnd = MathematicalOperations.ConvertBinaryStringToInt(moveAwayFromEndChromosome);
            MoveToPassableTerrain = MathematicalOperations.ConvertBinaryStringToInt(moveToPassableTerrainChromosome);
            MoveToNonPassableTerrain = MathematicalOperations.ConvertBinaryStringToInt(moveToNonPassableTerrainChromosome);
            SpendTheLessEnergy = MathematicalOperations.ConvertBinaryStringToInt(spendTheLessEnergyChromosome);
            SpendTheMostEnergy = MathematicalOperations.ConvertBinaryStringToInt(spendTheMostEnergyChromosome);
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
            MoveAwayFromEnd = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            
            var moveTowardsEndChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveTowardsEnd);
            var moveAwayFromEndChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveAwayFromEnd);

            var movingModuleChromosome = moveTowardsEndChromosome + moveAwayFromEndChromosome;
            
            MoveToPassableTerrain = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            MoveToNonPassableTerrain = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            
            var moveToPassableTerrainChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveToPassableTerrain);
            var moveToNonPassableTerrainChromosome = MathematicalOperations.ConvertIntToBinaryString(MoveToNonPassableTerrain);

            var terrainModuleChromosome = moveToPassableTerrainChromosome + moveToNonPassableTerrainChromosome;
            
            SpendTheLessEnergy = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            SpendTheMostEnergy = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);
            SpendNormalEnergy = MathematicalOperations.RandomIntegerInRange(minValue, maxValue);

            var spendTheLessEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(SpendTheLessEnergy);
            var spendTheMostEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(SpendTheMostEnergy);
            var spendNormalEnergyChromosome = MathematicalOperations.ConvertIntToBinaryString(SpendNormalEnergy);

            var energyModuleChromosome =
                spendTheLessEnergyChromosome + spendTheMostEnergyChromosome + spendNormalEnergyChromosome;

            CompleteChromosome = movingModuleChromosome + terrainModuleChromosome + energyModuleChromosome;

        }
        
        public void Mutate(string pMutatedChromosome)
        {
            
            CompleteChromosome = pMutatedChromosome;
            
            SetGenotypes();
            
        }
        
    }
}