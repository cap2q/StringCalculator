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
            for (; ; )
            {
                Console.WriteLine("Enter your numbers. Default delimiters are \\n and comma. Custom delimiters are enabled.");
                var input = Console.ReadLine();
                var parsedInput = ParseInput(input);
                Console.WriteLine("Select an operation: + * / -");
                var operation = Console.ReadLine();
                CalculateReport(operation, parsedInput);
            }
        }
        public static List<int> ParseInput(string input)
        {
            var splitInputs = SplitInput(input);
            var numbers = SanitizeInput(splitInputs);
            var negativeNumbers = numbers.Where(x => x < 0).ToList();

            if (negativeNumbers.Count > 0)
            {
                var offendingNumbers = "";
                negativeNumbers.ForEach(x => offendingNumbers += x + " ");
                throw new ArgumentOutOfRangeException("input", $"Numbers must be positive: {offendingNumbers.Trim()}");
            }

            return numbers;
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
            }

            return numbers;
        }

        public static string[] SplitInput(string input)
        {
            var delimiters = new List<string> { ",", "\\n" };
            var sequence = "//[";

            if (input.StartsWith(sequence))
            {
                delimiters.AddRange(ParseMultipleDelimiters(input));
                var index = input.IndexOf("\\n");
                input = input.Substring(index + 2, input.Length - index - 2);
            }
            else if (input.Length > 4 && input.StartsWith("//") && input[3] == '\\' && input[4] == 'n')
            {
                delimiters.Add(input[2].ToString());
                input = input.Substring(5, input.Length - 5);
            }

            return input.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<string> ParseMultipleDelimiters(string input)
        {
            var delimiters = new List<string>();
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

        public static int CalculateReport(string operation, List<int> inputs)
        {
            var total = 0;
            Console.WriteLine();

            foreach (var input in inputs)
            {
                if (inputs.IndexOf(input) == inputs.Count() - 1)
                {
                    Console.Write($"{input} = ");
                }
                else
                {
                    Console.Write($"{input} {operation} ");
                }
            }

            foreach (var input in inputs)
            {
                switch (operation)
                {
                    case "+":
                        total = inputs.Sum();
                        break;

                    case "*":
                        if (inputs.IndexOf(input) == 0)
                        {
                            total = 1;
                        }
                        total *= input;
                        break;

                    case "-":
                        total = inputs.IndexOf(input) == 0 ? input : total -= input;
                        break;

                    case "/":
                        if (inputs.IndexOf(input) == 0)
                        {
                            total = input;
                        }
                        else
                        {
                            if (input != 0)
                            {
                                total /= input;
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid Operation.");
                        break;
                }
            }

            Console.WriteLine(total);
            return total;
        }
    }
}


