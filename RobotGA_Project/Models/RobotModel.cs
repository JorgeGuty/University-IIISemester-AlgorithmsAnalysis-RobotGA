using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models
{
    public class RobotModel
    {
        [Required] 
        public int Id { get; set; }
        [Required] 
        public int GenerationId { get; set; }
        [Display(Name = "ParentA")] 
        public RobotModel ParentA { get; set; }
        [Display(Name = "ParentB")] 
        public RobotModel ParentB { get; set; }
        [Required][Display(Name = "Fitness")] 
        public int Fitness { get; set; }
        [Required][Display(Name = "Cost")] 
        public float Cost { get; set; }
        [Required][Display(Name = "Reproduction Probability")] 
        public float ReproductionProbability { get; set; }
        [Required][Display(Name = "Route")] 
        public List<(int,int)> Route { get; set; }
        [Required][Display(Name = "Hardware")] 
        public HardwareModel Hardware { get; set; }
        [Required][Display(Name = "Software")] 
        public SoftwareModel Software { get; set; }
    }
}