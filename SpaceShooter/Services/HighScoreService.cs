using SpaceShooter.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.Services
{

    // Klass som hanterar High Score
    public static class HighScoreService
    {
        public static void SetHighScore(int score)
        {            
            var currentHighScores = GetHighScores();


            // kolla om det är ett highscore som ska in på listan

            bool isNewHighscore = false;

            foreach (var highScore in currentHighScores)
            {
                if(highScore.PlayerScore < score)
                {
                    isNewHighscore = true;
                }
            }

            // Om nytt High Score ska det in i listan
            if (isNewHighscore)
            {
                currentHighScores.Reverse();
                currentHighScores[0] = new Highscore {
                    Placement = 0,
                    PlayerInitials = "",
                    PlayerScore = score
                };

                // uppdatera om något highscore är slaget i listan av highscores
                SaveHighscores(currentHighScores);

            }

        }

        // Spara High Score
        private static void SaveHighscores(List<Highscore> highScores)
        {
            var fileHandlerService = new FileHandlerService();
            var newHighscoreArray = new string[1];
            int counter = 0;

            // göra om till en string array
            foreach (var highScore in highScores)
            {
                newHighscoreArray[counter] = $"{highScore.PlayerScore.ToString()}";
                //newHighscoreArray[counter] = score.PlayerInitials + "|" + score.PlayerScore;
                counter++;
            }

            // clear previous highscores
            fileHandlerService.ClearFile();

            //  Spara nya highscore arrayen
            fileHandlerService.WriteToFile(newHighscoreArray);
        }

        // Erhåll High Score
        public static List<Highscore> GetHighScores()
        {
            var fileHandlerService = new FileHandlerService();
            var highscores = fileHandlerService.ReadFileLines();

            var highScoreList = new List<Highscore>();

            foreach (var line in highscores)
            {
                //var splitLine = line.Split('|');
                var highscore = new Highscore();

                var score = 0;

                var success = int.TryParse(line, out score);

                if(success == true)
                {
                    highscore.PlayerScore = score;
                }
                else
                {
                    highscore.PlayerScore = 0;
                }

                //highscore.PlayerInitials = splitLine[1];

                //highscore.Placement = 0;

                highScoreList.Add(highscore);
            }

            highScoreList = SortHighscore(highScoreList);


            return highScoreList;
        }

        private static List<Highscore> SortHighscore(List<Highscore> highScore)
        {
            // 1: sortera fallande på poängen            
            
            highScore = highScore.OrderByDescending(score => score.PlayerScore).ToList();

            // 2: Sätt placering på highscore
            //int placement = 1;

            //foreach (var score in highScore)
            //{
            //    score.Placement = placement;
            //    placement++;
            //}
           


            return highScore;
        }
    }
}
