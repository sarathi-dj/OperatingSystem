using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatingSystem
{
    public class OSGame
    {
        private int totalMemory = 1024; // Total RAM in MB for simulation
        private int usedMemory = 0;

        // Simulated process list: process name → allocated memory
        private Dictionary<string, int> processes = new Dictionary<string, int>();

        public void Start()
        {
            Console.WriteLine("Welcome to the OS Learning Game!");
            Console.WriteLine("Type 'help' for commands.");

            while (true)
            {
                Console.Write("osgame> ");
                string input = Console.ReadLine().Trim();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                HandleCommand(input);
            }

            Console.WriteLine("Exiting OS Learning Game. Returning to Dj.OS...");
        }

        private void HandleCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();

            switch (command)
            {
                case "help":
                    ShowHelp();
                    break;

                case "create":
                    if (parts.Length < 3 || parts[1].ToLower() != "process")
                    {
                        Console.WriteLine("Usage: create process <processName>");
                    }
                    else
                    {
                        CreateProcess(parts[2]);
                    }
                    break;

                case "kill":
                    if (parts.Length < 3 || parts[1].ToLower() != "process")
                    {
                        Console.WriteLine("Usage: kill process <processName>");
                    }
                    else
                    {
                        KillProcess(parts[2]);
                    }
                    break;

                case "use":
                    if (parts.Length < 3 || parts[1].ToLower() != "memory")
                    {
                        Console.WriteLine("Usage: use memory <MB>");
                    }
                    else
                    {
                        if (int.TryParse(parts[2], out int mem))
                            UseMemory(mem);
                        else
                            Console.WriteLine("Invalid memory amount.");
                    }
                    break;

                case "free":
                    if (parts.Length < 3 || parts[1].ToLower() != "memory")
                    {
                        Console.WriteLine("Usage: free memory <MB>");
                    }
                    else
                    {
                        if (int.TryParse(parts[2], out int mem))
                            FreeMemory(mem);
                        else
                            Console.WriteLine("Invalid memory amount.");
                    }
                    break;

                case "status":
                    ShowStatus();
                    break;

                case "interrupt":
                    SimulateInterrupt();
                    break;

                case "explain":
                    if (parts.Length < 2)
                        Console.WriteLine("Usage: explain <topic>");
                    else
                        ExplainTopic(parts[1].ToLower());
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for a list.");
                    break;
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("\n📘 OS Learning Game Help:");
            Console.WriteLine();
            Console.WriteLine("This game simulates some core OS concepts.");
            Console.WriteLine("Use commands to learn how the OS manages processes, memory, and interrupts.");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  create process <name>   - Simulate starting a new process");
            Console.WriteLine("                           The OS tracks processes using Process Control Blocks (PCBs).");
            Console.WriteLine("  kill process <name>     - Terminate a running process");
            Console.WriteLine("                           OS frees resources when processes end.");
            Console.WriteLine("  use memory <MB>         - Allocate memory to the last created process");
            Console.WriteLine("                           OS manages RAM allocation and prevents crashes.");
            Console.WriteLine("  free memory <MB>        - Free allocated memory from last created process");
            Console.WriteLine("                           Memory is returned to the OS for reuse.");
            Console.WriteLine("  interrupt               - Simulate an interrupt");
            Console.WriteLine("                           Interrupts pause CPU tasks for urgent events.");
            Console.WriteLine("  status                  - Show current OS state");
            Console.WriteLine("                           Displays running processes and free memory.");
            Console.WriteLine("  explain <topic>         - Learn detailed OS topics");
            Console.WriteLine("                           Topics: process, memory, interrupt, scheduler");
            Console.WriteLine("  exit                    - Quit the game and return to Dj.OS");
            Console.WriteLine();
        }

        private void CreateProcess(string name)
        {
            if (processes.ContainsKey(name))
            {
                Console.WriteLine($"Process '{name}' already exists.");
                return;
            }

            processes[name] = 0;
            Console.WriteLine($"Process '{name}' created.");
        }

        private void KillProcess(string name)
        {
            if (processes.TryGetValue(name, out int mem))
            {
                processes.Remove(name);
                usedMemory -= mem;
                if (usedMemory < 0) usedMemory = 0;
                Console.WriteLine($"Process '{name}' terminated and freed {mem} MB memory.");
            }
            else
            {
                Console.WriteLine($"Process '{name}' not found.");
            }
        }

        private void UseMemory(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Memory amount must be positive.");
                return;
            }

            if (processes.Count == 0)
            {
                Console.WriteLine("No processes to allocate memory to.");
                return;
            }

            if (usedMemory + amount > totalMemory)
            {
                Console.WriteLine("Error: Not enough memory available.");
                return;
            }

            string lastProcess = null;
            foreach (var p in processes.Keys)
                lastProcess = p;

            processes[lastProcess] += amount;
            usedMemory += amount;

            Console.WriteLine($"Allocated {amount} MB memory to process '{lastProcess}'.");
        }

        private void FreeMemory(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Memory amount must be positive.");
                return;
            }

            if (processes.Count == 0)
            {
                Console.WriteLine("No processes to free memory from.");
                return;
            }

            string lastProcess = null;
            foreach (var p in processes.Keys)
                lastProcess = p;

            if (processes[lastProcess] < amount)
            {
                amount = processes[lastProcess];
            }

            processes[lastProcess] -= amount;
            usedMemory -= amount;
            if (usedMemory < 0) usedMemory = 0;

            Console.WriteLine($"Freed {amount} MB memory from process '{lastProcess}'.");
        }

        private void ShowStatus()
        {
            Console.WriteLine($"\nTotal Memory: {totalMemory} MB");
            Console.WriteLine($"Used Memory: {usedMemory} MB");
            Console.WriteLine($"Free Memory: {totalMemory - usedMemory} MB");

            if (processes.Count == 0)
            {
                Console.WriteLine("No running processes.");
            }
            else
            {
                Console.WriteLine("Processes:");
                foreach (var p in processes)
                {
                    Console.WriteLine($"- {p.Key}: {p.Value} MB allocated");
                }
            }
            Console.WriteLine();
        }

        private void SimulateInterrupt()
        {
            Console.WriteLine("\nSimulating an interrupt...");
            Console.WriteLine("CPU pauses current tasks to handle urgent event.");
            Console.WriteLine("Interrupt handled, CPU resumes tasks.\n");
        }

        private void ExplainTopic(string topic)
        {
            switch (topic)
            {
                case "process":
                    Console.WriteLine("\nA process is a running program instance. The OS manages processes via Process Control Blocks (PCBs).\n");
                    break;

                case "memory":
                    Console.WriteLine("\nThe OS allocates and frees memory to processes, managing RAM usage efficiently.\n");
                    break;

                case "interrupt":
                    Console.WriteLine("\nAn interrupt is a signal that pauses the CPU to deal with urgent events like I/O or timers.\n");
                    break;

                case "scheduler":
                    Console.WriteLine("\nThe scheduler decides which process the CPU runs next, managing multitasking.\n");
                    break;

                default:
                    Console.WriteLine("\nUnknown topic. Try: process, memory, interrupt, scheduler\n");
                    break;
            }
        }
    }
}
