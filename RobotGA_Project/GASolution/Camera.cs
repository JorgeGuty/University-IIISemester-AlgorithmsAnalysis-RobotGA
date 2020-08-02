namespace RobotGA_Project.GASolution
{
    public class Camera
    {
        public int Range { get; set; }
        public int EnergyConsumption { get; set; }
        
        public int Cost { get; set; }

        public Camera(int pRange, int pEnergyConsumption, int pCost)
        {
            Range = pRange;
            EnergyConsumption = pEnergyConsumption;
            Cost = pCost;
        }
        
    }
}