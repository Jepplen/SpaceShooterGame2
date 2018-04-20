using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceShooter.FileHandler
{

    // Klass för filhanterare för externa filer
    public class FileHandlerService
    {
        public FileHandlerService()
        {

            // Ge roottillgång till High Score filen
            _rootFilePath = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\");
            _filePath = _rootFilePath + @"\SpaceShooter\SpaceShooter\Highscore\highscore.txt";
        }

        private string _rootFilePath;
        private string _filePath;

        public void WriteToFile(string[] lines)
        {
            // Tömmer listan på tidigare poäng

            using (StreamWriter file =
            new StreamWriter(_filePath))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        // Töm High Score listan
        public void ClearFile()
        {
            System.IO.File.WriteAllText(@_filePath, string.Empty);
        }

        // Läs High Score listan
        public List<string> ReadFileLines()
        {
            string[] lines = File.ReadAllLines(_filePath);
            var asList = lines.ToList();
            return asList;
        }
    }
}
