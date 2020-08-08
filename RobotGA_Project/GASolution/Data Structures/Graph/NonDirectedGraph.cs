using System;
using System.Collections.Generic;

namespace RobotGA_Project.GASolution.Data_Structures.Graph
{
    public class NonDirectedGraph <T>
    {
        private List<Node<T>> Nodes { get; set; }
        private List<Arc<T>> Arcs { get; set; }

        public NonDirectedGraph()
        {
            Nodes = new List<Node<T>>();
            Arcs = new List<Arc<T>>();
        }

        public void AddNode(Node<T> pNewNode)
        {
            if (!Nodes.Contains(pNewNode))
            {
                Nodes.Add(pNewNode);
            }
        }
        
        public void AddArc(float pWeight, Node<T> pNodeA, Node<T> pNodeB)
        {
            if (Nodes.Contains(pNodeA) && Nodes.Contains(pNodeB))
            {
                Arc<T> newArc = new Arc<T>(pWeight,pNodeA,pNodeB);
                Arcs.Add(newArc);
                pNodeA.AddArc(pNodeB);
                pNodeB.AddArc(pNodeA);
            }
        }

        public float GetWeightOfArc(Node<T> pNodeA, Node<T> pNodeB)
        {
            foreach (var arc in Arcs)
            {
                if ((arc.PointA == pNodeA && arc.PointB == pNodeB) || (arc.PointA == pNodeB && arc.PointB == pNodeA))
                {
                    return arc.Weight;
                }  
            }
            return float.MaxValue;
        }
        
    }
}