using System;
using System.Collections.Generic;

namespace RobotGA_Project.GASolution.Data_Structures.Graph
{
    public class Node <T>
    {
        private List<Node<T>> Connections { get; set; }
        private T Object { get; set; }
        private string Id { get; set; }

        public Node(T pObject)
        {
            Object = pObject;
            Connections = new List<Node<T>>();
            Id = Guid.NewGuid().ToString("N");
        }

        public bool AddArc(Node<T> pNode)
        {
            try
            {
                Connections.Add(pNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}