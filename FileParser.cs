using System;
using System.Collections.Generic;
using System.IO;

namespace A3_ADT
{
    public class FileParser
    {
        //list to store the tasks loaded from the file
        public List<string> ParsedData { get; private set; }

        public FileParser(string fileName)
        {
            //setup file path
            string fileFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            string filePath = Path.Combine(fileFolder, fileName);

            if (!Path.HasExtension(filePath))
            {
                filePath += ".txt";
            }

            ParsedData = new List<string>();

            //get all file content in an array
            string[] fileContent = File.ReadAllLines(filePath);

            for (int i = 0; i < fileContent.Length; i++)
            {
                string line = fileContent[i];

                //validate file and task formats
                if (ValidateLine(line))
                {
                    ParsedData.Add(line);
                }
                else
                {
                    Console.WriteLine($"Invalid line {i + 1}: {line}. Line skipped.");
                }

                if (ParsedData.Count == 0)
                {
                    Console.WriteLine("The file is empty or invalid.");
                }
            }
        }

        private bool ValidateLine(string line)
        {
            return TaskValidator.ValidateLine(line);
        }
    }
}