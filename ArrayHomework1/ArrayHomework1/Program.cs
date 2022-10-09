/*
* Name: [YOUR NAME HERE]
* South Hills Username: [YOUR SOUTH HILLS USERNAME HERE]
*/
using System;
using System.Numerics;

namespace ArrayHomework1
{
    class Program
    {
        const byte UNDERFLOW_CODE = 1;
        const byte OVERFLOW_CODE = 2;

        static void Main()
        {
            bool goAgain = true;

            while (goAgain == true)
            {
                Console.WriteLine("This program will take five inputs and sum them all together!");

                int number1 = GetNumberViaInput(1);
                int number2 = GetNumberViaInput(2);
                int number3 = GetNumberViaInput(3);
                int number4 = GetNumberViaInput(4);
                int number5 = GetNumberViaInput(5);

                byte underOverflowCheck = CheckForOverUnderflow(number1, number2, number3, number4, number5);

                if(underOverflowCheck == UNDERFLOW_CODE)
                {
                    Console.WriteLine("ERROR: The sum of your numbers is too low!");
                }
                else if(underOverflowCheck == OVERFLOW_CODE)
                {
                    Console.WriteLine("ERROR: The sum of your numbers is too large!");
                }
                else
                {
                    int sum = number1 + number2 + number3 + number4 + number5;
                    Console.WriteLine(number1 + " + " + number2 + " + " + number3 + " + " + number4 + " + " + number5 + " = " + sum);
                }

                goAgain = AskUserToDoAnother();
            }

            Console.WriteLine("Goodbye!\nPress any key to exit...");
            Console.ReadKey();
        }

        static int GetNumberViaInput(int num)
        {
            Console.Write("Input the " + num + GetNumberSuffix(num) + " number: ");
            String alphanumericInput = Console.ReadLine();

            while(Int32.TryParse(alphanumericInput, out _) == false)
            {
                //I want to be as descriptive as possible with my error message. A general "bad input message" is a bad user experience.
                if (BigInteger.TryParse(alphanumericInput, out _) == true)
                {
                    Console.WriteLine("ERROR: '" + alphanumericInput + "' is too " + ((alphanumericInput.StartsWith('-')) ? "small" : "large") + "! Please try again!");
                }
                else
                {
                    Console.WriteLine("ERROR: '" + alphanumericInput + "' is not a number! Please try again!");
                }
                alphanumericInput = Console.ReadLine();
            }

            return Convert.ToInt32(alphanumericInput);
        }

        static String GetNumberSuffix(int num)
        {
            if(num > 10 && num < 20)
            {
                return "th";//Teens (ex: 11th, 12th, etc.)
            }
            else if(num % 10 == 1)
            {
                return "st";//Ex: 1st
            }
            else if(num % 10 == 2)
            {
                return "nd";//Ex: 2nd
            }
            else if(num % 10 == 3)
            {
                return "rd";//Ex: 3rd
            }
            else
            {
                return "th";
            }
        }

        static bool AskUserToDoAnother()
        {
            Console.WriteLine("Would you like to sum five more numbers? Please type in 'yes' or 'no'.");
            String decision = Console.ReadLine();
            
            while(decision.Equals("yes", StringComparison.OrdinalIgnoreCase) == false &&
                decision.Equals("no",StringComparison.OrdinalIgnoreCase) == false)
            {
                Console.WriteLine("ERROR: '" + decision + "' was not a valid input! Please try again!");
                Console.WriteLine("Would you like to sum five more numbers? Please type in 'yes' or 'no'.");
                decision = Console.ReadLine();
            }

            //At this point, we know the user typed in either "yes" or "no", so if they typed in "yes", that means they want to do another.
            //If they did not type in "yes", it means they do not want to do another.
            return decision.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        static byte CheckForOverUnderflow(int number1, int number2, int number3, int number4, int number5)
        {
            BigInteger sum = 0;
            sum += number1;
            sum += number2;
            sum += number3;
            sum += number4;
            sum += number5;

            if(sum < Int32.MinValue)
            {
                return UNDERFLOW_CODE;//If an underflow would happen, return UNDERFLOW_CODE
            }
            else if(sum > Int32.MaxValue)
            {
                return OVERFLOW_CODE;//If an overflow would happen, return OVERFLOW_CODE
            }
            else
            {
                return 0;//If no overflow or underflow would happen, return 0
            }
        }
    }
}