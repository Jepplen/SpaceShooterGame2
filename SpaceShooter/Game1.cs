﻿using System;
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

  
        public static double spawnTimeReferenceSavedTimeFromPause;
        public static double gameTimeReferenceSavedTimeFromPause;
        public static double gameTimeReferenceSavedTimeFromPauseDifference;
        bool gamePause = true;
        

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

            


            //// Stänger av spelet om man trycker på back-knappen på gamepaden:
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //{
            //    GameElements.gameCanStart = true;
            //    this.Exit();

            //}


            // OM spelaren har dött måst den trycka escape för att komma till menyn
            if (GameElements.gameOver) // Spelaren är död
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                }
            }

            // Om spelaren har tryckt Escape in-game så pausar hela spelet
            if (GameElements.player.EscapeIsPressed) // Spelaren har tryckt Escape in-game
            {

                if (gamePause)
                {
                    gameTimeReferenceSavedTimeFromPause = gameTime.TotalGameTime.TotalMilliseconds; // Sparar game time
                    spawnTimeReferenceSavedTimeFromPause = GameElements.spawnTimeReference; // Sparar Spawntimereference
                    gameTimeReferenceSavedTimeFromPauseDifference = gameTime.TotalGameTime.TotalMilliseconds - GameElements.spawnTimeReference; // Sparar hur långt tid har gått i spelet
                    GameElements.spawnTimeReference = GameElements.spawnTimeReference + 200000; // Ställer Spawntimereference tillen tid som aldrig kan trigga något i spelet
                    gamePause = false;
                }
               

                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Y))
                {
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                }
                else if (keyboardState.IsKeyDown(Keys.N))
                {
                    GameElements.spawnTimeReference = gameTime.TotalGameTime.TotalMilliseconds - gameTimeReferenceSavedTimeFromPauseDifference; // Återställer Spawntimereferernce till när spelaren först tryckte på escape
                    GameElements.player.EscapeIsPressed = false;
                    gamePause = true;
                    
                }
                

            }                      


            if (!GameElements.player.EscapeIsPressed)
            {
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




        }

   
        

        // ===============================================================================================
        // Draw(), Här ritas själva spelet ut.
        // gameTime, används för att hålla koll på spelets uppdateringsfrekvens.
        // <param name="gameTime">Provides a snapshot of timing values.</param>
        // ===============================================================================================
        protected override void Draw(GameTime gameTime)
        {

          

            // Använd spriteBatch för att rita ut saker på skärmen
            spriteBatch.Begin();

            if (GameElements.player.EscapeIsPressed) // Spelaren har tryckt Escape in-game
            {
                

                GameElements.printText.Print("Are you sure you want to quit?", spriteBatch, 280, 220);
                GameElements.printText.Print("Press Y to quit or N to continue", spriteBatch, 280, 260);                

            }


            if (GameElements.gameOver) // Spelaren är död
            {
                GameElements.printText.Print("Game Over", spriteBatch, 350, 220);
                GameElements.printText.Print("Press Escape to exit", spriteBatch, 315, 260);
            }
          

            if (!GameElements.player.EscapeIsPressed && GameElements.gameOver == false)
            {

                // Rensa skärmen
                GraphicsDevice.Clear(Color.Black);

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

                

                base.Draw(gameTime);
            }

            spriteBatch.End();

        }
    }
}
