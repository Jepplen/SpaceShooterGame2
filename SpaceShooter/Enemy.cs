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






    // ===================================================================================
    // Enemy, (bas)klass för fiender
    // ===================================================================================
    abstract class Enemy : PhysicalObject
    {

        // ===================================================================================
        // Enemy(), konstruktor för att skapa objekt
        // ===================================================================================
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
        }

        // ===================================================================================
        // Update(), abstrakt metod som måste implementeras i alla härledda fiender.
        // Används för att uppdatera fienderna position.
        // ===================================================================================
        public abstract void Update(GameWindow window);

    }

    // ===================================================================================
    // Mine, en elak mina som rör sig fram och tillbaka över skärmen
    // ===================================================================================
    class Mine : Enemy
    {
        // ===================================================================================
        // Mine(), konstruktor för att skapa objektet
        // ===================================================================================
        public Mine(Texture2D texture, float X, float Y) : base(texture, X, Y, 6f, 0.3f)
        {
        }

        // ===================================================================================
        // Update(), uppdaterar fiendens position.
        // ===================================================================================
        public override void Update(GameWindow window)
        {

            // Flytta på fienden
            vector.X += speed.X;

            // Kontrollera så den inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width - 10 || vector.X < 10)
            {
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;
            }

            // Gör fienden inaktiv om den åker ut nedanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }


   
    // ===================================================================================
    // Tripod, en elak fiende som åker i full fart rakt framåt
    // ===================================================================================
    class Tripod : Enemy
    {

        // ===================================================================================
        // Tripod(), konstruktor för att skapa objektet
        // ===================================================================================
        public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 3f)
        {
        }

        // ===================================================================================
        // Update(), uppdaterar fiendens position
        // ===================================================================================
        public override void Update(GameWindow window)
        {
            // Flytta på fienden:
            vector.Y += speed.Y;

            // Gör fienden inaktiv om den åker ut nedanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }



    // EnemyTripodGreen, fiende
    class EnemyTripodGreen : PhysicalObject
    {
        //double timeForTripodGreenToDie; // Hur länge Tripod lever kvar i spelet
        int tripodEnemyDamage = 0;

        // ===========================================================
        // EnemyTripodGreen(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyTripodGreen(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0f, 4f)
        {
            //timeForTripodGreenToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;

        }

        // ===========================================================
        // Update(), kontrollerar om Tripod:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime, GameWindow window)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
            // Döda Tripoden:en om det är för gammalt
            //if (timeForTripodGreenToDie < gameTime.TotalGameTime.TotalMilliseconds)
            //{
            //    isAlive = false;
            //}

            if (tripodEnemyDamage == 1)
            {
                isAlive = false;
            }
        }
    }


    // EnemyTripodRed, fiende
    class EnemyTripodRed : PhysicalObject
    {
        
        int tripodEnemyDamage = 0;

        // ===========================================================
        // EnemyTripodRed(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyTripodRed(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0f, 3f)
        {
            //timeForTripodRedToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;

        }

        
      

        // ===========================================================
        // Update(), kontrollerar om Tripod:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime, GameWindow window)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
            // Döda Tripoden:en om det är för gammalt
            //if (timeForTripodRedToDie < gameTime.TotalGameTime.TotalMilliseconds)
            //{
            //    isAlive = false;
            //}

            if (tripodEnemyDamage == 1)
            {
                isAlive = false;
            }
        }
    }

    // MineEnemy, fiende
    class MineEnemy : PhysicalObject
    {
        //double timeForMineToDie; // Hur länge Tripod lever kvar i spelet

        // ===========================================================
        // TripodEnemy(), konstruktor för att skapa objektet
        // ===========================================================
        public MineEnemy(Texture2D texture, float X, float Y, GameTime gameTime, GameWindow window) : base(texture, X, Y, 5f, 1f)
        {
            //timeForMineToDie = gameTime.TotalGameTime.TotalMilliseconds + 15000;
        }

        // ===========================================================
        // Update(), kontrollerar om Tripod:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime, GameWindow window)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;
            vector.X += speed.X;
           

          

            // Kontrollera så den inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width -10)
            {       
                // check that speed always is minus                
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;

                if (speed.X == -5)
                {                    
                }
                else
                {
                    speed.X = -5;
                }
            }

            else if (vector.X < 10)
            {
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;

                if (speed.X == 5)
                {
                }
                else
                {
                    speed.X = 5;
                }
            }

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }

            // Döda Tripoden:en om det är för gammalt
            //if (timeForMineToDie < gameTime.TotalGameTime.TotalMilliseconds)
            //{
            //    isAlive = false;
            //}
        }
    }


    class EnemyBossRedReaper : PhysicalObject
    {
        

        // ===========================================================
        //EnemyBossRedReaper(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyBossRedReaper(Texture2D texture, float X, float Y, GameTime gameTime, GameWindow window) : base(texture, X, Y, 3f, 1f)
        {           
        }

        // ===========================================================
        // Update(), kontrollerar om bossen:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime, GameWindow window)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;
            vector.X += speed.X;


            // Kontrollera så den inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width - 10)
            {
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;
                if (speed.X == -3)
                {
                }
                else
                {
                    speed.X = -3;
                }
            }

            if (vector.X < 10)
            {
                if (speed.X == 3)
                {
                }
                else
                {
                    speed.X = 3;
                }
            }


            // Kontrollera så den inte åker utanför fönstret på uppe och nere
            if (vector.Y > window.ClientBounds.Height - texture.Height - 10)
            {
                speed.Y *= -1; // Byt riktning på fienden i X-led
                speed.Y += speed.X;

                if (speed.Y == -1)
                {
                }
                else
                {
                    speed.Y = -1;
                }
            }
            if (vector.Y < 10)
            {
                if (speed.Y == 1)
                {
                }
                else
                {
                    speed.Y = 1;
                }
            } 

            //if (gameTime.TotalGameTime.TotalMilliseconds > GameElements.spawnTimeReference + 145000 && gameTime.TotalGameTime.TotalMilliseconds < GameElements.spawnTimeReference + 160000)
            //{
            //    if (vector.X > window.ClientBounds.Width / 2)
            //    {
            //        speed.X = -1;
            //    }
            //    else if (vector.X < window.ClientBounds.Width / 2)
            //    {
            //        speed.X = 1;
            //    }

            //    if (vector.Y > window.ClientBounds.Height / 2)
            //    {
            //        speed.Y = -1;
            //    }
            //    else if (vector.Y < window.ClientBounds.Height / 2)
            //    {
            //        speed.Y = 1;
            //    }

            //    if (vector.X == 399 || vector.X == 400 || vector.X == 401)
            //    {
            //        speed.X = 0;
            //    }

            //    if (vector.Y == 239 || vector.Y == 240 || vector.Y == 241)
            //    {
            //        speed.Y = 0;
            //    }
            //}
            
                
            

        }
    }
}

