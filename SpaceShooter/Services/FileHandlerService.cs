using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceShooter.FileHandler
{
    public class FileHandlerService
    {
        public FileHandlerService()
        {
            _filePath = @"C:\Users\Jepplen\source\repos\SpaceShooter\SpaceShooter\Highscore\highscore.txt";
        }

        private string _filePath;

        public void WriteToFile(string[] lines)
        {
            // clear previous scores

            using (StreamWriter file =
            new StreamWriter(_filePath))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
        
        public List<string> ReadFileLines()
        {
            string[] lines = File.ReadAllLines(_filePath);
            var asList = lines.ToList();
            return asList;
        }
    }
}
