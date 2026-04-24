using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;

namespace bruteForceTest
{

    class Program
    {
        //C:\Users\powellj\source\repos\bfTest2\bfTest2\cSharpFiles\
        static string passAct = @"C:\\Users\\powellj\\source\\repos\\bfTest2\\bfTest2\\cSharpFiles\\bruteForcePassAct.txt";
        static string? password;

        static string passEg = @"C:\Users\\powellj\\source\\repos\\bfTest2\\bfTest2\\cSharpFiles\\bruteForcePassEg.txt";
        static int passEgNum;
        static string[]? passExamples;

        static string validateConditFile = @"C:\\Users\\powellj\\source\\repos\\bfTest2\\bfTest2\\cSharpFiles\\validationConditions.txt";
        static string? validationConditions;

        static string specialCharFile = @"C:\\Users\\powellj\\source\\repos\\bfTest2\\bfTest2\\cSharpFiles\\specialCharacters.txt";
        static string[]? specialChars;
        static int specialCharNum;

        static bool correctPassword = false;
        static int numOfPreSuff = 20;
        static string currentGuess = "";





        static void Main(string[] args)
        {

            Console.WriteLine("Starting...\n");
            FileReads();
            MenuOne();
        }

        public static void FileReads()
        {
            bool fileError = true;

            if (File.Exists(passAct))
            {
                password = File.ReadAllText(passAct);
                fileError = false;
            }

            if (File.Exists(passEg))
            {
                passEgNum = File.ReadAllLines(passEg).Count();
                passExamples = new string[passEgNum];
                passExamples = File.ReadAllLines(passEg);
                fileError = false;
            }

            if (File.Exists(specialCharFile))
            {
                specialCharNum = File.ReadAllLines(specialCharFile).Count();
                specialChars = new string[specialCharNum];
                specialChars = File.ReadAllLines(specialCharFile);
                fileError = false;
            }

            if (File.Exists(validateConditFile))
            {
                validationConditions = File.ReadAllText(validateConditFile);
                fileError = false;

            }

            if (fileError == true)
            {
                Console.WriteLine("ERROR: A file is missing");
                Console.WriteLine("\nPlease close program...");
            }
            else
            {
                return;
            }
        }
        public static void MenuOne()
        {
            Console.Clear();


            Console.WriteLine("Choose an option");
            Console.WriteLine("0. Print Validation Conditionals \n1. Info Screen \n2. Test password matches");


            while (true)
            {
                char input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '0':
                        ValidationConditionsFile();
                        break;
                    case '1':
                        Info();
                        break;
                    case '2':

                        CallAllMethods();
                        break;
                    default:
                        Console.WriteLine("\nInvalid input, try again...");

                        break;
                }
            }




        }

        static void ValidationConditionsFile()
        {
            Console.Clear();
            Console.WriteLine(validationConditions);
            Console.WriteLine("Press enter/space key to go back");
            Console.ReadKey();
            Console.Clear();
            MenuOne();
        }
        static void Info()
        {

            Console.Clear();

            Console.WriteLine("Welcome to info screen");
            Console.WriteLine("Press the enter/space key to continue");
            Console.WriteLine("-------------------------------------");
            Console.ReadKey();

            if (File.Exists(passAct))
            {
                password = File.ReadAllText(passAct);
                Console.WriteLine($"\nThe real password is {password}\n");



            }
            else
            {
                Console.WriteLine("This file does not exist");
                MenuOne();
            }

            if (File.Exists(passEg))
            {
                passEgNum = File.ReadAllLines(passEg).Count();
                Console.WriteLine("-------------------------------------\n");



                Console.WriteLine($"There are {passEgNum} example password/s");
                Console.WriteLine("Press the enter/space key to view the list of example passwords");
                Console.ReadKey();
                Console.WriteLine("\nExample passwords:");
                passExamples = new string[passEgNum];
                passExamples = File.ReadAllLines(passEg);

                {
                    int x = 1;
                    foreach (string passExample in passExamples)
                    {
                        Console.WriteLine($"\n{x}. {passExample}\n");
                        x++;
                    }



                }

                Console.WriteLine("Press enter/space key to go back to menu");
                Console.ReadKey();
                Console.Clear();
                MenuOne();
            }
        }
        static void BasePasswordTest(string currentGuess)
        {

            Console.WriteLine("\n\n\n");
            int count = 1;
            foreach (string passExample in passExamples)
            {

                Console.WriteLine($"\nPassword guess {count}. {passExample}");
                count++;



                if (password == currentGuess)
                {
                    correctPassword = true;

                }
                else
                {
                    correctPassword = false;
                }




                if (correctPassword == true)
                {
                    Console.WriteLine($"Corerct password found: Your password is {password}");
                    Console.ReadKey();
                }

            }
            Console.ReadKey();
            return;


        }

