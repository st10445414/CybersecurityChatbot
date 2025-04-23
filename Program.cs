

using System;
using System.Media;
using System.Threading;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Displaya the border and logo
            PrintBorder();
            PrintAsciiArt();
            PrintBorder();
            PlayGreeting();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine($"\nHi {userName}, I'm your Cybersecurity Assistant. Ask me anything about online safety!\n");

            PrintMenu();

            string input;
            do

            // Ask for user's name and personalize interaction
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                input = Console.ReadLine()?.Trim().ToLower();
                HandleInput(input, userName);
            } while (input != "exit");

            Console.ResetColor();
            Console.WriteLine("\nGoodbye! Stay safe online.");
        }
        //audio greeting
        static void PlayGreeting()
        {
            try
            {
                using SoundPlayer player = new SoundPlayer("greeting.wav");
                player.Load();
                player.PlaySync();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Error playing greeting audio: " + e.Message + "]");
            }
        }

        static void PrintAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("""
                              
                                                                                      

                                   _____      _                                          _ _            _____ _           _     ____        _    
                                  / ____|    | |                                        (_) |          / ____| |         | |   |  _ \      | |   
                                 | |    _   _| |__   ___ _ __   ___  ___  ___ _   _ _ __ _| |_ _   _  | |    | |__   __ _| |_  | |_) | ___ | |_  
                                 | |   | | | | '_ \ / _ \ '__| / __|/ _ \/ __| | | | '__| | __| | | | | |    | '_ \ / _` | __| |  _ < / _ \| __| 
                                 | |___| |_| | |_) |  __/ |    \__ \  __/ (__| |_| | |  | | |_| |_| | | |____| | | | (_| | |_  | |_) | (_) | |_  
                                  \_____\__, |_.__/ \___|_|    |___/\___|\___|\__,_|_|  |_|\__|\__, |  \_____|_| |_|\__,_|\__| |____/ \___/ \__| 
                                         __/ |                                                  __/ |                                            
                                        |___/                                                  |___/                                             

            """);
        }


        // Display menu of available questions to help avoid confusion on what questions can be asked
        static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Available Topics You Can Ask Me About:");
            Console.WriteLine("- How are you?");
            Console.WriteLine("- What can I ask you about?");
            Console.WriteLine("- Phishing");
            Console.WriteLine("- Safe passwords suggestions");
            Console.WriteLine("- Safe browsing Advice");
            Console.WriteLine("- Exit\n");
        }

        static void PrintBorder()
        {
            Console.WriteLine(new string('=', 70));
        }
        //fallback for unknown input
        static void HandleInput(string input, string name)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?");
                return;
            }
            //all questions that can be asked
            switch (input)
            {
                case "how are you?":
                    Console.WriteLine("Bot: I'm a bot, but I'm functioning within optimal parameters.");
                    break;
                case "what can i ask you about?":
                    Console.WriteLine("Bot: You can ask about phishing, safe passwords suggestions, or browsing Advice.");
                    break;
                case "phishing":
                    Console.WriteLine("Bot: Phishing is a scam where attackers try to trick you into giving up personal info via fake emails or sites.");
                    break;
                case "Safe passwords suggestions":
                    Console.WriteLine("Bot: Use long, unique passwords for each account. Consider a password manager.");
                    break;
                case "Safe browsing Advice":
                    Console.WriteLine("Bot: Avoid clicking on suspicious links and make sure websites use HTTPS.");
                    break;
                case "exit":
                    Console.WriteLine("Bot: Ending the session. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Bot: I'm not sure how to respond to that. Try asking about phishing, passwords, or safe browsing.");
                    break;
            }
        }
    }
}

