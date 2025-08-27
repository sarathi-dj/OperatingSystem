using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace OperatingSystem
{
    public class Kernel : Sys.Kernel
    {
        private bool hasStarted = false;
        private FileSystem fs = new FileSystem(); // Simulated file system

        protected override void BeforeRun()
        {
            Console.Clear();
            ShowSplashScreen();
        }

        protected override void Run()
        {
            if (!hasStarted)
            {
                Console.WriteLine("Welcome to Dj.OS!");
                Console.WriteLine("Type 'help' to see available commands.\n");
                hasStarted = true;
            }

            Console.Write("DjOS> ");
            string input = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(input))
                return;

            HandleCommand(input);
        }

        private void HandleCommand(string input)
        {
            string[] parts = input.Split(' ', 3); // command, arg1, arg2 (rest)
            string command = parts[0];

            switch (command)
            {
                case "help":
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("  help              - Show available commands");
                    Console.WriteLine("  clear             - Clear the screen");
                    Console.WriteLine("  about             - Info about Dj.OS");
                    Console.WriteLine("  echo <text>       - Repeat your input");
                    Console.WriteLine("  shutdown          - Halt the system");
                    Console.WriteLine("  create <file>     - Create a file");
                    Console.WriteLine("  write <file> <txt>- Write content to a file");
                    Console.WriteLine("  read <file>       - Read file content");
                    Console.WriteLine("  delete <file>     - Delete a file");
                    Console.WriteLine("  ls                - List all files");
                    Console.WriteLine("  calc <n1> <op> <n2> - Simple calculator (+ - * /)");
                    Console.WriteLine("  osgame            - Start the OS learning game");

                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "about":
                    Console.WriteLine("Dj.OS - A simple custom OS built by Partha Sarathi using Cosmos.");
                    break;

                case "shutdown":
                    Console.WriteLine("Shutting down...");
                    Sys.Power.Shutdown();
                    break;

                case "echo":
                    if (parts.Length > 1)
                        Console.WriteLine(input.Substring(5));
                    else
                        Console.WriteLine();
                    break;

                case "create":
                    if (parts.Length >= 2)
                        fs.Create(parts[1]);
                    else
                        Console.WriteLine("Usage: create <filename>");
                    break;

                case "write":
                    if (parts.Length >= 3)
                        fs.Write(parts[1], parts[2]);
                    else
                        Console.WriteLine("Usage: write <filename> <content>");
                    break;

                case "read":
                    if (parts.Length >= 2)
                        fs.Read(parts[1]);
                    else
                        Console.WriteLine("Usage: read <filename>");
                    break;

                case "delete":
                    if (parts.Length >= 2)
                        fs.Delete(parts[1]);
                    else
                        Console.WriteLine("Usage: delete <filename>");
                    break;

                case "ls":
                    fs.List();
                    break;

                case "calc":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Usage: calc <num1> <operator> <num2>");
                        Console.WriteLine("Example: calc 5 + 3");
                    }
                    else
                    {
                        string[] exprParts = input.Substring(5).Split(' ');
                        if (exprParts.Length != 3)
                        {
                            Console.WriteLine("Invalid expression. Format: <num1> <operator> <num2>");
                            break;
                        }

                        if (!double.TryParse(exprParts[0], out double num1) ||
                            !double.TryParse(exprParts[2], out double num2))
                        {
                            Console.WriteLine("Invalid numbers.");
                            break;
                        }

                        string op = exprParts[1];
                        double result = 0;
                        bool valid = true;

                        switch (op)
                        {
                            case "+": result = num1 + num2; break;
                            case "-": result = num1 - num2; break;
                            case "*": result = num1 * num2; break;
                            case "/":
                                if (num2 == 0)
                                {
                                    Console.WriteLine("Error: Division by zero.");
                                    valid = false;
                                }
                                else
                                    result = num1 / num2;
                                break;
                            default:
                                Console.WriteLine("Unsupported operator. Use +, -, *, or /.");
                                valid = false;
                                break;
                        }

                        if (valid)
                            Console.WriteLine($"Result: {result}");
                    }
                    break;

                case "osgame":
                    OSGame game = new OSGame();
                    game.Start();
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for a list.");
                    break;
            }
        }

        private void ShowSplashScreen()
        {
            string[] splashArt = new string[]
            {
                "               .__                                  __           ",
                "__  _  __ ____ |  |   ____  ____   _____   ____   _/  |_  ____   ",
                "\\ \\/ \\/ // __ \\|  | _/ ___\\/  _ \\ /     \\_/ __ \\  \\   __\\/  _ \\  ",
                " \\     /\\  ___/|  |_\\  \\__(  <_> )  Y Y  \\  ___/   |  | (  <_> ) ",
                "  \\/\\_/  \\___  >____/\\___  >____/|__|_|  /\\___  >  |__|  \\____/  ",
                "             \\/          \\/            \\/     \\/                 ",
                "  ________       __/\\       ________    _________                ",
                "  \\______ \\     |__)/______ \\_____  \\  /   _____/                ",
                "   |    |  \\    |  |/  ___/  /   |   \\ \\_____  \\                 ",
                "   |    `   \\   |  |\\___ \\  /    |    \\/        \\                ",
                "  /_______  /\\__|  /____  > \\_______  /_______  /                ",
                "          \\/\\______|    \\/          \\/        \\/                 "
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string line in splashArt)
            {
                Console.WriteLine(line);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n      Welcome to Dj.OS!\n");
        }
    }

    // 🗂️ Simulated File System Class
    public class FileSystem
    {
        private Dictionary<string, string> files = new Dictionary<string, string>();

        public void Create(string name)
        {
            if (files.ContainsKey(name))
                Console.WriteLine($"File '{name}' already exists.");
            else
            {
                files[name] = "";
                Console.WriteLine($"File '{name}' created.");
            }
        }

        public void Write(string name, string content)
        {
            if (files.ContainsKey(name))
            {
                files[name] = content;
                Console.WriteLine($"Content written to '{name}'.");
            }
            else
                Console.WriteLine($"File '{name}' not found.");
        }

        public void Read(string name)
        {
            if (files.ContainsKey(name))
            {
                Console.WriteLine($"Contents of '{name}':");
                Console.WriteLine(files[name]);
            }
            else
                Console.WriteLine($"File '{name}' not found.");
        }

        public void Delete(string name)
        {
            if (files.Remove(name))
                Console.WriteLine($"File '{name}' deleted.");
            else
                Console.WriteLine($"File '{name}' not found.");
        }

        public void List()
        {
            if (files.Count == 0)
            {
                Console.WriteLine("No files found.");
                return;
            }

            Console.WriteLine("Files:");
            foreach (var file in files)
            {
                Console.WriteLine($"- {file.Key}");
            }
        }
    }
}