        static void NumbersPreSuff(string currentGuess)
        {
            bool suffix = false;
            bool bothPreStuff = false;
            Console.Clear();
            Console.WriteLine("\n\n\n");
            // Console.WriteLine("Starting test2");
            // Console.WriteLine("Press any key to continue");
            //Console.ReadKey();

            foreach (string passExample in passExamples)
            {

                for (int x = 0; x <= numOfPreSuff; x++)
                {
                    if (x == numOfPreSuff && suffix == false && bothPreStuff == false)
                    {
                        x = 0;
                        suffix = true;
                    }
                   else  if (x == numOfPreSuff && suffix == true && bothPreStuff == false)
                    {

                        bothPreStuff = true;
                        x = 0;
                    }

                    if (correctPassword == false && suffix == false && bothPreStuff == false)
                    {
                        currentGuess = ($"{Convert.ToString(x)}{passExample}");
                        Console.WriteLine(currentGuess);


                    }
                    else if (correctPassword == false && suffix == true && bothPreStuff == false)
                    {
                        currentGuess = ($"{passExample}{Convert.ToString(x)}");
                        Console.WriteLine(currentGuess);
                    }


                    if (password == currentGuess)
                    {
                        correctPassword = true;
                        break;
                    }

                    else
                    {
                        correctPassword = false;
                        SpecialChars(currentGuess);
                    }

                    if (bothPreStuff == true)
                    {


                        for (int y = 0; y <= numOfPreSuff; y++)
                        {
                            if (correctPassword == false && suffix == true)
                            {
                                currentGuess = ($"{Convert.ToString(x)}{passExample}{Convert.ToString(y)}");
                                Console.WriteLine(currentGuess);
                                if (password == currentGuess)
                                {

                                    correctPassword = true;
                                    break;
                                }
                                else
                                {
                                    SpecialChars(currentGuess);

                                }
                            }
                            else
                            {
                                break;
                            }
                        }


                    }
                    if (bothPreStuff == true && x == numOfPreSuff)
                    {
                        break;
                    }

                }
            }

            if (correctPassword == true)
            {
                Console.WriteLine($"Corerct password found: Your password is {password}");
                Console.WriteLine("Press enter/space key to return to main menu");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("No password match was found");
                Console.ReadKey();

            }
        }


        static void SpecialChars(string guess)
        {
            foreach (string specialChar in specialChars)
            {
                if (correctPassword == false)
                {
                    for (int y = 0; y < guess.Length + 1; y++)
                    {
                        if (correctPassword == false)
                        {
                            Console.WriteLine(guess.Insert(y, specialChar));
                            string specialCharGuess = (guess.Insert(y, specialChar));

                            if (specialCharGuess == password)
                            {
                                correctPassword = true;
                                break;

                            }

                        }

                        else
                        {
                            break;
                        }
                    }

                    return;
                }
            }
        }

        static void CallAllMethods()
        {
            foreach (string passExample in passExamples)
            {
                currentGuess = passExample;

                BasePasswordTest(currentGuess);
                SpecialChars(currentGuess);

                Console.ReadKey();
                NumbersPreSuff(currentGuess);
                // Console.WriteLine("??????????????????");
                Console.ReadKey();



            }

            }
            
        
    }
}


