﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace A3_ADT
{
    public class TaskInfo
    {
        public string TaskID { get; }
        public int TimeNeeded { get; private set; }
        public List<string> Dependencies { get; }

        public TaskInfo (string taskID, int timeNeeded, List<string> dependencies)
        {
            TaskID = taskID;
            TimeNeeded = timeNeeded;
            Dependencies = dependencies;
        }

        public void UpdateTimeNeeded (int timeNeeded)
        {
            TimeNeeded = timeNeeded;
        }
    }
}
