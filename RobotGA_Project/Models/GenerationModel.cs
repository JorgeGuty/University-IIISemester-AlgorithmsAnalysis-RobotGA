using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models
{
    public class GenerationModel
    {
        [Required] 
        [Display(Name = "ID")] 
        public int Id { get; set; }
        
        [Required] 
        [Display(Name = "Fitness Standard Deviation")]
        public float FitnessStandardDeviation { get; set; }
        
        [Required] public List<RobotModel> Population { get; set; }
        
        
    }
}