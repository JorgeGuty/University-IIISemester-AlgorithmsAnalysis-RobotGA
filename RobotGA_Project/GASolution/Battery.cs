namespace RobotGA_Project.GASolution
{
    public class Battery
    {
        public int Energy { get; set; }
        public int Cost { get; set; }

        public Battery(int pEnergy, int pCost)
        {
            Energy = pEnergy;
            Cost = pCost;
        }
    }
}