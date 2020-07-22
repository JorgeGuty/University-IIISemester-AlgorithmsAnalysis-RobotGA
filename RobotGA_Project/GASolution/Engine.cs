namespace RobotGA_Project.GASolution
{
    public class Engine
    {
        
        public int MaxTerrainDifficulty { get; set; }
        

        public Engine(Terrain pMaxTerrainDifficulty)
        {
            MaxTerrainDifficulty = pMaxTerrainDifficulty.DifficultyLevel;
        }
        
    }
}