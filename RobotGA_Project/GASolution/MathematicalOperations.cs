using System;
using System.Collections.Generic;
using System.Linq;

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

        public static float StandardDeviation(List<int> pPopulation)
        {
            var quantity = pPopulation.Count;
            return (float)Math.Sqrt(Variance(pPopulation, quantity));
        }

        public static float Variance(List<int> pPopulation, int pQuantity)
        {
            var average = Average(pPopulation, pQuantity);
            var summation = 0f;
            foreach (var value in pPopulation)
            {
                var difference = value - average;
                summation += (float)Math.Pow(difference, 2);
            }
            return summation / pQuantity;
        }

        public static float Average(List<int> pPopulation, int pQuantity)
        {
            var summation = pPopulation.Sum();
            return (float) summation / pQuantity;
        }

        public static int CountAppearances<T>(List<T> pList, T pElement)
        {
            var count = 0;
            foreach (var element in pList)
            {
                if (element.Equals(pElement)) count++;
            }

            return count;
        }


    }
}