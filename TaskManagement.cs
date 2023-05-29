using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace A3_ADT
{
    public class TaskManagement
    {
        private List<string> centralizedList;
        private Dictionary<string, TaskInfo> taskDict;

        public TaskManagement (List<string> centralizedList)
        {
            this.centralizedList = centralizedList;
            taskDict = new Dictionary<string, TaskInfo> ();
        }

        public void ConvertTaskToObjects()
        {
            foreach (string taskString in centralizedList)
            {
                string[] taskParts = taskString.Split (',');

                string taskID = taskParts[0].Trim ();
                int timeNeeded = int.Parse(taskParts[1].Trim());
                
                List<string> dependencies = new List<string> ();
                if (taskParts.Length > 2)
                {
                    for (int i = 0; i < taskParts.Length; i++)
                    {
                        dependencies.Add(taskParts[i].Trim ());
                    }
                }

                TaskInfo taskInfo = new TaskInfo (taskID, timeNeeded, dependencies);
                taskDict.Add (taskID, taskInfo);
            }
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
                Console.WriteLine($"Task '{taskID}' added successfully.");
            }
        }

        public void RemoveTask (string taskID)
        {
            if (taskDict.ContainsKey(taskID))
            {
                taskDict.Remove(taskID);
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
    }
}
