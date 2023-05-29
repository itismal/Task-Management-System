using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
