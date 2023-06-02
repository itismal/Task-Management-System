using System;

namespace A3_ADT
{
    public class Menu
    {
        private TaskManagement taskManagement;

        public Menu()
        {
            taskManagement = new TaskManagement();
        }

        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                //refreshes the menu screen
                Console.Clear();

                //print menu
                Console.WriteLine("------ PROJECT MANAGEMENTMENT SYSTEM ------");
                Console.WriteLine();
                Console.WriteLine("1. Load tasks from the file");
                Console.WriteLine("2. Add a new task");
                Console.WriteLine("3. Remove a task");
                Console.WriteLine("4. Update completion time");
                Console.WriteLine("5. Display the current tasks");
                Console.WriteLine("6. Save tasks to the file");
                Console.WriteLine("7. Find task sequence");
                Console.WriteLine("8. Calculate earliest completion times");
                Console.WriteLine("9. Exit");
                Console.WriteLine();

                //prompt user to select an option
                Console.WriteLine("Select an option: ");
                string userInput = Console.ReadLine();

                Console.WriteLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Loading tasks from the file...");

                        //get the file name from user
                        Console.WriteLine("Enter the file name: ");
                        string fileName =  Console.ReadLine();

                        Console.WriteLine();
                        //instantiate FileParser class to load the file
                        FileParser fileParser = new FileParser(fileName);

                        //check for the task loading
                        if (fileParser.ParsedData.Count > 0)
                        {
                            Console.WriteLine("Tasks loaded successfully.");

                            foreach (string line in fileParser.ParsedData)
                            {
                                //Console.WriteLine(line);
                                string[] taskParts = line.Split(',');
                                string taskID = taskParts[0].Trim();
                                int timeNeeded = int.Parse(taskParts[1].Trim());

                                List<string> dependencies = new List<string>();

                                if (taskParts.Length > 2)
                                {
                                    for (int i = 2; i < taskParts.Length; i++)
                                    {
                                        dependencies.Add(taskParts[i].Trim());
                                    }
                                }

                                taskManagement.AddTask(taskID, timeNeeded, dependencies);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: The file is empty or invalid.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter new task (ID, Time, Dependencies if any): ");
                        string task = Console.ReadLine();

                        //validate the format
                        if (TaskValidator.ValidateLine(task))
                        {
                            string[] taskParts = task.Split(',');
                            string taskID = taskParts[0].Trim();
                            int timeNeeded = int.Parse(taskParts[1].Trim());

                            List<string> dependencies = new List<string>();

                            if (taskParts.Length > 2)
                            {
                                for (int i = 2; i < taskParts.Length; i++)
                                {
                                    dependencies.Add(taskParts[i].Trim());
                                }
                            }
                            

                            taskManagement.AddTask(taskID, timeNeeded, dependencies);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid task format. Task '{task}' not added");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter new taskID to be removed: ");
                        string tID1 = Console.ReadLine();

                        if (string.IsNullOrEmpty(tID1))
                        {
                            Console.WriteLine("Invalid task ID."); break;
                        }
                        else
                        {
                            taskManagement.RemoveTask(tID1); break;
                        }

                    case "4":
                        Console.WriteLine("Enter taskID: ");
                        string tID2 = Console.ReadLine();

                        if (string.IsNullOrEmpty(tID2))
                        {
                            Console.WriteLine("Invalid task ID."); break;
                        }

                        Console.WriteLine("Enter the new time: ");
                        string input = Console.ReadLine();
                        bool isNum = int.TryParse(input, out int newTime);

                        if (isNum)
                        {
                            taskManagement.UpdateTimeCompletion(tID2, newTime); break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid time format."); break;
                        }

                    case "5":
                        taskManagement.DisplayTasks();
                        break;

                    case "6":
                        Console.WriteLine("Option 6 selected");
                        break;

                    case "7":
                        Console.WriteLine("Option 7 selected");
                        break;

                    case "8":
                        taskManagement.CalculateCommenceTimes();
                        break;

                    case "9":
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