using System.Collections.Generic;

namespace RobotGA_Project.GASolution
{
    public class Generation
    {
        /*
         * Class that stores and manages the reproduction, mutation and fitness of a population.
         */
        private List<Robot> Population { get; }

        public Generation(List<Robot> pLastGeneration)
        {

            Population = Reproduct(pLastGeneration);

        }

        private List<Robot> Reproduct(List<Robot> pLastGeneration)
        {
            throw new System.NotImplementedException();
        }
        
        
        
    }
}