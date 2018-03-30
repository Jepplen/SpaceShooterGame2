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
    // This is the main type for your game.

   

    // ===============================================================================================
    // Game1, detta är vår huvudklass i spelet, det är här all action sker!
    // ===============================================================================================
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics; // Används för grafik
        SpriteBatch spriteBatch; // Används för att rita bilder

        //Player player;

        //List<Enemy> enemies;

        //List<GoldCoin> goldCoins;
        //Texture2D goldCoinSprite;

      

        //PrintText printText;


        // ===============================================================================================
        // Game1(), klassens konstruktor
        // ===============================================================================================
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       



        // =================================================================================================================
        // Initialize(), anropas då spelet startar.
        // Här ligger all kod för att initiera objekt och skapa dem, dock inte laddning av olika filer (bilder, ljud mm...)
        // =================================================================================================================
        protected override void Initialize()
        {

            // Ligger numera i GameElements.cs

            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();
            base.Initialize();
        }



        // ===============================================================================================
        // LoadContent(), anropas då spelet startar.
        // Anropas endast en gång per spel och laddar här in all content
        // Här laddas alla objekt och filer in (bilder, ljud mm...)
        // ===============================================================================================
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);

            // Ligger numera i GameElements.cs

        }




        // ===============================================================================================
        // UnloadContent(), anropas då spelet avslutas.
        // Anropas en gång per spel, platsen man unloadar spelspecifikt content
        // Här kan man ladda ur de objekt som skulle kunna behöva det för att rensa minne.
 
        // ===============================================================================================
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }




        // ===============================================================================================
        // Update(), första delen av spel-loopen.
        // Här förändras olika objekt. T.ex, så läser vi in data från användaren, flyttar på olika objekt, kollar kollisioner av objekt, spelar audio osv.
        // gameTime, används för att hålla kolla på spelets uppdateringsfrekvens.
        // <param name="gameTime">Provides a snapshot of timing values.</param>
        // ===============================================================================================
        protected override void Update(GameTime gameTime)
        {

            // Stänger av spelet om man trycker på back-knappen på gamepaden:
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                var tjoho = 1;
                this.Exit();
            }


            switch (GameElements.currentState)
            {
                case GameElements.State.Run: // Kör själva spelet                    
                    //gameTime.TotalGameTime = new TimeSpan();
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.HighScore: // Visa HighScore-listan
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;

                case GameElements.State.Quit: // Avsluta spelet
                    this.Exit();
                    break;

                case GameElements.State.OnlySprite: // Null-case
                    break;

                default: // Tillbaka till menyn
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;
            }


            base.Update(gameTime);

            // Ligger numera i GameElements.cs

        }

   






        // ===============================================================================================
        // Draw(), Här ritas själva spelet ut.
        // gameTime, används för att hålla koll på spelets uppdateringsfrekvens.
        // <param name="gameTime">Provides a snapshot of timing values.</param>
        // ===============================================================================================
        protected override void Draw(GameTime gameTime)
        {
            
            // Rensa skärmen
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Använd spriteBatch för att rita ut saker på skärmen
            spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements.State.Run: // Kör själva spelet
                    GameElements.RunDraw(spriteBatch, gameTime);
                    break;

                case GameElements.State.HighScore: // HighScore-listan
                    GameElements.HighScoreDraw(spriteBatch);
                    break;

                case GameElements.State.Quit: // Avsluta spelet
                    this.Exit();
                    break;

                case GameElements.State.OnlySprite: // null-case
                    
                    break;

                default: // Tillbaka till menyn
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}








//               SpaceShooter kapitel 6 precis avslutat, kapitel 7 ej påbörjat.
// ===================================================================================================================================================


//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//namespace SpaceShooter
//{

//    /// This is the main type for your game.


//    // ===============================================================================================
//    // Game1, detta är vår huvudklass i spelet, det är här all action sker!
//    // ===============================================================================================
//    public class Game1 : Game
//    {
//        GraphicsDeviceManager graphics; // Används för grafik
//        SpriteBatch spriteBatch; // Används för att rita bilder

//        Texture2D ship_texture; // Rymdskeppets textur

//        Vector2 ship_vector; // Rymdskeppets koordinater
//        Vector2 ship_speed; // Rymdskeppets hastighet



//        // ===============================================================================================
//        // Game1(), klassens konstruktor
//        // ===============================================================================================
//        public Game1()
//        {
//            graphics = new GraphicsDeviceManager(this);
//            Content.RootDirectory = "Content";
//        }





//        // =================================================================================================================
//        // Initialize(), anropas då spelet startar.
//        // Här ligger all kod för att initiera objekt och skapa dem, dock inte laddning av olika filer (bilder, ljud mm...)
//        // =================================================================================================================
//        protected override void Initialize()
//        {


//            base.Initialize();

//            // Rymdskeppets startkoordinat
//            ship_vector.X = 380;
//            ship_vector.Y = 400;

//            // Rymdskeppets hastighet
//            ship_speed.X = 2.5f;
//            ship_speed.Y = -4.5f;
//        }



//        // ===============================================================================================
//        // LoadContent(), anropas då spelet startar.
//        // Anropas endast en gång per spel och laddar här in all content
//        // Här laddas alla objekt och filer in (bilder, ljud mm...)
//        // ===============================================================================================
//        protected override void LoadContent()
//        {
//            // Create a new SpriteBatch, which can be used to draw textures.
//            spriteBatch = new SpriteBatch(GraphicsDevice);

//            // TODO: use this.Content to load your game content here

//            ship_texture = Content.Load<Texture2D>("images/player/ship");
//        }




