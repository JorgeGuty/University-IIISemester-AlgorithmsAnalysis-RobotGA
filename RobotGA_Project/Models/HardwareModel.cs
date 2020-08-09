using System.ComponentModel.DataAnnotations;

namespace RobotGA_Project.Models
{
    public class HardwareModel
    {
        [Required] [Display(Name = "Camera Range")] public int CameraRange { get; set; }
        [Required] public int CameraGenotype { get; set; }
        [Required] public string CameraChromosome { get; set; }
        [Required] [Display(Name = "Battery Energy")] public int BatteryEnergy { get; set; }
        [Required] public int BatteryGenotype { get; set; }
        [Required] public string BatteryChromosome { get; set; }
        [Required] [Display(Name = "Engine Capacity")] public int EngineCapacity { get; set; }
        [Required] public int EngineGenotype { get; set; }
        [Required] public string EngineChromosome { get; set; }
    }
}