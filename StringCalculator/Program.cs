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
            var delimiters = new string[] { ",", "\\n" };
            var splitInputs = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var numbers = new List<int>();
            var negativeNumbers = new List<int>();

            foreach (var splitInput in splitInputs)
            {
                if (int.TryParse(splitInput, out int number))
                {
                    if (IsValidNumber(number))
                    {
                        numbers.Add(number);
                    }
                    else
                    {
                        negativeNumbers.Add(number);
                    }
                }
                else
                {
                    numbers.Add(0);
                }
            }

            if (negativeNumbers.Count > 0)
            {
                string offendingNumbers = "";
                negativeNumbers.ForEach(x => offendingNumbers += x + " ");
                throw new ArgumentOutOfRangeException("input", $"Numbers must be positive: {offendingNumbers.Trim()}");
            }

            return numbers.Sum();
        }

        public static bool IsValidNumber(int number)
        {
            return number > 0;
        }
    }
}