//        // ===============================================================================================
//        // UnloadContent(), anropas då spelet avslutas.
//        // Anropas en gång per spel, platsen man unloadar spelspecifikt content
//        // Här kan man ladda ur de objekt som skulle kunna behöva det för att rensa minne.

//        // ===============================================================================================
//        protected override void UnloadContent()
//        {
//            // TODO: Unload any non ContentManager content here
//        }




//        // ===============================================================================================
//        // Update(), första delen av spel-loopen.
//        // Här förändras olika objekt. T.ex, så läser vi in data från användaren, flyttar på olika objekt, kollar kollisioner av objekt, spelar audio osv.
//        // gameTime, används för att hålla kolla på spelets uppdateringsfrekvens.
//        // <param name="gameTime">Provides a snapshot of timing values.</param>
//        // ===============================================================================================
//        protected override void Update(GameTime gameTime)
//        {

//            // Stänger av spelet om man trycker på back-knappen på gamepaden
//            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
//                Exit();


//            // TODO: Add your update logic here


//            // Läs in tangenttryckningar:
//            KeyboardState keyboardState = Keyboard.GetState();


//            // Flytta rymdskeppet efter tangenttryckningar (om det inte är på väg ut från kanten):
//            if (keyboardState.IsKeyDown(Keys.Right))
//                ship_vector.X += ship_speed.X;
//            if (keyboardState.IsKeyDown(Keys.Left))
//                ship_vector.X -= ship_speed.X;
//            if (keyboardState.IsKeyDown(Keys.Up))
//                ship_vector.Y += ship_speed.Y;
//            if (keyboardState.IsKeyDown(Keys.Down))
//                ship_vector.Y -= ship_speed.Y;


//            if (ship_vector.X <= Window.ClientBounds.Width - ship_texture.Width && ship_vector.X >= 0)
//            {
//                if (keyboardState.IsKeyDown(Keys.Right))
//                    ship_vector.X += ship_speed.X;
//                if (keyboardState.IsKeyDown(Keys.Left))
//                    ship_vector.X -= ship_speed.X;
//            }

//            if (ship_vector.Y <= Window.ClientBounds.Height - ship_texture.Height && ship_vector.Y >= 0)
//            {
//                if (keyboardState.IsKeyDown(Keys.Up))
//                    ship_vector.Y += ship_speed.Y;
//                if (keyboardState.IsKeyDown(Keys.Down))
//                    ship_vector.Y -= ship_speed.Y;
//            }



//            // Kontrollera om rymdskeppet har åkt utanför kanten, om det har det, så återställ dess position

//            // Har det åkt ut till vänster:
//            if (ship_vector.X < 0)
//                ship_vector.X = 0;
//            // Har det åkt ut till höger:
//            if (ship_vector.X > Window.ClientBounds.Width - ship_texture.Width)
//                ship_vector.X = Window.ClientBounds.Width - ship_texture.Width;
//            // Har det åkt ut upptill:
//            if (ship_vector.Y < 0)
//                ship_vector.Y = 0;
//            // Har det åkt ut nedtill:
//            if (ship_vector.Y > Window.ClientBounds.Height - ship_texture.Height)
//                ship_vector.Y = Window.ClientBounds.Height - ship_texture.Height;





//            // ------------------Studsande rymdskepp-----------------------------------------------------------------------------------
//            //// Flytta på rymdskeppet
//            //ship_vector.X += ship_speed.X;

//            //// Kontrollera så rymdskeppet inte åker utanför fönstret på sidorna
//            //if (ship_vector.X >  Window.ClientBounds.Width - ship_texture.Width || ship_vector.X < 0)
//            //{
//            //    ship_speed.X *= -1; // Byt riktning på rymdskeppet
//            //}


//            //ship_vector.Y += ship_speed.Y;

//            //// Kontrollera så rymdskeppet inte åker utanför fönstret på uppe/nere
//            //if (ship_vector.Y > Window.ClientBounds.Height - ship_texture.Height || ship_vector.Y < 0)
//            //{
//            //    ship_speed.Y *= -1; // Byt riktning på rymdskeppet
//            //}
//            // ------------------------------------------------------------------------------------------------------------------------



//            base.Update(gameTime);
//        }






//        // ===============================================================================================
//        // Draw(), Här ritas själva spelet ut.
//        // gameTime, används för att hålla koll på spelets uppdateringsfrekvens.
//        // <param name="gameTime">Provides a snapshot of timing values.</param>
//        // ===============================================================================================
//        protected override void Draw(GameTime gameTime)
//        {
//            // Rensa skärmen
//            GraphicsDevice.Clear(Color.CornflowerBlue);

//            // TODO: Add your drawing code here

//            // Använd spriteBatch för att rita ut saker på skärmen
//            spriteBatch.Begin();
//            spriteBatch.Draw(ship_texture, ship_vector, Color.White);
//            spriteBatch.End();

//            base.Draw(gameTime);
//        }
//    }
//}








// ------------------Studsande rymdskepp-----------------------------------------------------------------------------------
//// Flytta på rymdskeppet
//ship_vector.X += ship_speed.X;

//// Kontrollera så rymdskeppet inte åker utanför fönstret på sidorna
//if (ship_vector.X >  Window.ClientBounds.Width - ship_texture.Width || ship_vector.X < 0)
//{
//    ship_speed.X *= -1; // Byt riktning på rymdskeppet
//}


//ship_vector.Y += ship_speed.Y;

//// Kontrollera så rymdskeppet inte åker utanför fönstret på uppe/nere
//if (ship_vector.Y > Window.ClientBounds.Height - ship_texture.Height || ship_vector.Y < 0)
//{
//    ship_speed.Y *= -1; // Byt riktning på rymdskeppet
//}
// ------------------------------------------------------------------------------------------------------------------------





