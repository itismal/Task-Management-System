using System;
using System.Collections.Generic;
using System.Linq;

namespace A3_ADT
{
    public class TaskManagement
    {
        private Dictionary<string, TaskInfo> taskDict;
        private Dictionary<TaskInfo, int> commenceTimes = new Dictionary<TaskInfo, int>();
        private Dictionary<TaskInfo, bool> isCalculated = new Dictionary<TaskInfo, bool>();
        private Dictionary<TaskInfo, List<int>> dependencyProcessTimes = new Dictionary<TaskInfo, List<int>>();

        public TaskManagement ()
        {
            taskDict = new Dictionary<string, TaskInfo> ();
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

        public void CalculateCommenceTimes()
        {
            commenceTimes.Clear();
            isCalculated.Clear();
            dependencyProcessTimes.Clear();

            foreach (TaskInfo taskInfo in taskDict.Values)
            {
                commenceTimes[taskInfo] = 0;
                isCalculated[taskInfo] = false;

                if (taskInfo.Dependencies.Count != 0)
                {
                    dependencyProcessTimes[taskInfo] = new List<int>();
                }
            }

            foreach (TaskInfo task in taskDict.Values)
            {
                RecursiveDFS(task);
            }

            //foreach (TaskInfo remTask in taskDict.Values)
            //{
            //    if (remTask.Dependencies.Count != 0)
            //    {
            //        if (remTask.Dependencies.Count != dependencyProcessTimes[remTask].Count)
            //        {
            //            RecursiveDFS(remTask);
            //        }
            //    }
            //}

            Console.WriteLine();
            foreach (TaskInfo taskInfo in taskDict.Values)
            {
                Console.WriteLine($"Task '{taskInfo.TaskID}': {commenceTimes[taskInfo]}");
            }
        }

        //private int counter = 1;

        private void RecursiveDFS(TaskInfo task)
        {
            if (task.Dependencies.Count == 0)
            {
                if (isCalculated[task])
                {
                    return;
                }
                else
                {
                    isCalculated[task] = true;
                }
            }
            else
            {
                foreach (string dependency in task.Dependencies)
                {
                    TaskInfo dependencyTask = taskDict[dependency];

                    if (!isCalculated[dependencyTask])
                    {
                        RecursiveDFS(dependencyTask);
                    }
                }

                foreach (string dependency in task.Dependencies)
                {
                    TaskInfo dependencyTask = taskDict[dependency];
                    dependencyProcessTimes[task].Add(commenceTimes[dependencyTask] + dependencyTask.TimeNeeded);
                }

                //Console.WriteLine();
                //Console.WriteLine(counter);
                //foreach (KeyValuePair<TaskInfo, List<int>> kvp in dependencyProcessTimes)
                //{
                //    TaskInfo taskInfo = kvp.Key;
                //    List<int> processTimes = kvp.Value;

                //    Console.WriteLine($"Task '{taskInfo.TaskID}:");

                //    foreach (int process in processTimes)
                //    {
                //        Console.WriteLine(process);
                //    }
                //}
                //counter++;

                commenceTimes[task] = dependencyProcessTimes[task].Max();
                isCalculated[task] = true;

                //return;
            }
        }
    }
}