using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace A3_ADT
{
    public class TaskManagement
    {
        //dictionary to store task and its object info
        private Dictionary<string, TaskInfo> taskDict;

        //dictionary to store task object and its commence time
        private Dictionary<TaskInfo, int> commenceTimes;

        //dictionary to store task object and its commence time calculated status
        private Dictionary<TaskInfo, bool> isCalculated;

        //dictionary to store task object and its dependencies completion time/s
        private Dictionary<TaskInfo, List<int>> dependencyProcessTimes;

        private List<string> sequence;

        //instantiate dictionaries on instantiation
        public TaskManagement ()
        {
            taskDict = new Dictionary<string, TaskInfo> ();
            commenceTimes = new Dictionary<TaskInfo, int>();
            isCalculated = new Dictionary<TaskInfo, bool>();
            dependencyProcessTimes = new Dictionary<TaskInfo, List<int>>();
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
            if (taskDict.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("Current tasks in the list: ");

                foreach (TaskInfo taskInfo in taskDict.Values)
                {
                    Console.WriteLine(taskInfo);
                }
            }
        }

        public void CalculateCommenceTimes()
        {
            //clear the dictionaries if previously used for effective memory usage
            commenceTimes.Clear();
            isCalculated.Clear();
            dependencyProcessTimes.Clear();

            //populate the dictionaries
            foreach (TaskInfo taskInfo in taskDict.Values)
            {
                //set default commence time to 0 AND status to false
                commenceTimes[taskInfo] = 0;
                isCalculated[taskInfo] = false;

                //add task with dependencies and instantiate its list to store dependencies processing times
                if (taskInfo.Dependencies.Count != 0)
                {
                    dependencyProcessTimes[taskInfo] = new List<int>();
                }
            }

            //apply depth first search recursively
            foreach (TaskInfo task in taskDict.Values)
            {
                RecursiveDFS(task);
            }

            //display earliest commence times
            //Console.WriteLine();
            //foreach (TaskInfo taskInfo in taskDict.Values)
            //{
            //    Console.WriteLine($"Task '{taskInfo.TaskID}': {commenceTimes[taskInfo]}");
            //}

            string fileFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            string filePath = Path.Combine(fileFolder, "EarliestTimes.txt");

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (TaskInfo taskInfo in taskDict.Values)
                {
                    writer.WriteLine(taskInfo.TaskID + ", " +commenceTimes[taskInfo]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Earliest Commence Times saved to EarliestTimes.txt\n" +
                $"Location: {filePath}");
        }

        //testing purposes - counter for KeyValuePair in recursiveDFS
        //private int counter = 1;

        //recursively implement a depth first search
        private void RecursiveDFS(TaskInfo task)
        {
            //SCENARIO: task with no dependency
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
            //SCENARIO: task with dependency
            else
            {
                foreach (string dependency in task.Dependencies)
                {
                    TaskInfo dependencyTask = taskDict[dependency];

                    //SCENARIO: dependency task is not yet calculated
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

                //testing purposes - code to display the stack when dependencies are found
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

                //select the max processing time amongst the dependencies
                commenceTimes[task] = dependencyProcessTimes[task].Max();
                isCalculated[task] = true;
            }
        }

        public void GetSequence()
        {
            if (sequence == null)
            {
                CalculateCommenceTimes();
                CalculateSeqeunce();
            }

            //display seqeunce
            //Console.WriteLine();
            //Console.WriteLine("Task Sequence: " + string.Join(", ", sequence));

            string fileFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            string filePath = Path.Combine(fileFolder, "Sequence.txt");

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(string.Join(", ", sequence));
            }
            Console.WriteLine();
            Console.WriteLine("Earliest Commence Times saved to Sequence.txt\n" +
                $"Location: {filePath}");
        }

        //calculate sequence by ordering taskID in ascending order of their corresponding earliest commence times
        private void CalculateSeqeunce()
        {
            sequence = taskDict.Keys.OrderBy(taskID => commenceTimes[taskDict[taskID]]).ToList();
        }
    }
}