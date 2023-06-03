using System;
using System.Collections.Generic;
using System.Linq;

namespace A3_ADT
{
    public class TaskInfo
    {
        //task attributes
        public string TaskID { get; }
        public int TimeNeeded { get; private set; }
        public List<string> Dependencies { get; }

        //set task attributes on instantiation
        public TaskInfo (string taskID, int timeNeeded, List<string> dependencies)
        {
            TaskID = taskID;
            TimeNeeded = timeNeeded;
            Dependencies = dependencies;
        }

        //update task completion time
        public void UpdateTimeNeeded (int timeNeeded)
        {
            TimeNeeded = timeNeeded;
        }

        //updated ToString to print the task attributes
        public override string ToString()
        {
            return $"TaskID: {TaskID}, Time Needed: {TimeNeeded}, Dependencies: {string.Join(", ", Dependencies)}";
        }
    }
}