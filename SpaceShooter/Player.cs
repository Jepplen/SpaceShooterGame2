using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;




namespace SpaceShooter
{

    // This is the main type for your game.

    // ====================================================================================================================
    // Player, klass för att skapa ett spelarobjekt.
    // Klassen ska hantera spelarens rymdskepp (sprite) och ta emot tangenttryckningar för att ändra rymdskeppets position
    // ====================================================================================================================
    class Player : PhysicalObject
    {


        public static int rateOfFire = Settings.RateOfFire;

        public List<Bullet> Bullets { get { return bullets; } }

        List<Bullet> bullets; // Lista som innehåller alla skott

        Texture2D bulletTexture; // Skottets sprite
        double timeSinceLastBullet = 0; // I millisekunder

        

        static int points = 0;

        // ===================================================
        // Player(), konstruktor för att skapa spelarobjekt
        // ===================================================
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }


        // Spelarpoäng till PowerUp klassen
        public int Points { get { return points; } set { points = value; } }


        // ========================================================================
        // Update(), Tar emot tangenttryckningar och uppdatera spelarens position
        // ========================================================================
        public void Update(GameWindow window, GameTime gameTime)
        {

            // Läs in tangentbordstryckningar:
            KeyboardState keyboardState = Keyboard.GetState();





            // RATE OF FIRE SET TO DEFAULT
            if (gameTime.TotalGameTime.TotalMilliseconds > GameElements.spawnTimeReference && gameTime.TotalGameTime.TotalMilliseconds < GameElements.spawnTimeReference + 1000)
            {
                rateOfFire = Settings.RateOfFire;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds > GameElements.spawnTimeReference + 119000)
            {
                rateOfFire = Settings.RateOfFire;
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                isAlive = false;
            }

                // Flytta rymdskeppet efter tangenttryckningar (om det inte är på väg ut från kanten):
                if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.Down))
                if (keyboardState.IsKeyDown(Keys.Down))
                    vector.Y -= speed.Y;
            }

            // Kontrollera om rymdskeppet har åkt utanför kanten, om det har det, så återställ dess position
            // Har det åkt ut till vänster:
            if (vector.X < 0)
            {
                vector.X = 0;
            }
            // Har det åkt ut till höger:
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }
            // Har det åkt ut upptill:
            if (vector.Y < 0)
            {
                vector.Y = 0;
            }
            // Har det åkt ut nedtill:
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }
            

            //Spelaren vill skjuta
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                // Kontrollera om spelaren FÅR skjuta:
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + rateOfFire)
                {
                    // Skapa skottet:
                    Bullet temp = new Bullet(bulletTexture, vector.X + (texture.Width - 1) / 2, vector.Y);
                    bullets.Add(temp); // Lägg till skottet i listan bullets (av klassen Bullet)

                    // Sätt timeSinceLastBullet till detta ögonblick:
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // Flytta på alla skott
            foreach (Bullet b in bullets.ToList())
            {
                // Flytta på skottet
                b.Update();
                // Kontrollera så att skottet inte är "dött"
                if (!b.IsAlive)
                {
                    bullets.Remove(b); // Ta bort skottet ur listan
                }
            }


  
        }

        // ========================================================================
        // Draw(), ritar ut spelaren (bilden) på skärmen
        // ========================================================================
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }


        }




        // ========================================================================
        // Reset(), återställer spelaren för ett nytt spel
        // ========================================================================
        public void Reset(float X, float Y, float speedX, float speedY)
        {

            // Återställ spelarens position och hastighet:
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;

            // Återställ alla skott:
            bullets.Clear();

            // Återställ spelarens poäng:
            points = 0;

            rateOfFire = Settings.RateOfFire;

            // Gör så att spelaren lever igen:
            isAlive = true;

        }

    }

    // ==============================================================
    // Bullet, en klass för att skapa skott
    // ==============================================================
    class Bullet : PhysicalObject
    {
        // ==============================================================
        // Bullet(), konstruktor för att skapa ett skott-objekt
        // ==============================================================
        public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 3f)
        {
        }

        // =======================================================================================
        // Update(), uppdaterar skottets position och tar bort det om det åker utanför skärmen
        // =======================================================================================
        public void Update()
        {
            vector.Y -= speed.Y;
            if (vector.Y < 0)
            {
                isAlive = false;
            }

        }



    }
}