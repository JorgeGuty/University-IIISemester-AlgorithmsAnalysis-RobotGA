using System.ComponentModel.DataAnnotations;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models
{
    public class GenerationModel
    {
        [Required] public int Id { get; set; }
        [Required] public Generation Generation { get; set; }
    }
}