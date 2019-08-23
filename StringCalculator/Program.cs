using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter two numbers, with a comma as a delimiters. Example: 1,2");
            var input = Console.ReadLine();
            var total = ParseInput(input);
            Console.WriteLine(total);
        }

        public static int ParseInput(string input)
        {
            var splitInputs = input.Split(',');

            if (splitInputs.Length > 2)
            {
                throw new ArgumentException("Only two numbers can be used.");
            }

            var numbers = new List<int>();

            foreach (var splitInput in splitInputs)
            {
                if (int.TryParse(splitInput, out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    numbers.Add(0);
                }
            }

            return numbers.Sum();
        }
    }
}