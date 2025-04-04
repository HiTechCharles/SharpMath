using System;
using System.Diagnostics;
using System.Security.Cryptography;

//Sharpmath is a program for solving different kinds of math problems.
//the number of problems and the difficulty level can be chosen.
namespace SharpMath

{
    internal class Program
    {
        //number of problems, highest # allowed in a problem
        public static int NumProblems, HighNum;

        static void Main(string[] args)   //entrypoint of program
        {
            Menu();  //display math type menu
        }

        static int GetNumber(String Prompt)  //get a number from thee user
        {
            string line; int rtn = 0;  //line read and numebr returned

            Console.Write(Prompt);  //display prompt message before getting input

            line = Console.ReadLine();  //store input

            rtn = int.Parse(line);
            if (rtn < 0)  //if input < 0, retry
            {
                GetNumber(Prompt);
            }
            return rtn;  //return number
        }

        static void SetOptions()  //get # of problems and highest #
        {
            HighNum = GetNumber("\n\nHighest number allowed in a problem?  ");
            NumProblems = GetNumber("How many problems to solve?  ");
        }

        static void CorrectMessage() //prints encouragement for correct answers
        {
            string Message = "Hungry";  //message to print
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            int MNumber = RNG.Next(1, 7); //7 messages to choose from

            switch (MNumber)  //assign message based on chosen number
            {
                case 1:
                    Message = "Correct!";
                    break;
                case 2:
                    Message = "Keep it up!";
                    break;
                case 3:
                    Message = "You're Great!";
                    break;
                case 4:
                    Message = "Good Job!";
                    break;
                case 5:
                    Message = "Super Duper!";
                    break;
                case 6:
                    Message = "Well Done!";
                    break;
                case 7:
                    Message = "Nice work!";
                    break;
            }
            Console.WriteLine(Message);   //print the message
        }

        static void IncorrectMessage()  //tells user when answer is incorrect
        {
            string Message = "Thirsty";  //everything identical to correctmessage()
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            int MNumber = RNG.Next(1, 7);

            switch (MNumber)
            {
                case 1:
                    Message = "Incorrect.";
                    break;
                case 2:
                    Message = "Maybe next time.";
                    break;
                case 3:
                    Message = "Not Quite.";
                    break;
                case 4:
                    Message = "You can do it.";
                    break;
                case 5:
                    Message = "Nope!";
                    break;
                case 6:
                    Message = "Almost!";
                    break;
                case 7:
                    Message = "Better luck next time.";
                    break;
            }
            Console.WriteLine(Message);
        }

        static void Addition()  //addition loop.  all math functions are the same except for the math type
        {
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            //2 numbers, answer, user's answer
            int x, y, Answer, UserAnswer;
            int C = 0, I = 0;  //correct and incorrect answers
            Stopwatch SolveTime = new Stopwatch();  //see how long solving took 
            TimeSpan ts;  //elapsed time

            SolveTime.Start();  //start timer 
            for (int CurProblem = 1; CurProblem <= NumProblems; CurProblem++)
            {
                x = RNG.Next(0, HighNum);  //pick 2 numbers
                y = RNG.Next(0, HighNum);
                Answer = x + y;  //store answer to problem

                if (y > x)  //make sure first number is larger
                {
                    (x, y) = (y, x);
                }

                //display problem and get user's answer
                Console.WriteLine("\nProblem " + CurProblem.ToString() + " of " + NumProblems.ToString());
                Console.WriteLine(x.ToString() + " + " + y.ToString());
                UserAnswer = GetNumber("Answer:  ");
                if ( UserAnswer == Answer )  //correct answer
                {
                    CorrectMessage();
                    C++;  //correct answer
                }
                else
                {
                    IncorrectMessage();
                    Console.WriteLine("Correct answer was " + Answer.ToString());
                    I++;  //incorrect
                }
            }
            SolveTime.Stop();  //stop timer
            ts = SolveTime.Elapsed;  //save elapsed time

            ReportCard("Addition", C, I, ts);  //displays statistics
            Menu();  //ask for another math type
        }

        static void Subtraction()
        {
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            int x, y, Answer, UserAnswer;
            int C = 0, I = 0;  //correct and incorrect answers
            Stopwatch SolveTime = new Stopwatch();  //see how long solving took 
            TimeSpan ts;  //elapsed time

            SolveTime.Start();  //start timer 
            for (int CurProblem = 1; CurProblem <= NumProblems; CurProblem++)
            {
                x = RNG.Next(0, HighNum);  //pick 2 numbers
                y = RNG.Next(0, HighNum);

                if (y > x)  //make sure first number is larger
                {
                    (x, y) = (y, x);
                }

                Answer = x - y;  //store answer to problem
                //display problem and get user's answer
                Console.WriteLine("\nProblem " + CurProblem.ToString() + " of " + NumProblems.ToString());
                Console.WriteLine(x.ToString() + " - " + y.ToString());
                UserAnswer = GetNumber("Answer:  ");
                if (UserAnswer == Answer)  //correct answer
                {
                    CorrectMessage();
                    C++;  //correct answer
                }
                else
                {
                    IncorrectMessage();
                    Console.WriteLine("Correct answer was " + Answer.ToString());
                    I++;  //incorrect
                }
            }
            SolveTime.Stop();
            ts = SolveTime.Elapsed;

            ReportCard("Subtraction", C, I, ts);  //displays 
            Menu();
        }

