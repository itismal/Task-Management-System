using System;
using System.Collections.Generic;
using System.IO;

namespace A3_ADT
{
    public class TaskList
    {
        private List<string> taskList;

        public TaskList()
        {
            //instatiate the list for tasks
            taskList = new List<string>();
        }

        public void AddTask(string task)
        {
            //add to the list
            taskList.Add(task);
            Console.WriteLine($"Task '{task}' added successfully.");
        }

        public void RemoveTask(string taskID)
        {
            bool removed = false;

            //find the task ID in the list and remove it and its dependencies from string if any
            for (int i = 0; i < taskList.Count; i++)
            {
                string task = taskList[i];
                string[] taskParts = task.Split(',');

                if (taskParts[0].Trim() == taskID)
                {
                    taskList.Remove(task);
                    Console.WriteLine($"Task '{taskID}' removed successfully.");
                    removed = true;
                    break;
                }
            }

            //for task not found in the list
            if (!removed)
            {
                Console.WriteLine($"Task '{taskID}' not found in the list.");
            }
        }

        public void UpdateTimeCompletion (string taskID, int newTime)
        {
            bool found = false;

            //find the task ID in the list and udpate its time
            for (int i = 0;i < taskList.Count;i++)
            {
                string task = taskList[i];
                string[] taskParts = task.Split(",");

                if (taskParts[0].Trim() == taskID)
                {
                    taskParts[1] = newTime.ToString();
                    taskList[i] = string.Join(", ", taskParts);

                    Console.WriteLine($"Time completion for task '{taskID}' updated to '{newTime}'");
                    found  = true; 
                    break;
                }
            }

            //for task not found in the list
            if (!found)
            {
                Console.WriteLine($"Task '{taskID}' not found in the list.");
            }
        }

        public void DisplayTasks()
        {
            Console.WriteLine("Current tasks in the list:");

            //display all the tasks, their completion time and dependencies in the list
            foreach (string task in taskList)
            {
                Console.WriteLine(task);
            }
        }
    }
}