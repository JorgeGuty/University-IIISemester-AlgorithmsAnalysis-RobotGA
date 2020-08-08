using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models
{
    public class RobotModel
    {
        [Required] public int Id { get; set; }
        
        [Required] public int GenerationId { get; set; }
        [Required][Display(Name = "Fitness")] public int Fitness { get; set; }
        [Required][Display(Name = "Costo")] public float Cost { get; set; }
        [Required][Display(Name = "Probabilidad de Reproducción")] public float ReproductionProbability { get; set; }
        [Required][Display(Name = "Ruta")] public List<(int,int)> Route { get; set; }
        [Required][Display(Name = "Hardware")] public HardwareModel Hardware { get; set; }
        [Required][Display(Name = "Software")] public SoftwareModel Software { get; set; }
    }
}