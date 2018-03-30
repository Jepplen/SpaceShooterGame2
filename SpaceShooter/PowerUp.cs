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

    // GoldCoin, mynt som ger poäng
    class GoldCoin : PhysicalObject
    {
        double timeToDie; // Hur länge guldmyntet lever kvar

        // ===========================================================
        // GoldCoin(), konstruktor för att skapa objektet
        // ===========================================================
        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }

        // ===========================================================
        // Update(), kontrollerar om guldmyntet ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime)
        {
            // Döda guldmyntet om det är för gammalt
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }


    class CrashAnimation : PhysicalObject
    {
        double timeToDie; // Hur länge guldmyntet lever kvar

        // ===========================================================
        // GoldCoin(), konstruktor för att skapa objektet
        // ===========================================================
        public CrashAnimation(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0, 0)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 200;
        }

        // ===========================================================
        // Update(), kontrollerar om guldmyntet ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime)
        {
            // Döda guldmyntet om det är för gammalt
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }


    class PowerUpWeaponLaser : PhysicalObject
    {
        double timeForLaserToDie; // Hur länge Vapenuppgraderingen lever kvar i spelet

        // ===========================================================
        // PowerUpWeaponLaser(), konstruktor för att skapa objektet
        // ===========================================================
        public PowerUpWeaponLaser(Texture2D texture, float X, float Y, GameTime gameTime, GameWindow window) : base(texture, X, Y, 0f, 2f)
        {
            timeForLaserToDie = gameTime.TotalGameTime.TotalMilliseconds + 30000;
        }

        // ===========================================================
        // Update(), kontrollerar om Laseruppgraderingen ska få leva vidare
        // ===========================================================
        public void Update(GameTime gameTime, GameWindow window)
        {

            // Flytta på fienden:
            vector.Y += speed.Y;
            


    
            if (timeForLaserToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }
}
