using System;

namespace A3_ADT
{
    public class Menu
    {
        private TaskManagement taskManagement;
        private string fileFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

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
                Console.WriteLine($"File Folder: {fileFolder}");
                Console.WriteLine();
                Console.WriteLine("0. Display help menu");
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
                string userInput = Console.ReadLine().Trim();

                Console.WriteLine();
                switch (userInput)
                {
                    //help menu
                    case "0":
                        HelpMenu();
                        break;

                    //load tasks from file
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

                            //parse all the lines in the file
                            foreach (string line in fileParser.ParsedData)
                            {
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

                    //add a task
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

                    //remove a task
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

                    //update completion of a task
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

                    //display loaded tasks
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

        private void HelpMenu()
        {
            Console.Clear();

            Console.WriteLine("------ HELP MENU ------");
            Console.WriteLine($"File Folder: {fileFolder}");
            Console.WriteLine();

            Console.WriteLine("The program allows you to manage your tasks. Please see the below functionilities:");
            Console.WriteLine();
            Console.WriteLine("- It allows you to choose main menu options by numerics only.");
            Console.WriteLine();
            Console.WriteLine("- It allows you to load tasks from one file at a time.\n" +
                "  Please make sure it is in text (.txt) format.\n" +
                "  The file will need to be created and available at the location displayed.\n" +
                "  Warning: invalid format of task will skip the line in the file!");
            Console.WriteLine();
            Console.WriteLine("- It allows you to add tasks manually one at a time.\n" +
                "  Add format: 'task name, process time, dependencies if any'. Please make sure to use comma for separation.");
            Console.WriteLine();
            Console.WriteLine("- It allows you to remove a task one at a time.\n" +
                "  Remove format: 'task name'. Please note this deletes the task and its data.");
            Console.WriteLine();
            Console.WriteLine("- It allows you to update completion time of a task; one at a time.\n" +
                "  Update time format: 'task name, new time'. Please make sure to use comma for separation.");
            Console.WriteLine();
            Console.WriteLine("- It allows you to display the existing task in memory.\n" +
                "  In other words, the task/s should be loaded to use this feature.\n" +
                "  This is an optional functionality to add user friendliness in using the program :)");
            Console.WriteLine();
            Console.WriteLine("- It allows you to save the tasks and their data into the file.\n" +
                "  If the tasks were loaded from a file, then they will be saved in the same file.\n" +
                "  If the tasks were added manually, then you will be asked for file name.\n" +
                "  Please make sure the file exists at the displayed location for both cases.\n" +
                "  WARNING: the file be overwritten in both the cases!");
            Console.WriteLine();
            Console.WriteLine("- It allows you to find the task sequence.\n" +
                "  This will create a file Sequence.txt at the location displayed with sequence written in it.\n" +
                "  WARNING: the file be overwritten if it already exists!");
            Console.WriteLine();
            Console.WriteLine("- It allows you to find the earliest time completion of tasks loaded.\n" +
                "  This will create a file EarliestTimes.txt at the location displayed with earliest time completion written in it.\n" +
                "  WARNING: the file be overwritten if it already exists!");
            Console.WriteLine();

            //Console.WriteLine("Press any key to return to menu...");
            //Console.ReadKey();
        }
    }
}