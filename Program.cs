

using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;



namespace CybersecurityChatbot
{
    class Program
    {
        //updated respone list that uses random respones too increase chatbot feeling of random responses 
        static string lastTopic = "";


        static List<string> userInterests = new();


        static readonly Dictionary<string, List<string>> keywordResponses = new()
{
    { "password", new List<string>
        {
            "Use long, unique passwords for each account.",
            "Avoid using personal details in your passwords.",
            "Consider a password manager to securely store credentials."
        }
    },
    { "phishing", new List<string>
        {
            "Be cautious of emails asking for personal info.",
            "Scammers often mimic trusted companies—double-check URLs.",
            "Never click suspicious links; report them if unsure."
        }
    },
    { "scam", new List<string>
        {
            "Scammers exploit fear—stay calm and verify before acting.",
            "Always check the sender's email address closely.",
            "When in doubt, contact the organization directly."
        }
    },
    { "privacy", new List<string>
        {
            "Keep your profiles private and limit public info.",
            "Don't overshare on social media—it can be exploited.",
            "Use browser privacy settings and VPNs where possible."
        }
    }
};

        static readonly Random rng = new();


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
            Console.WriteLine("- Password safety");
            Console.WriteLine("- Scam prevention");
            Console.WriteLine("- Privacy tips");
            Console.WriteLine("- Exit\n");
        }

