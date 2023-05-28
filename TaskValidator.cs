using System;
using System.Collections.Generic;
using System.Linq;

namespace A3_ADT
{
    public static class TaskValidator
    {
        public static bool ValidateLine (string line)
        {
            //split the line by comma
            string[] parts = line.Split(',');

            //check if the line has at least minimum of two parts (task ID and time)
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid line format. The should contain at least two items (task ID and its processing time)");
                return false;
            }

            //validate task ID to be added
            string taskID = parts[0].Trim();
            if (string.IsNullOrEmpty(taskID))
            {
                Console.WriteLine($"Invalid line format. Task ID is empty. '{taskID}'");
                return false;
            }

            //validate time of the task to be added
            if (!int.TryParse(parts[1].Trim(), out int timeNeeded) || timeNeeded <= 0)
            {
                Console.WriteLine($"Invalid time format. '{timeNeeded}'");
                return false;
            }

            //validate the dependencies if of the task to be added if any
            for (int i = 2; i < parts.Length; i++)
            {
                string dependency = parts[i].Trim();

                if (string.IsNullOrEmpty(dependency))
                {
                    Console.WriteLine($"Invalid task dependency. '{dependency}'");
                    return false;
                }
            }

            return true;
        }
    }
}
