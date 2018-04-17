using SpaceShooter.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.Services
{
    public class HighScoreService
    {
        public void SetHighScore(Highscore highScores)
        {
            var fileHandlerService = new FileHandlerService();
            var currentHighScores = GetHighScores();

            // kolla om det är ett highscore som ska in på listan

            bool isNewHighscore = false;

            foreach (var score in currentHighScores)
            {
                if(score.PlayerScore < highScores.PlayerScore)
                {
                    isNewHighscore = true;
                }
            }

            if (isNewHighscore)
            {
                currentHighScores.Reverse();
                currentHighScores[0]= highScores;
            

                // uppdatera om något highscore är slaget i listan av highscores


                var newHighscoreArray = new string[10];
                int counter = 0;

                // göra om till en string array
                foreach (var score in currentHighScores)
                {
                    newHighscoreArray[counter] = $"{score.PlayerInitials}|{score.PlayerScore}";
                    //newHighscoreArray[counter] = score.PlayerInitials + "|" + score.PlayerScore;
                    counter++;
                }           

                //  Spara nya highscore arrayen
                fileHandlerService.WriteToFile(newHighscoreArray);

            }

        }

        public List<Highscore> GetHighScores()
        {
            var fileHandlerService = new FileHandlerService();
            var highscores = fileHandlerService.ReadFileLines();

            var highScoreList = new List<Highscore>();

            foreach (var line in highscores)
            {
                var splitLine = line.Split('|');
                var highscore = new Highscore();

                var score = 0;

                var success = int.TryParse(splitLine[0], out score);

                if(success == true)
                {
                    highscore.PlayerScore = score;
                }
                else
                {
                    highscore.PlayerScore = 0;
                }

                highscore.PlayerInitials = splitLine[1];

                highscore.Placement = 0;

                highScoreList.Add(highscore);
            }

            highScoreList = SortHighscore(highScoreList);


            return highScoreList;
        }

        private List<Highscore> SortHighscore(List<Highscore> highScore)
        {
            // 1: sortera fallande på poängen            
            
            highScore = highScore.OrderByDescending(score => score.PlayerScore).ToList();

            // 2: Sätt placering på highscore
            int placement = 1;

            foreach (var score in highScore)
            {
                score.Placement = placement;
                placement++;
            }
           


            return highScore;
        }
    }
}
