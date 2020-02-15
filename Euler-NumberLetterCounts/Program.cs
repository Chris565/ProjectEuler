using System;
using System.Collections.Generic;
using System.Text;

namespace Euler_NumberLetterCounts
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseHolderToText = new Dictionary<int, string>
            {
                {4, "thousand" }, {7, "million" }, {10, "billion" }
            };

            var digitsToText = new Dictionary<int, string>
            {
                {0, "" }, {1, "one" }, {2, "two" }, {3, "three" }, {4, "four" }, {5, "five" }, {6, "six" }, {7, "seven" }, {8, "eight" }, {9, "nine" },
                {10, "ten" }, {11, "eleven" }, {12, "twelve" }, {13, "thirteen" }, {14, "fourteen" }, {15, "fifteen"}, {16, "sixteen"}, {17, "seventeen"},
                {18, "eighteen" }, {19, "nineteen"}, {20, "twenty"}, {30, "thirty"}, {40, "forty"}, {50, "fifty"}, {60, "sixty"}, {70, "seventy"}, 
                {80, "eighty"}, {90, "ninety"}
            };

            DisplayInfo();

            var startingNum = 0;
            var endingNum = 0;

            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter starting value: ");
                Console.ForegroundColor = ConsoleColor.White;
                startingNum = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nEnter ending value: ");
                Console.ForegroundColor = ConsoleColor.White;
                endingNum = int.Parse(Console.ReadLine());

                if (ValidateInput(startingNum) == false)
                    throw new ArgumentException(string.Format("Starting value {0} not in accepted range. Ending value not considered.", startingNum.ToString()));

                if (ValidateInput(endingNum) == false)
                    throw new ArgumentException(string.Format("Ending value {0} not in accepted range.", endingNum.ToString()));

                if (endingNum < startingNum)
                    throw new ArgumentException(string.Format("Ending value {0} is not greater than starting value {1}", endingNum.ToString(), startingNum.ToString()));

                var runningTotal = 0;
                for(int i = startingNum; i < endingNum + 1; i++)
                {
                    var digits = IntToArray(i);
                    var numText = new StringBuilder();
                    for (int d = 0; d < digits.Length; d++)
                    {
                        var distanceFromRight = digits.Length - d;

                        if(digits.Length > 1)
                        {
                            if (digits[d] == 0)
                            {
                                numText.Append(digitsToText[digits[d]]);

                                if (baseHolderToText.ContainsKey(distanceFromRight))
                                    numText.Append(baseHolderToText[distanceFromRight]);
                            }
                            else
                            {
                                if (distanceFromRight % 3 == 0)
                                {
                                    numText.Append(digitsToText[digits[d]]);
                                    numText.Append("hundred");

                                    if (digits[d + 1] > 0 || digits[d + 2] > 0)
                                        numText.Append("and");
                                }

                                if (distanceFromRight % 3 == 2)
                                {
                                    if (digits[d] == 1)
                                        numText.Append(digitsToText[(digits[d] * 10) + digits[d + 1]]);
                                    else
                                    {
                                        numText.Append(digitsToText[digits[d] * 10]);
                                        numText.Append(digitsToText[digits[d + 1]]);
                                    }
                                }

                                var leftNumeral = d - 1 < 0 ? 99 : digits[d - 1];
                                if (distanceFromRight % 3 == 1 && leftNumeral == 0)
                                    numText.Append(digitsToText[digits[d]]);

                                if (baseHolderToText.ContainsKey(distanceFromRight))
                                {
                                    numText.Append(digitsToText[digits[d]]);
                                    numText.Append(baseHolderToText[distanceFromRight]);
                                }
                            }
                        }
                        else
                            numText.Append(digitsToText[digits[d]]);
                    }
                    runningTotal += numText.Length;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n[TOTAL LETTERS USED] {0}", runningTotal.ToString());
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                var errorMsg = ex.InnerException != null ? string.Format("[ERROR] {0} : [DETAILS] {1}", ex.Message, ex.InnerException.ToString()) :
                    string.Format("[ERROR] {0}", ex.Message);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n{0}", errorMsg);
                Console.ResetColor();
            }
        }

        static int[] IntToArray(int num)
        {
            var numHolder = new List<int>();
            while(num > 0)
            {
                numHolder.Add(num % 10);
                num = num / 10;
            }
            numHolder.Reverse();
            return numHolder.ToArray();
        }

        static void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n---===[Project Euler: Problem 17 - Number letter counts]===---\nIf the numbers 1 to 5 are written out in words: " +
                "one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.\n\nFind the total number of letters used in " +
                "any given range of signed integers, where no value is greater than 2,147,483,647 or less than 0.\n\nNOTE: Do not count spaces or hyphens. " +
                "For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. The use of \"and\" when " +
                "writing out numbers is in compliance with British usage.\n\n\n");
            Console.ResetColor();
        }

        static bool ValidateInput(int num)
        {
            if (num > 0 && num < 2147483647)
                return true;

            return false;            
        }
    }
}
