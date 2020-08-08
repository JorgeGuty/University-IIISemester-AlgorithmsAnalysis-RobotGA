namespace RobotGA_Project.GASolution
{
    public class Terrain
    {
        public int DifficultyLevel { get; set; }
        public int EnergyConsumption { get; set; }

        public string Id { get; }

        public Terrain(int pDifficultyLevel, int pEnergyConsumption, string id)
        {
            DifficultyLevel = pDifficultyLevel;
            EnergyConsumption = pEnergyConsumption;
            Id = id;
        }
        
        
        
    }
}