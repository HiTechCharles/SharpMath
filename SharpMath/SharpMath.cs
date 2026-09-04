using System;
using ShadowFlame;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Speech.Synthesis;


//Sharpmath is a program for solving different kinds of math problems.
//the number of problems and the difficulty level can be chosen.
namespace SharpMath
{
    internal class SharpMath
    {
        #region Variables
        private static int NumProblems, HighNum;  //number of problems, highest # allowed in a problem
        private static readonly Random RNG = new Random(); //random number generator
        private static readonly Stopwatch SolveTime = new Stopwatch();  //stopwatch to time solving
        // Single shared TTS instance (disposable)
        private static readonly TTS tts = new TTS();
        private static bool UseTTS = TTS.speechEnabled;  //text to speech enabled

        private static readonly string AppDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SharpMath"); //directory to save log file
        private static readonly string TodayLog = Path.Combine(AppDirectory, "Last Problem Set.txt"); //log file path
        private static readonly string FullLog = Path.Combine(AppDirectory, "Full Log.txt"); //full log file path
        #endregion

        static char WaitForKeyPress(string prompt)  //get a keypress and store it
        {
            tts.SpeakAndDisplay(prompt);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Prevents the key from being displayed

            // Return the character of the pressed key, lowercase
            tts.Stop();  //stop any ongoing speech
            return char.ToLower(keyInfo.KeyChar);
        }

        static void Main()   //entrypoint of program
        {
            Console.Title = "SharpMath by Charles Martin";
            Console.ForegroundColor = ConsoleColor.White;  //text color for console

            try
            {
                tts.SpeakAndDisplay("");
                Menu();  //display math type menu
            }
            finally
            {
                // ensure TTS resources are released on exit
                try { tts.Dispose(); } catch { }
            }
        }

        static int GetNumber(string prompt)  //get a number from the user with validation
        {
            while (true)
            {
                tts.SpeakAndDisplay(prompt, true);
                string line = Console.ReadLine();
                if (int.TryParse(line, out int value))
                {
                    if (value >= 0) return value;
                }
                tts.SpeakAndDisplay("Please enter a valid non-negative integer.");
            }
        }

        static void SetOptions()  //get # of problems and highest #
        {
            while (true)
            {
                HighNum = GetNumber("\n\nHighest number allowed in a problem?  ");
                if (HighNum >= 0) break;
                tts.SpeakAndDisplay("Highest number must be 0 or greater.");
            }

            while (true)
            {
                NumProblems = GetNumber("How many problems to solve?  ");
                if (NumProblems > 0) break;
                tts.SpeakAndDisplay("Number of problems must be greater than zero.");
            }
            
        }

        static string CorrectMessage() //prints encouragement for correct answers
        {
            string[] Messages =
            {
                "Correct!",
                "Keep it up!",
                "You're Great!",
                "Good Job!",
                "Super Duper!",
                "Well Done!",
                "Nice work!"
            };
            int MNumber = RNG.Next(0, Messages.Length); //random message index
            return Messages[MNumber];
        }

        static string IncorrectMessage(int correctAnswer)  //tells user when answer is incorrect
        {
            string[] Messages =
            {
                "Incorrect.",
                "Maybe next time.",
                "Not Quite.",
                "You can do it.",
                "Nope!",
                "Almost!",
                "Better luck next time."
            };
            int MNumber = RNG.Next(0, Messages.Length); //random message index
            return Messages[MNumber] + $" The correct answer was {correctAnswer}.";
        }

