using System;
using System.Collections.Generic;
using System.Linq;

namespace A3_ADT
{
    public class TaskManagement
    {
        private Dictionary<string, TaskInfo> taskDict;
        private List<string> taskOrder;

        public TaskManagement ()
        {
            taskDict = new Dictionary<string, TaskInfo> ();
            taskOrder = new List<string> ();
        }

        public void AddTask (string taskID, int timeNeeded, List<string> dependencies)
        {
            if (taskDict.ContainsKey(taskID))
            {
                Console.WriteLine($"Task '{taskID}' already exists.");
            }
            else
            {
                TaskInfo taskInfo = new TaskInfo(taskID, timeNeeded, dependencies);
                taskDict.Add(taskID, taskInfo);
                taskOrder.Add(taskID);
                Console.WriteLine($"Task '{taskID}' added successfully.");
            }
        }

        public void RemoveTask (string taskID)
        {
            if (taskDict.ContainsKey(taskID))
            {
                taskDict.Remove(taskID);
                taskOrder.Remove(taskID);
                Console.WriteLine($"Task '{taskID}' removed successfully.");
            }
            else { Console.WriteLine($"Task '{taskID}' was not found."); }
        }

        public void UpdateTimeCompletion(string taskID, int newTime)
        {
            if (taskDict.ContainsKey(taskID))
            {
                TaskInfo taskinfo = taskDict[taskID];
                taskinfo.UpdateTimeNeeded(newTime);
                Console.WriteLine($"New time for task '{taskID}' updated.");
            }
            else { Console.WriteLine($"Task '{taskID}' was not found."); }
        }

        public void DisplayTasks ()
        {
            Console.WriteLine("Current tasks in the list: ");

            if (taskDict.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (TaskInfo taskInfo in taskDict.Values)
                {
                    Console.WriteLine(taskInfo);
                }
            }            
        }


    }
}
