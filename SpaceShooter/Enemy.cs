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
        double timeForTripodGreenToDie; // Hur länge Tripod lever kvar i spelet
        int tripodEnemyDamage = 0;

        // ===========================================================
        // EnemyTripodGreen(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyTripodGreen(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0f, 3f)
        {
            timeForTripodGreenToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;

        }

        // ===========================================================
        // Update(), kontrollerar om Tripod:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            //if (vector.Y > window.ClientBounds.Height)
            //{
            //    isAlive = false;
            //}
            // Döda Tripoden:en om det är för gammalt
            if (timeForTripodGreenToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }

            if (tripodEnemyDamage == 1)
            {
                isAlive = false;
            }
        }
    }


    // EnemyTripodRed, fiende
    class EnemyTripodRed : PhysicalObject
    {
        double timeForTripodRedToDie; // Hur länge Tripod lever kvar i spelet
        int tripodEnemyDamage = 0;

        // ===========================================================
        // EnemyTripodRed(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyTripodRed(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0f, 3f)
        {
            timeForTripodRedToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;

        }

        
      

        // ===========================================================
        // Update(), kontrollerar om Tripod:en ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            //if (vector.Y > window.ClientBounds.Height)
            //{
            //    isAlive = false;
            //}
            // Döda Tripoden:en om det är för gammalt
            if (timeForTripodRedToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }

            if (tripodEnemyDamage == 1)
            {
                isAlive = false;
            }
        }
    }

    // MineEnemy, fiende
    class MineEnemy : PhysicalObject
    {
        double timeForMineToDie; // Hur länge Tripod lever kvar i spelet

        // ===========================================================
        // TripodEnemy(), konstruktor för att skapa objektet
        // ===========================================================
        public MineEnemy(Texture2D texture, float X, float Y, GameTime gameTime, GameWindow window) : base(texture, X, Y, 5f, 1f)
        {
            timeForMineToDie = gameTime.TotalGameTime.TotalMilliseconds + 15000;
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
            if (vector.X > window.ClientBounds.Width - texture.Width - 10 || vector.X < 10)
            {
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;
            }

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            //if (vector.Y > window.ClientBounds.Height)
            //{
            //    isAlive = false;
            //}
            // Döda Tripoden:en om det är för gammalt
            if (timeForMineToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }


    class EnemyBossJarJar : PhysicalObject
    {
        double timeForJarJarToDie; // Hur länge Tripod lever kvar i spelet

        // ===========================================================
        // TripodEnemy(), konstruktor för att skapa objektet
        // ===========================================================
        public EnemyBossJarJar(Texture2D texture, float X, float Y, GameTime gameTime, GameWindow window) : base(texture, X, Y, 3f, 1f)
        {
            timeForJarJarToDie = gameTime.TotalGameTime.TotalMilliseconds + 30000;
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
            if (vector.X > window.ClientBounds.Width - texture.Width - 10 || vector.X < 10)
            {
                speed.X *= -1; // Byt riktning på fienden i X-led
                speed.X += speed.Y;
            }

            // Kontrollera så den inte åker utanför fönstret på uppe och nere
            if (vector.Y > window.ClientBounds.Height - texture.Height - 10 || vector.Y < 10)
            {
                speed.Y *= -1; // Byt riktning på fienden i X-led
                speed.Y += speed.X;
            }

            //// Gör fienden inaktiv om den åker ut nedanför skärmen
            //if (vector.Y > window.ClientBounds.Height)
            //{
            //    isAlive = false;
            //}
            // Döda Tripoden:en om det är för gammalt
            if (timeForJarJarToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }





    //// ===================================================================================
    //// Egenskaper för Enemy
    //// ===================================================================================
    //public bool IsAlive
    //{
    //    get { return isAlive; }
    //    set { isAlive = value; }
    //}


    //{

    //        // Flytta på fienden:
    //        vector.X += speed.X;

    //        // Kontrollera så fienden inte åker utanför fönstret på sidorna
    //        if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
    //        {
    //            speed.X *= -1; // Byt riktning på fienden

    //            vector.Y += speed.Y;
    //        }

    //        // Gör fienden inaktiv om den åker ut där nere
    //        if (vector.Y > window.ClientBounds.Height)
    //        {
    //            isAlive = false;
    //        }


    //    }


}

