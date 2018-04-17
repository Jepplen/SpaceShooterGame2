using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter
{    
    // ============================================================================================
    // BackgroundSprite, behållareklass för en bakgrundsbild.
    // Denna typ av bakgrundsbild är en del av en 2Dvektor med flera BackgroundSprite-objekt.
    // ============================================================================================
    class BackgroundSprite : GameObject
    {
        // ============================================================================================
        // BackgroundSprite(), konstruktor för att skapa BackgroundSprite-objekt
        // ============================================================================================
        public BackgroundSprite(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {
        }


        // ============================================================================================
        // Update(), ändrar positionen för att BackgroundSprite-objekt.
        // Flyttar det längst upp ifall det har gått utanför i nedkanten av skärmen.
        // ============================================================================================
        public void Update(GameWindow window, int nrBackgroundsY)
        {

            if (GameElements.player.EscapeIsPressed) // Spelaren har tryckt Escape in-game
            {
                vector.Y += 0; // Flytta bakgrunden
            }
            else
            {
                vector.Y += 2f; // Flytta bakgrunden
            }
            // Kontrollera om bakgrunden har åkt ut i nedkant:
            if (vector.Y > window.ClientBounds.Height)
            {
                // Flytta bilden så att den hamnar ovanför alla andra bakgrundsbilder
                vector.Y = vector.Y - nrBackgroundsY * texture.Height;
            }
        }
    }

   


    // ============================================================================================
    // Background, klass för att rita ut en 2d-vektor med bakgrundsbilder.
    // ============================================================================================
    class Background
    {
        BackgroundSprite[,] background;
        int nrBackgroundsX, nrBackgroundsY;

        // =================================================================================================
        // Background(), konstruktor som skapar alla BackgroundSprite.objekt i en tvådimensionell vektor.
        // =================================================================================================
        public Background(Texture2D texture, GameWindow window)
        {
            // Hur många bilder ska vi ha på bredden?
            double tmpX = (double)window.ClientBounds.Width / texture.Width;
            // Avrunda uppåt med Math.Ceiling():
            nrBackgroundsX = (int)Math.Ceiling(tmpX);

            // Hur många bilder ska vi ha på höjden?
            double tmpY = (double)window.ClientBounds.Height / texture.Height;
            // Avrunda uppåt med Math.Ceiling(), lägg till 1 extra:
            nrBackgroundsY = (int)Math.Ceiling(tmpY) + 1;

            // Sätt storlek på vektorn:
            background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];

            // Fyll på vektorn med BackgroundSprite-objekt:
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    int posX = i * texture.Width;

                    // Gör så den första hamnar ovanför skärmen:
                    int posY = j * texture.Height - texture.Height;

                    background[i, j] = new BackgroundSprite(texture, posX, posY);

                }

            }
        }


        // ========================================================================================================================================
        // Update(), uppdaterar positionen för samtliga BackgroundSprite-objekt.
        // Den gör inte så mycket, den loopar igenom samtliga klassobjekt i den tvådimensionella vektorn och anropar BackgroundSprite.Update():
        // ========================================================================================================================================
        public void Update(GameWindow window)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Update(window, nrBackgroundsY);
                }
            }
        }


        // ============================================================================================
        // Draw(), ritar ut samtliga BackgroundSprite-objekt.
        // Samma som Background.Update(), fast anropar Background.Draw() istället.
        // ============================================================================================
        public void Draw(SpriteBatch spriteBatch)
        {

            //if (GameElements.player.EscapeIsPressed)
            //{
            //    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);


            //    for (int i = 0; i < nrBackgroundsX; i++)
            //    {
            //        for (int j = 0; j < nrBackgroundsY; j++)
            //        {
            //            background[i, j].Draw(spriteBatch);
            //        }
            //    }

            //}


            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Draw(spriteBatch);
                }
            }
        }
    }

}
