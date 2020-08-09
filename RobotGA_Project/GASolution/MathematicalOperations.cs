using System;

namespace RobotGA_Project.GASolution
{
    public static class MathematicalOperations
    {
        public static Random Random = new Random();

        public static int RandomIntegerInRange(int pLowerLimit, int pUpperLimit)
        {
            return Random.Next(pLowerLimit, pUpperLimit);
        }

        public static float Random0To1Float()
        {
            return (float)Random.NextDouble();
        }
        
        
        public static string ConvertIntToBinaryString(int pIntegerValue)
        {

            string binaryString = Convert.ToString(pIntegerValue, 2);

            while (binaryString.Length < Constants.ChromosomeSize)
            {
                binaryString = binaryString.Insert(0, "0");
            }
            return binaryString;
        }

        public static int ConvertBinaryStringToInt(string pBinaryValue)
        {
            return Convert.ToInt32(pBinaryValue, 2);
        }

        public static double DistanceBetweenPoints((int, int) a, (int, int) b) {
            return Math.Sqrt(Math.Pow(b.Item1 - a.Item1, 2) + Math.Pow(b.Item2 - a.Item2, 2));
        }


    }
}