        static void PrintBorder()
        {
            Console.WriteLine(new string('=', 70));
        }
        //fallback for unknown inputs
        static void HandleInput(string input, string name)
        {

            if (input == "help" || input == "menu" || input == "what can you do")
            {
                PrintMenu();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Bot: I didn’t quite understand that. Could you rephrase?");
                return;
            }
            //updated handle method to help with keyword respones
            if (CheckSentiment(input)) return;
            if (CheckMemoryRecall(input)) return;
            if (CheckInterestCapture(input)) return;
            if (CheckFollowUp(input)) return;
            if (CheckKeywordMatch(input)) return;

            Console.WriteLine("Bot: I'm not sure how to respond to that. Try asking about phishing, passwords, or safe browsing.");
            // Memory & Recall too Track user interest
            
            //
            string[] worriedWords = { "worried", "scared", "nervous", "anxious" };
            string[] frustratedWords = { "frustrated", "angry", "annoyed" };
            string[] confusedWords = { "confused", "lost", "unsure" };

            if (worriedWords.Any(word => input.Contains(word)))
            {
                Console.WriteLine("Bot: It's completely understandable to feel that way. You're not alone, and I'm here to help you stay safe online.");
                return;
            }

            if (frustratedWords.Any(word => input.Contains(word)))
            {
                Console.WriteLine("Bot: I hear you — these topics can be tricky. Let's work through it together step-by-step.");
                return;
            }

            if (confusedWords.Any(word => input.Contains(word)))
            {
                Console.WriteLine("Bot: No worries, I’ll try to explain it more clearly. Just let me know where you feel stuck.");
                return;
            }

            if (input.Contains("tell me more") || input.Contains("more info") || input.Contains("explain further"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && keywordResponses.ContainsKey(lastTopic))
                {
                    var extraResponses = keywordResponses[lastTopic];
                    string extra = extraResponses[rng.Next(extraResponses.Count)];
                    Console.WriteLine($"Bot: Sure! Here's something more about {lastTopic}: {extra}");
                }
                else
                {
                    Console.WriteLine("Bot: I’d be happy to explain, but could you tell me what topic you’re referring to?");
                }
                return;
            }

            if (input == "what do you remember" || input == "what do you know about me")
            {
                if (userInterests.Count == 0)
                {
                    Console.WriteLine("Bot: I don't know much about you yet. Tell me what you're interested in!");
                }
                else
                {
                    Console.WriteLine($"Bot: You've told me you're interested in: {string.Join(", ", userInterests)}.");
                }
                return;
            }


            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    string response = responses[rng.Next(responses.Count)];

                    // Personalize response if interests are stored
                    if (userInterests.Count > 0)
                    {
                        string interest = userInterests[rng.Next(userInterests.Count)];
                        response = $"As someone interested in {interest}, here's a tip: {response}";
                    }

                    lastTopic = keyword;
                    Console.WriteLine($"Bot: {response}");
                    return;
                }
            }




            //all questions that can be asked to add clarity
            switch (input.ToLower())

            {
                case "how are you?":
                    Console.WriteLine("Bot: I'm a bot, but I'm functioning within optimal parameters.");
                    break;
                case "what can i ask you about?":
                    Console.WriteLine("Bot: You can ask about phishing, safe passwords suggestions, or browsing Advice,advice if your feeling frustrated,worried,etc,privacy tips");
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
        //check sentiment meant to deploy responses if the user is feeling any of these emotions or feelings
        static bool CheckSentiment(string input)
        {
            string[] worried = { "worried", "scared", "nervous", "anxious" };
            string[] frustrated = { "frustrated", "angry", "annoyed" };
            string[] confused = { "confused", "lost", "unsure" };

            if (worried.Any(input.Contains))
            {
                Console.WriteLine("Bot: It's okay to feel that way. I'm here to help you stay safe online.");
                return true;
            }

            if (frustrated.Any(input.Contains))
            {
                Console.WriteLine("Bot: I hear you. Let's break it down step-by-step.");
                return true;
            }

            if (confused.Any(input.Contains))
            {
                Console.WriteLine("Bot: Let me clarify that. What part is unclear?");
                return true;
            }

            return false;
        }
        //memory recall check to make the chatbot feel better and more responsive
        static bool CheckMemoryRecall(string input)
        {
            if (input == "what do you remember" || input == "what do you know about me")
            {
                if (userInterests.Count == 0)
                    Console.WriteLine("Bot: I don't know much yet. Tell me what you're interested in!");
                else
                    Console.WriteLine($"Bot: You've told me you're interested in: {string.Join(", ", userInterests)}.");
                return true;
            }

            return false;
        }
        //this block is too check interests and adds to memory to contribute to memory recall
        static bool CheckInterestCapture(string input)
        {
            if (input.StartsWith("i like ") || input.StartsWith("i’m interested in ") || input.StartsWith("i'm interested in "))
            {
                string topic = input.Replace("i like ", "")
                                    .Replace("i’m interested in ", "")
                                    .Replace("i'm interested in ", "");

                if (!string.IsNullOrWhiteSpace(topic) && !userInterests.Contains(topic))
                {
                    userInterests.Add(topic);
                    Console.WriteLine($"Bot: Great! I’ll remember that you're interested in {topic}.");
                }
                return true;
            }
            return false;
        }
        //follow up code block to ensure that conversations feel more natural and chatbot like
        static bool CheckFollowUp(string input)
        {
            if (input.Contains("tell me more") || input.Contains("more info") || input.Contains("explain further"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && keywordResponses.ContainsKey(lastTopic))
                {
                    var more = keywordResponses[lastTopic];
                    Console.WriteLine($"Bot: Here's something more about {lastTopic}: {more[rng.Next(more.Count)]}");
                }
                else
                {
                    Console.WriteLine("Bot: I’d be happy to explain — just remind me what topic you mean.");
                }
                return true;
            }
            return false;
        }
        //key word check block to ensure correct respones are deployed to the user to increase natural conversation feel
        static bool CheckKeywordMatch(string input)
        {
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;
                    var responses = keywordResponses[keyword];
                    string response = responses[rng.Next(responses.Count)];

                    if (userInterests.Count > 0)
                    {
                        string interest = userInterests[rng.Next(userInterests.Count)];
                        response = $"As someone interested in {interest}, here's a tip: {response}";
                    }

                    Console.WriteLine($"Bot: {response}");
                    return true;
                }
            }

            return false;
        }
    }
}



    

