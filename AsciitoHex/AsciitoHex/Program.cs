using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1234
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "55010711";
            char[] charValues = str.ToCharArray();
            string hexOutput = "";
            foreach (char _eachChar in charValues)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(_eachChar);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += String.Format("{0:X}", value);
                
                // to make output as your eg 
                //  hexOutput +=" "+ String.Format("{0:X}", value);
            }
            Console.Write(hexOutput);
        }
    }
}