        static void Multiplication()
        {
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            int x, y, Answer, UserAnswer;
            int C = 0, I = 0;  //correct and incorrect answers
            Stopwatch SolveTime = new Stopwatch();  //see how long solving took 
            TimeSpan ts;  //elapsed time

            SolveTime.Start();  //start timer 
            for (int CurProblem = 1; CurProblem <= NumProblems; CurProblem++)
            {
                x = RNG.Next(0, HighNum);  //pick 2 numbers
                y = RNG.Next(0, HighNum);

                if (y > x)  //make sure first number is larger
                {
                    (x, y) = (y, x);
                }

                Answer = x * y;  //store answer to problem
                //display problem and get user's answer
                Console.WriteLine("\nProblem " + CurProblem.ToString() + " of " + NumProblems.ToString());
                Console.WriteLine(x.ToString() + " * " + y.ToString());
                UserAnswer = GetNumber("Answer:  ");
                if (UserAnswer == Answer)  //correct answer
                {
                    CorrectMessage();
                    C++;  //correct answer
                }
                else
                {
                    IncorrectMessage();
                    Console.WriteLine("Correct answer was " + Answer.ToString());
                    I++;  //incorrect
                }
            }
            SolveTime.Stop();
            ts = SolveTime.Elapsed;

            ReportCard("Multiplication", C, I, ts);  //displays 
            Menu();
        }

        static void Mixed()
        {
            uint seed = (uint)Math.Pow(System.DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            Random RNG = new Random((int)seed);  //create random number generator
            int x, y, t, Answer=0, UserAnswer;
            int C = 0, I = 0;  //correct and incorrect answers
            Stopwatch SolveTime = new Stopwatch();  //see how long solving took 
            TimeSpan ts;  //elapsed time

            SolveTime.Start();  //start timer 
            for (int CurProblem = 1; CurProblem <= NumProblems; CurProblem++)
            {
                x = RNG.Next(0, HighNum);  //pick 2 numbers
                y = RNG.Next(0, HighNum);
                    
                if (y > x)  //make sure first number is larger
                {
                    (x, y) = (y, x);
                }

                t = RNG.Next(1, 3);  //pick a math type 1=+ 2=- 3=*
                //store answer, and display problem based on math type
                Console.WriteLine("\nProblem " + CurProblem.ToString() + " of " + NumProblems.ToString());
                switch (t)
                {
                    case 1:  //addition
                        Answer = x + y;
                        Console.WriteLine(x.ToString() + " + " + y.ToString());
                        break;
                    case 2: //subtraction
                        Answer = x - y;  
                        Console.WriteLine(x.ToString() + " - " + y.ToString());
                        break;
                    case 3: //Multiplication 
                        Answer = x * y;
                        Console.WriteLine(x.ToString() + " * " + y.ToString());
                        break;
                }
                
                UserAnswer = GetNumber("Answer:  ");
                if (UserAnswer == Answer)  //correct answer
                {
                    CorrectMessage();
                    C++;  //correct answer
                }
                else
                {
                    IncorrectMessage();
                    Console.WriteLine("Correct answer was " + Answer.ToString());
                    I++;  //incorrect
                }
            }
            SolveTime.Stop();
            ts = SolveTime.Elapsed;

            ReportCard("Mixed", C, I, ts);  //displays 
            Menu();
        }
        
        static void ReportCard(string MathType, int Correct, int Incorrect, TimeSpan ts)
        {
            int Percent = Correct * 100 / NumProblems;  //percent of correct answers
            string elapsedTime = String.Format("{0} Hours, {1} Minutes, {2} Seconds",
            ts.Hours, ts.Minutes, ts.Seconds); //store elapsed time sto string

            Console.WriteLine("\nSharpMath Report Card");
            Console.WriteLine("          Math Type:  " + MathType);
            Console.WriteLine("          Highest #:  " + HighNum);
            Console.WriteLine(" Number of Problems:  " + NumProblems);
            Console.WriteLine("            Correct:  " + Correct);
            Console.WriteLine("          Incorrect:  " + Incorrect);
            Console.WriteLine("            Percent:  " + Percent);
            Console.WriteLine("       Elapsed Time:  " + elapsedTime);
            Console.WriteLine("Seconds per Problem:  " + (ts.TotalSeconds / NumProblems).ToString("n1"));
        }

        static void Menu ()  //display list of math types
        {
            Console.Title = "SharpMath by Charles Martin";
            Console.ForegroundColor = ConsoleColor.White;  //text color for console
            Console.WriteLine("\n\nWelcome to SharpMath by Charles Martin");
            Console.WriteLine("\nWhich type of math would you like:  ");
            Console.WriteLine("     1 - Addition");
            Console.WriteLine("     2 - Subtraction");
            Console.WriteLine("     3 - Multiplication");
            Console.WriteLine("     4 - Mixed");
            Console.WriteLine("     0 - Exit");
            int MenuItem=GetNumber("Your Choice:  ");

            switch (MenuItem)  //run fuctions based on numbers above
            {
                case 0:  //exit
                    Environment.Exit(0);
                    break;
                case 1:   //add
                    SetOptions();
                    Addition();
                    break;
                case 2:  //subtraction
                    SetOptions();
                    Subtraction();
                    break;
                case 3:  //multiplication
                    SetOptions();
                    Multiplication();
                    break;
                case 4:  //mixed ( all three types)
                    SetOptions();
                    Mixed();
                    break;
                 default:  //other answers, exit
                    Environment.Exit(0);
                    break;
            }
        }
    
    }  //end class
}  //end namespace