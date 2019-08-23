using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter your numbers. Current delimiters are \\n and comma.");
            var input = Console.ReadLine();
            var total = ParseInput(input);
            Console.WriteLine(total);
        }

        public static int ParseInput(string input)
        {
            var splitInputs = SplitInput(input);
            var numbers = SanitizeInput(splitInputs);
            var negativeNumbers = numbers.Where(x => x < 0).ToList();

            if (negativeNumbers.Count > 0)
            {
                string offendingNumbers = "";
                negativeNumbers.ForEach(x => offendingNumbers += x + " ");
                throw new ArgumentOutOfRangeException("input", $"Numbers must be positive: {offendingNumbers.Trim()}");
            }

            return numbers.Sum();
        }

        public static List<int> SanitizeInput(string[] splitInputs)
        {
            var numbers = new List<int>();

            foreach (var splitInput in splitInputs)
            {
                if (int.TryParse(splitInput, out int number))
                {
                    if (number > 1000)
                    {
                        numbers.Add(0);
                    }
                    else
                    {
                        numbers.Add(number);
                    }
                }
                else
                {
                    numbers.Add(0);
                }
            }

            return numbers;
        }

        public static string[] SplitInput(string input)
        {
            var delimiters = new List<string> { ",", "\\n" };
            string sequence = "//[";

            if (input.StartsWith(sequence))
            {
                delimiters.AddRange(ParseMultipleDelimiters(input));
            }
            else if (input.Length > 4 && input.StartsWith("//") && input[3] == '\\' && input[4] == 'n')
            {
                delimiters.Add(input[2].ToString());
                input = input.Substring(5, input.Length - 5);
            }


            return input.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private static List<string> ParseMultipleDelimiters(string input)
        {
            List<string> delimiters = new List<string>();
            var regex = new Regex(@"\[(.*?)\]");

            foreach (Match match in regex.Matches(input))
            {
                if (match.Groups.Count > 0)
                {
                    delimiters.Add(match.Groups[1].Value);
                }
            }

            return delimiters;
        }
    }
}