using System.ComponentModel.DataAnnotations;

namespace RobotGA_Project.Models
{
    public class SoftwareModel
    {
        [Required][Display(Name = "Acercarse a la meta")] public int MoveTowardsEnd { get; set; }
        [Required][Display(Name = "Cromosoma")] public string MoveTowardsEndChromosome { get; set; }
        
        [Required][Display(Name = "Alejarse de la meta ")] public int MoveAwayFromEnd { get; set; }
        [Required][Display(Name = "Cromosoma")] public string MoveAwayFromEndChromosome { get; set; }
        
        [Required][Display(Name = "Moverse a terreno transitable")] public int MoveToPassableTerrain { get; set; }
        [Required][Display(Name = "Cromosoma")] public string MoveToPassableTerrainChromosome { get; set; }
        
        [Required][Display(Name = "Moverse a terreno NO transitable")] public int MoveToNonPassableTerrain { get; set; }
        [Required][Display(Name = "Cromosoma")] public string MoveToNonPassableTerrainChromosome { get; set; }
        
        [Required][Display(Name = "Gastar menor cantidad de energía")] public int SpendTheLessEnergy { get; set; }
        [Required][Display(Name = "Cromosoma")] public string SpendTheLessEnergyChromosome { get; set; }
        
        [Required][Display(Name = "Gastar mayor cantidad de energía")] public int SpendTheMostEnergy { get; set; }
        [Required][Display(Name = "Cromosoma")] public string SpendTheMostEnergyChromosome { get; set; }
        
        [Required][Display(Name = "Ni mayor ni menor cantidad de energía")] public int SpendNormalEnergy { get; set; }
        [Required][Display(Name = "Cromosoma")] public string SpendNormalEnergyChromosome { get; set; }
    }
}