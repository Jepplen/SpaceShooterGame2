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


    // =======================================================================
    // GameObject, en (bas)klass för att kunna skapa olika spelobjekt.
    // Klassen innehåller ett spelobjekts bild oc position.
    // =======================================================================
    class GameObject
    {
        protected Texture2D texture; // Rymdskeppets textur
        protected Vector2 vector; // Rymdskeppets koordinater
        
        // =======================================================================
        // GameObjects(), konstruktor för att skapa objekt
        // =======================================================================
        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
        }

        // =======================================================================
        // Draw(),ritar ut bilden på skärmen
        // =======================================================================
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        // =======================================================================
        // Egenskaper för GameObject
        // =======================================================================
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
    }





    // =======================================================================
    // MovingObject, klass för objekt som rör sig
    // =======================================================================
    abstract class MovingObject : GameObject
    {


        protected Vector2 speed; // Hastigheten på objektet

        // =======================================================================
        // MovingObject(), konstruktor för att skapa objektet
        // =======================================================================
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }

    }

    // =======================================================================
    // PhysicalObject, klass för objekt som kan kollidera med andra objekt
    // =======================================================================
    abstract class PhysicalObject : MovingObject
    {

        protected bool isAlive = true;

        protected int isDamaged = 0;

        protected bool isDestroyed = false;

        protected bool escapeIsPressed = false;

        // =======================================================================
        // PhysicalObject(), konstruktor för att skapa spelar-objekt
        // =======================================================================
        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
        }

        // ================================================================================
        // CheckCollision(), kontrollerar om detta objekt kolliderar med ett annat objekt
        // ================================================================================
        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));

            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));

            return myRect.Intersects(otherRect);
        }

        // ================================================================================
        // Egenskaper för PhysicalObject
        // ================================================================================
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        public int IsDamaged { get { return isDamaged; } set { isDamaged = value; } }

        public bool IsDestroyed { get { return isDestroyed; } set { isDestroyed = value; } }

        public bool EscapeIsPressed { get { return escapeIsPressed; } set { escapeIsPressed = value; } }

    }

}
