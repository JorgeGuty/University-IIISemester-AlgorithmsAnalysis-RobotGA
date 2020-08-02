namespace RobotGA_Project.GASolution
{
    public class Engine
    {
        
        public int MaxTerrainDifficulty { get; set; }
        public int Cost { get; set; }
        

        public Engine(Terrain pMaxTerrainDifficulty, int pCost)
        {
            MaxTerrainDifficulty = pMaxTerrainDifficulty.DifficultyLevel;
            Cost = pCost;
        }
        
    }
}