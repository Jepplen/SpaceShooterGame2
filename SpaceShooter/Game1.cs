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
                GameElements.gameCanStart = true;
                this.Exit();

            }


            switch (GameElements.currentState)
            {
                case GameElements.State.Run: // Kör själva spelet                    
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.HighScore: // Visa HighScore-listan
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;

                case GameElements.State.Quit: // Avsluta spelet
                    this.Exit();
                    break;

                case GameElements.State.OnlySprite: // Null-case till menybakgrunden, går ej att välja
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

                case GameElements.State.OnlySprite: // null-case till menybakgrunden, går ej att välja
                    
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
