using System.ComponentModel.DataAnnotations;

namespace RobotGA_Project.Models
{
    public class HardwareModel
    {
        [Required][Display(Name = "Rango")] public int CameraRange { get; set; }
        [Required][Display(Name = "Genotipo")] public int CameraGenotype { get; set; }
        [Required][Display(Name = "Cromosoma")] public string CameraChromosome { get; set; }
        [Required][Display(Name = "Carga Máx")] public int BatteryEnergy { get; set; }
        [Required][Display(Name = "Genotipo")] public int BatteryGenotype { get; set; }
        [Required][Display(Name = "Cromosoma")] public string BatteryChromosome { get; set; }
        [Required][Display(Name = "Dificultad Máxima")] public int EngineCapacity { get; set; }
        [Required][Display(Name = "Genotipo")] public int EngineGenotype { get; set; }
        [Required][Display(Name = "Cromosoma")] public string EngineChromosome { get; set; }
    }
}