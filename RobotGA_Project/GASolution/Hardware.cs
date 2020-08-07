namespace RobotGA_Project.GASolution
{
    public class Hardware
    {
        
        public Battery Battery { get; set; }
        public Camera Camera { get; set; }
        public Engine Engine { get; set; }
        
        public int BatteryGenotype { get; set; }
        public int CameraGenotype { get; set; }
        public int EngineGenotype { get; set; }
        
        public string CompleteChromosome { get; set; }
        
        public int Cost { get; set; }

        public Hardware(string pChromosomeA, string pChromosomeB, int pPartitionIndex)
        {
            /*
             *  Initializes a hardware system based on two past hardware systems.
             */
            
            CompleteChromosome = GeneticOperations.MixGeneticMaterial(pChromosomeA, pChromosomeB, pPartitionIndex);
            SetGenotypes();
            InitializeFields();
        }

        public Hardware()
        {
            
            /*
             * Initializes a hardware system with random initial values.
             */
            
            CameraGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);
            BatteryGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;
            EngineGenotype = MathematicalOperations.RandomIntegerInRange(Constants.GenotypeMinvalue, Constants.GenotypeMaxValue);;

            string batteryChromosome = MathematicalOperations.ConvertIntToBinaryString(BatteryGenotype);
            string cameraChromosome = MathematicalOperations.ConvertIntToBinaryString(CameraGenotype);
            string engineChromosome = MathematicalOperations.ConvertIntToBinaryString(EngineGenotype);

            // IMPORTANT: The order of the chromosomes in the complete chromosome must stay the same !!!
            CompleteChromosome = batteryChromosome + cameraChromosome + engineChromosome;

            InitializeFields();

        }
        
        private void InitializeFields()
        {
            int minValue = Constants.GenotypeMinvalue;
            int maxValue = Constants.GenotypeMaxValue;
            
            // With the given genotypes, gets the phenotype represented by each of them.
            SetBattery(minValue,maxValue);
            SetCamera(minValue,maxValue);
            SetEngine(minValue,maxValue);

            // Add the costs of all pieces of hardware to get the total cost of the hardware system.
            Cost = Battery.Cost + Engine.Cost + Camera.Cost;
        }
        
        private void SetGenotypes()
        {
            
            string batteryChromosome = CompleteChromosome.Substring(0, Constants.ChromosomeSize);
            string cameraChromosome =
                CompleteChromosome.Substring(Constants.ChromosomeSize, Constants.ChromosomeSize);
            string engineChromosome =
                CompleteChromosome.Substring(2 * Constants.ChromosomeSize, Constants.ChromosomeSize);

            BatteryGenotype = MathematicalOperations.ConvertBinaryStringToInt(batteryChromosome);
            CameraGenotype = MathematicalOperations.ConvertBinaryStringToInt(cameraChromosome);
            EngineGenotype = MathematicalOperations.ConvertBinaryStringToInt(engineChromosome);
            
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
        
        public void Mutate(string pMutatedChromosome)
        {
            int minValue = Constants.GenotypeMinvalue;
            int maxValue = Constants.GenotypeMaxValue;
            
            CompleteChromosome = pMutatedChromosome;
            
            SetGenotypes();
            
            SetBattery(minValue,maxValue);
            SetCamera(minValue,maxValue);
            SetEngine(minValue,maxValue);
        }

    }
}