using System;
using System.Collections.Generic;
using System.IO;

namespace A3_ADT
{
    public class FileParser
    {
        public List<string> ParsedData { get; private set; }

        public FileParser(string fileName)
        {
            string solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            string filePath = Path.Combine(solutionFolder, fileName);

            if (!Path.HasExtension(filePath))
            {
                filePath += ".txt";
            }

            if (!File.Exists(filePath))
            {
                using (StreamWriter fileWriter = File.CreateText(filePath))
                {
                    Console.WriteLine($"File '{fileName} created as it did not exist.'");
                }
            }

            ParsedData = new List<string>();

            string[] fileContent = File.ReadAllLines(filePath);

            for (int i = 0; i < fileContent.Length; i++)
            {
                string line = fileContent[i];

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