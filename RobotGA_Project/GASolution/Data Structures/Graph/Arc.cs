namespace RobotGA_Project.GASolution.Data_Structures.Graph
{
    public class Arc <T>
    {
        public float Weight { get; set; }
        public Node<T> PointA { get; set; }
        public Node<T> PointB { get; set; }
        
        public Arc(float pWeight ,Node<T> pPointA, Node<T> pPointB)
        {
            Weight = pWeight;
            PointA = pPointA;
            PointB = pPointB;
        }
        
    }
}