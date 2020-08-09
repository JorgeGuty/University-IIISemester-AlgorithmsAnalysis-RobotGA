using System.ComponentModel.DataAnnotations;

namespace RobotGA_Project.Models
{
    public class SoftwareModel
    {
        [Required][Display(Name = "Move Towards Goal")] public int MoveTowardsEnd { get; set; }
        [Required][Display(Name = "Chromosome")] public string MoveTowardsEndChromosome { get; set; }
        
        [Required][Display(Name = "Move Away From Goal")] public int MoveAwayFromEnd { get; set; }
        [Required][Display(Name = "Chromosome")] public string MoveAwayFromEndChromosome { get; set; }
        
        [Required][Display(Name = "Move to Passable Terrain")] public int MoveToPassableTerrain { get; set; }
        [Required][Display(Name = "Chromosome")] public string MoveToPassableTerrainChromosome { get; set; }
        
        [Required][Display(Name = "Move to NON Passable Terrain")] public int MoveToNonPassableTerrain { get; set; }
        [Required][Display(Name = "Chromosome")] public string MoveToNonPassableTerrainChromosome { get; set; }
        
        [Required][Display(Name = "Spend the Less Energy")] public int SpendTheLessEnergy { get; set; }
        [Required][Display(Name = "Chromosome")] public string SpendTheLessEnergyChromosome { get; set; }
        
        [Required][Display(Name = "Spend the Most Energy")] public int SpendTheMostEnergy { get; set; }
        [Required][Display(Name = "Chromosome")] public string SpendTheMostEnergyChromosome { get; set; }
        
        [Required][Display(Name = "Spend Normal Energy")] public int SpendNormalEnergy { get; set; }
        [Required][Display(Name = "Chromosome")] public string SpendNormalEnergyChromosome { get; set; }
    }
}