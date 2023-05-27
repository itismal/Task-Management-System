using System;

namespace A3_ADT
{
    class Menu
    {
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("------ PROJECT MANAGEMENTMENT SYSTEM ------");
                Console.WriteLine();
                Console.WriteLine("1. Load tasks from the file");
                Console.WriteLine("2. Add a new task");
                Console.WriteLine("3. Remove a task");
                Console.WriteLine("4. Update completion time");
                Console.WriteLine("5. Save tasks to the file");
                Console.WriteLine("6. Find task sequence");
                Console.WriteLine("7. Calculate earliest completion times");
                Console.WriteLine("8. Exit");
                Console.WriteLine();

                Console.WriteLine("Select an option: ");
                string userInput = Console.ReadLine();

                Console.WriteLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Option 1 selected");
                        break;

                    case "2":
                        Console.WriteLine("Option 2 selected");
                        break;

                    case "3":
                        Console.WriteLine("Option 3 selected");
                        break;

                    case "4":
                        Console.WriteLine("Option 4 selected");
                        break;

                    case "5":
                        Console.WriteLine("Option 5 selected");
                        break;

                    case "6":
                        Console.WriteLine("Option 6 selected");
                        break;

                    case "7":
                        Console.WriteLine("Option 7 selected");
                        break;

                    case "8":
                        Console.WriteLine("Exiting the program...");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please choose the number from the options.");
                            break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
