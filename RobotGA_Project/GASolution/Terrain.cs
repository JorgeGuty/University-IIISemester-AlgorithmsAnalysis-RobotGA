namespace RobotGA_Project.GASolution
{
    public class Terrain
    {
        public int DifficultyLevel { get; set; }
        public int EnergyConsumption { get; set; }

        public Terrain(int pDifficultyLevel, int pEnergyConsumption)
        {
            DifficultyLevel = pDifficultyLevel;
            EnergyConsumption = pEnergyConsumption;
        }
        
        
        
    }
}