        static void ProblemSet(char operation)
        {
            int correctCount = 0, incorrectCount = 0;  //correct and incorrect answers
            int userAnswer = 0;  //user's answer
            string opString = "";  //operation string

            SetOptions();  //get number of problems and highest number
            SolveTime.Restart();  //reset and start timer

            // preserve the original operation for reporting (so mixed doesn't get overwritten)
            char originalOperation = operation;

            for (int curProblem = 1; curProblem <= NumProblems; curProblem++)
            {
                // delegate problem generation
                var (x, y, currentOp, spokenOp, answer) = ProblemGenerator.GenerateProblem(operation, HighNum, RNG);
                opString = spokenOp;

                tts.SpeakAndDisplay();  //blank line before each problem
                Console.Write($"{x} {currentOp} {y} = ");

                if (UseTTS)  //text to speech
                {
                    SolveTime.Stop();  //pause timer while speaking
                    tts.Speak($"{x} {opString} {y} equals.");
                    SolveTime.Start();  //restart timer
                }

                userAnswer = GetNumber("");

                if (userAnswer == answer)  //correct answer
                {
                    tts.SpeakAndDisplay(CorrectMessage());
                    correctCount++;  //correct answer
                }
                else
                {
                    tts.SpeakAndDisplay(IncorrectMessage(answer));
                    incorrectCount++;  //incorrect
                }
            }

            SolveTime.Stop();  //stop timer
            TimeSpan ts = SolveTime.Elapsed;  //save elapsed time
            ReportCard(originalOperation.ToString(), correctCount, incorrectCount, ts);  //displays statistics
            Menu();  //ask for another math type
        }

        static void ReportCard(string mathType, int correct, int incorrect, TimeSpan ts)
        {
            int percent = NumProblems > 0 ? correct * 100 / NumProblems : 0;  //percent of correct answers
            string username = Environment.UserName;  //get username
            string mathOp = "";  //math operation string
            string todayLogContents = "";  //contents of today's log file
            string elapsedTime = String.Format("{0} Hours, {1} Minutes, {2} Seconds",
            ts.Hours, ts.Minutes, ts.Seconds); //store elapsed time to string

            switch (mathType)  //convert math type char to string
            {
                case "+":
                    mathOp = "Addition";
                    break;
                case "-":
                    mathOp = "Subtraction";
                    break;
                case "*":
                    mathOp = "Multiplication";
                    break;
                case "/":
                    mathOp = "Division";
                    break;
                case "M":
                    mathOp = "Mixed";
                    break;
            }

            Directory.CreateDirectory(AppDirectory);  //create app directory if it doesn't exist

            using (var logLines = new StreamWriter(TodayLog, false))  //open log file for writing
            {
                logLines.WriteLine("           Date & Time:  " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                logLines.WriteLine("                  User:  " + username);
                logLines.WriteLine("             Math Type:  " + mathOp);
                logLines.WriteLine("Highest # in a Problem:  " + HighNum);
                logLines.WriteLine("    Number of Problems:  " + NumProblems);
                logLines.WriteLine("               Correct:  " + correct);
                logLines.WriteLine("             Incorrect:  " + incorrect);
                logLines.WriteLine("               Percent:  " + percent);
                logLines.WriteLine("          Elapsed Time:  " + elapsedTime);
                logLines.WriteLine("   Seconds per Problem:  " + (NumProblems > 0 ? (ts.TotalSeconds / NumProblems).ToString("n1") : "N/A"));
                logLines.WriteLine();
                logLines.WriteLine("------------------------------------");
            }

            todayLogContents = File.ReadAllText(TodayLog);  //read log file contents
            // append to full log
            File.AppendAllText(FullLog, todayLogContents + Environment.NewLine);

            //display report card
            tts.SpeakAndDisplay(todayLogContents);
        }

        static void Menu()  //display list of math types
        {
            tts.SpeakAndDisplay("\n\nWelcome to SharpMath by Charles Martin");
            tts.SpeakAndDisplay("\nWhich type of math would you like:  ");
            tts.SpeakAndDisplay("     A - Addition");
            tts.SpeakAndDisplay("     S - Subtraction");
            tts.SpeakAndDisplay("     M - Multiplication");
            tts.SpeakAndDisplay("     D - Division");
            tts.SpeakAndDisplay("     E - Mixed");
            tts.SpeakAndDisplay("     X - Exit");
            char MenuItem = WaitForKeyPress("Choose an option: ");

            switch (MenuItem)  //run functions based on numbers above
            {
                case 'x':  //exit
                    Environment.Exit(0);
                    break;
                case 'a':   //add
                    ProblemSet('+');
                    break;
                case 's':  //subtraction
                    ProblemSet('-');
                    break;
                case 'm':  //multiplication
                    ProblemSet('*');
                    break;
                case 'e':  //mixed ( all three types)
                    ProblemSet('M');
                    break;
                case 'd':  //division
                    ProblemSet('/');
                    break;
                default:  //other answers, exit
                    Environment.Exit(0);
                    break;
            }
        }
    }  //end  class
}  //end namespace