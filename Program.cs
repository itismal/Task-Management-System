using System;

namespace A3_ADT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.DisplayMenu();

            //TaskManagement taskManagement = new TaskManagement();

            ////Add tasks
            //taskManagement.AddTask("T1", 100, new List<string>());
            //taskManagement.AddTask("T2", 30, new List<string> { "T1" });
            //taskManagement.AddTask("T3", 50, new List<string> { "T2", "T5" });
            //taskManagement.AddTask("T4", 90, new List<string> { "T1", "T7" });
            //taskManagement.AddTask("T5", 70, new List<string> { "T2", "T4" });
            //taskManagement.AddTask("T6", 55, new List<string> { "T5" });
            //taskManagement.AddTask("T7", 50, new List<string>());

            ////taskManagement.CalculateCommenceTimes();
            //taskManagement.GetSequence();
        }
    }
}