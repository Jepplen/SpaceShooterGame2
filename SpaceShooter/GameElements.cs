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

    // =================================================================================
    // GameState, statisk klass med metoder för olika game states.
    // Varje game state har två metoder vardera.
    // En för Update-funktionalitet och en för draw-funktionalitet.
    // Quit har inga egna metoder, vilket gör att spelet avslutas direkt.
    // =================================================================================
    static class GameElements
    {

        
        static Menu menu;

        public static bool gameCanStart = true;

       
        static Player player;      
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;
        static PrintText printText;
        static Background background;

        static List<EnemyTripodRed> tripodRedList;
        static List<EnemyTripodGreen> tripodGreenList;

        static List<PowerUpWeaponLaser> powerUpList;
        static bool newPowerUpWeaponLaser = true;

        static List<EnemyBossJarJar> enemyBossList;
        static bool newBossJarJar = true;

        static List<CrashAnimation> crashAnimationList;
        static Texture2D crashSprite;

        static Texture2D powerUpWeaponLaserSprite;
        static Texture2D tripodSpriteGreen;
        static Texture2D tripodSpriteRed;
        static Texture2D enemyBossSpriteJarJar;

        static bool jarJarIsDead = false;


        static List<MineEnemy> mineList;
        static Texture2D mikaMineSprite;

        static Random random = new Random();

        // Olika game states
        public enum State { Menu, Run, HighScore, Quit, OnlySprite};
        public static State currentState;
       
        public static double spawnTimeReference;
        public static double deathSpriteTimereference;




        // =================================================================================================================
        // Initialize(), anropas då spelet startar.
        // Här ligger all kod för att initiera objekt och skapa dem, dock inte laddning av olika filer (bilder, ljud mm...)
        // =================================================================================================================
        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();

            tripodGreenList = new List<EnemyTripodGreen>();
            tripodRedList = new List<EnemyTripodRed>();
            enemyBossList = new List<EnemyBossJarJar>();
            powerUpList = new List<PowerUpWeaponLaser>();
            crashAnimationList = new List<CrashAnimation>();
            mineList = new List<MineEnemy>();
        }





        // ===============================================================================================
        // LoadContent(), anropas då spelet startar.
        // Anropas endast en gång per spel och laddar här in all content
        // Här laddas alla objekt och filer in (bilder, ljud mm...)
        // ===============================================================================================
        public static void LoadContent(ContentManager content, GameWindow window)
        {

            // Skapa menyn:
            menu = new Menu((int)State.Menu); // Typomvandling från state till int
            menu.AddItem(content.Load<Texture2D>("images/menu/menustart"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("images/menu/menuhighscore"), (int)State.HighScore);
            menu.AddItem(content.Load<Texture2D>("images/menu/menuexit"), (int)State.Quit);

            // Manuell addering av menybakgrund (Är egentligen en menuItem, men kan aldrig väljas)
            menu.AddItemBackground(content.Load<Texture2D>("images/menu/menubackgroundtransparent"), (int)State.OnlySprite);


            // Skapar bakgrundsbilden i spelet
            background = new Background(content.Load<Texture2D>("images/background/backgroundspace_jeppel_small"), window);

            

            // Skapar spelarobjekt
            player = new Player(content.Load<Texture2D>("images/player/shipmika3"), 380, 400, 2.5f, -4.5f, content.Load<Texture2D>("images/player/bulletmika2"));




            // Laddar powerUpWeaponLaserSprite
            powerUpWeaponLaserSprite = content.Load<Texture2D>("images/player/powerup_weapon_laser");

            // Laddar EnemyTripodGreen sprite
            crashSprite = content.Load<Texture2D>("images/enemies/crashsprite");

            // Laddar enemy Mine sprite
            mikaMineSprite = content.Load<Texture2D>("images/enemies/minemika");

            // Laddar EnemyTripodGreen sprite
            tripodSpriteGreen = content.Load<Texture2D>("images/enemies/tripodGreen");

            // Laddar EnemyTripodRed sprite
            tripodSpriteRed = content.Load<Texture2D>("images/enemies/tripodred");

            // Laddar enemyBossJarJar sprite
            enemyBossSpriteJarJar = content.Load<Texture2D>("images/enemies/boss_jarjar");

            // Laddar guldmynt sprite
            goldCoinSprite = content.Load<Texture2D>("images/powerups/coinmika");

            // Laddar text sprite
            printText = new PrintText(content.Load<SpriteFont>("spriteFonts/gameText"));
        
    }



        // __________________________________ Menu-metoder_________________________________________________________________________________
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // ===============================================================================================
        // MenuUpdate(), kontrollerar om användaren väljer något av menyvalen
        // ===============================================================================================
        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime); // Typomvandling från int till state           
        }






        // ===============================================================================================
        // MenuDraw(), ritar ut menyn
        // ===============================================================================================
        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            menu.Draw(spriteBatch);
        }






        // __________________________________ Run-metoder__________________________________________________________________________________
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // ===============================================================================================
        // RunUpdate(), update-metod för själva spelet
        // ===============================================================================================
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            // Stänger av spelet om man trycker på back-knappen på gamepaden
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            

            if (gameCanStart == true)
            {
                spawnTimeReference = gameTime.TotalGameTime.TotalMilliseconds;
                gameCanStart = false;
            }

            background.Update(window);
            
            // Anropar i update
            player.Update(window, gameTime);        
            
            
            // Tillägg av fiender
            GenerateTripodsGreen(window, content, gameTime, 50, 9000, 17000);
            GenerateGoldCoins(window, content, gameTime, 400, 15000, 20000);
            GenerateTripodsGreen(window, content, gameTime, 45, 19000, 23000);
            GenerateMines(window, content, gameTime, 50, 21000, 28000);
            GenerateTripodsRed(window, content, gameTime, 50, 30000, 40000);
            GenerateGoldCoins(window, content, gameTime, 400, 32000, 60000);
            GenerateMines(window, content, gameTime, 50, 35000, 42000);
            GenerateMines(window, content, gameTime, 50, 38000, 44000);
            GenerateTripodsGreen(window, content, gameTime, 50, 42000, 58000);
            GenerateMines(window, content, gameTime, 50, 54000, 58000);

            GeneratePowerUpWeaponLaser(window, content, gameTime, 62000, 63000);

            GenerateTripodsRed(window, content, gameTime, 25, 70000, 116000);
            GenerateMines(window, content, gameTime, 25, 80000, 90000);
            GenerateTripodsGreen(window, content, gameTime, 25, 85000, 116000);
            GenerateMines(window, content, gameTime, 15, 90000, 116000);

            GenerateBossJarJar(window, content, gameTime, 130000, 131000);            

            CheckCollision(gameTime, window, content);
           






            if (!player.IsAlive) // Spelaren är död
            {
                gameCanStart = true;
                Reset(window, content, gameTime); // Återställ alla spelobjekt
                return State.Menu; // Återgå till menyn
                
            }

            return State.Run; // Stanna kvar i Run
                        
        }
    






        // ===============================================================================================
        // RunDraw(), metod för att rita ut själva spelet
        // ===============================================================================================
        public static void RunDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            background.Draw(spriteBatch);
                   
            player.Draw(spriteBatch);

                   
            // Visa hur mycket poäng spelaren har
            foreach (GoldCoin gc in goldCoins)
            {
                gc.Draw(spriteBatch);
                
            }

            foreach (CrashAnimation ca in crashAnimationList)
            {
                ca.Draw(spriteBatch);
            }

            foreach (EnemyTripodGreen etg in tripodGreenList)
            {
                etg.Draw(spriteBatch);
            }

            foreach (EnemyTripodRed etr in tripodRedList)
            {
                etr.Draw(spriteBatch);
            }

            foreach (MineEnemy me in mineList)
            {
                me.Draw(spriteBatch);
            }

            foreach (EnemyBossJarJar ebjj in enemyBossList)
            {
                ebjj.Draw(spriteBatch);
            }

            foreach (PowerUpWeaponLaser puwl in powerUpList)
            {
                puwl.Draw(spriteBatch);
            }

            printText.Print("Points: " + player.Points, spriteBatch, 0, 0);

            // Spelstart Get Ready!
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 3000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 6000)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);             
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 6200 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 6400)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 6600 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 6800)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 7000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 7200)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 7400 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 7600)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }


            // Laser Upgrade text
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 62000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 64500)
            {
                printText.Print("That looks like a laser upgrade!", spriteBatch, 300, 200);
            }

            // Swarm!
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 66000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 68000)
            {
                printText.Print("Watch out! They're coming!", spriteBatch, 300, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 68200 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 68400)
            {
                printText.Print("Watch out! They're coming!", spriteBatch, 300, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 68600 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 68800)
            {
                printText.Print("Watch out! They're coming!", spriteBatch, 300, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 69000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 69200)
            {
                printText.Print("Watch out! They're coming!", spriteBatch, 300, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 69400 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 69600)
            {
                printText.Print("Watch out! They're coming!", spriteBatch, 300, 200);
            }


            // Laser overheated
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 120000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 123000)
            {
                printText.Print("Darn it! The awesome laser upgrade has melted!", spriteBatch, 200, 200);
            }
   



            // Boss Fight
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 125000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 128000)
            {
                printText.Print("Oh no! You have awakened him!", spriteBatch, 280, 200);
            }
    
            // Get Ready!
           if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 128200 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 128400)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 128600 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 128800)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 129000 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 129200)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + 129400 && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + 129600)
            {
                printText.Print("Get Ready!", spriteBatch, 360, 200);
            }



            if (jarJarIsDead == true && gameTime.TotalGameTime.TotalMilliseconds > deathSpriteTimereference + 1000 && gameTime.TotalGameTime.TotalMilliseconds < deathSpriteTimereference + 5000)
            {
                printText.Print("Very good job, Congratulations!", spriteBatch, 300, 200);
            }

            if (jarJarIsDead == true && gameTime.TotalGameTime.TotalMilliseconds > deathSpriteTimereference + 7000)
            {
                printText.Print("Press ESCAPE to exit", spriteBatch, 200, 200);
            }


           
        }


        // __________________________________ HighScore-metoder____________________________________________________________________________
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // ===============================================================================================
        // HighScoreUpdate(), update-metod för för highscore-listan
        // ===============================================================================================
        public static State HighScoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            
            // Återgå till menyn om man trycker ESC:
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return State.Menu;
            }

            return State.HighScore; // Stanna kvar i HighScore
        }






        // ===============================================================================================
        // HighScoreDraw(), metod för att rita ut highscore-listan
        // ===============================================================================================
        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            // Rita ut HighScore-listan (Saknar vägledning i läroboken)
        }



        // =========================================================================================================================
        // Reset(), återställer alla objekt så att man har möjligheten att starta ett nytt spel.
        // (Denna metod ser mycket ut som LoadContent().)
        // Innehållet i de båda bör läggas i en separat metod som de båda anropar...
        // Det ligger nu i metoden GenerateEnemies()
        // =========================================================================================================================
        public static void Reset(GameWindow window, ContentManager content, GameTime gameTime)
        {
            player.Reset(380, 400, 2.5f, -4.5f); // Reset player position på skärmen

           // Tömmer samtliga listor
            crashAnimationList.Clear();
            tripodRedList.Clear();
            tripodGreenList.Clear();
            enemyBossList.Clear();
            mineList.Clear();
            powerUpList.Clear();

            

        }


        



        public static void GenerateTripodsGreen(GameWindow window, ContentManager content, GameTime gameTime, int chanceToSpawn, int existanceFromMilliseconds, int existanceToMilliseconds)
        {
            
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {
                int newTripod = random.Next(1, chanceToSpawn);

                if (newTripod == 1) // Ok, nytt tripod ska uppstå
                {
                    // Var ska tripoden uppstå:
                    int rndX = random.Next(0, window.ClientBounds.Width - tripodSpriteGreen.Width);                  
                    int posY = 0 - tripodSpriteGreen.Height;

                    // Lägg till tripod:en i listan tripodList
                    tripodGreenList.Add(new EnemyTripodGreen(tripodSpriteGreen, rndX, posY, gameTime));
                }
            }
        }

        public static void GenerateTripodsRed(GameWindow window, ContentManager content, GameTime gameTime, int chanceToSpawn, int existanceFromMilliseconds, int existanceToMilliseconds)
        {
            
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {
                int newTripod = random.Next(1, chanceToSpawn);

                if (newTripod == 1) // Ok, nytt tripod ska uppstå
                {
                    // Var ska tripoden uppstå:
                    int rndX = random.Next(0, window.ClientBounds.Width - tripodSpriteRed.Width);
                    int posY = 0 - tripodSpriteRed.Height;

                    // Lägg till tripod:en i listan tripodList
                    tripodRedList.Add(new EnemyTripodRed(tripodSpriteRed, rndX, posY, gameTime));
                }
            }
        }

        public static void GenerateMines(GameWindow window, ContentManager content, GameTime gameTime, int chanceToSpawn, int existanceFromMilliseconds, int existanceToMilliseconds)
        {
            
            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {
                int newMine = random.Next(1, chanceToSpawn);

                if (newMine == 1) // Ok, en ny mina ska uppstå
                {
                    // Var ska minan uppstå:
                    int rndX = random.Next(0, window.ClientBounds.Width - mikaMineSprite.Width);
                    int posY = 0 - mikaMineSprite.Height;

                    // Lägg till tripod:en i listan tripodList
                    mineList.Add(new MineEnemy(mikaMineSprite, rndX, posY, gameTime, window));
                }
            }
        }


        public static void GenerateBossJarJar(GameWindow window, ContentManager content, GameTime gameTime, int existanceFromMilliseconds, int existanceToMilliseconds)
        {

            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {
                

                if (newBossJarJar == true) // Ok, bossen ska spawna
                {
                    // Var ska bossen uppstå:
                    int rndX = random.Next(0, window.ClientBounds.Width - enemyBossSpriteJarJar.Width);
                    int posY = 0 - enemyBossSpriteJarJar.Height;

                   
                    enemyBossList.Add(new EnemyBossJarJar(enemyBossSpriteJarJar, rndX, posY, gameTime, window));

                    newBossJarJar = false;
                }
            }
        }


        public static void GeneratePowerUpWeaponLaser(GameWindow window, ContentManager content, GameTime gameTime, int existanceFromMilliseconds, int existanceToMilliseconds)
        {

            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {


                if (newPowerUpWeaponLaser == true) // Ok, vapenuppgraderingen ska spawna
                {
                    // Var ska den uppstå:
                    int posX = 390;
                    int posY = 0 - powerUpWeaponLaserSprite.Height;

                    // Lägg till den i listan
                    powerUpList.Add(new PowerUpWeaponLaser(powerUpWeaponLaserSprite, posX, posY, gameTime, window));

                    newPowerUpWeaponLaser = false;
                }
            }
        }


        // ==================================================================================================================
        // CheckCollision(), metod för att gå igenom alla listor och leta kollisioner
        // ==================================================================================================================

        public static void CheckCollision(GameTime gameTime, GameWindow window, ContentManager content)
        {


            // Gå igenom hela listan med existerande PowerUpWeaponLaser
            foreach (PowerUpWeaponLaser puwl in powerUpList.ToList())
            {
                if (puwl.IsAlive) // Kontrollera om fienden lever
                {
                    // gd.Update(), kollar om fienden har blivit för gammalt för att få leva vidare:
                    puwl.Update(gameTime, window);

                    // Kontrollera om fienden har kolliderat med spelaren:
                    if (puwl.CheckCollision(player))
                    {
                        powerUpList.Remove(puwl);
                        Player.rateOfFire = 25;
                    }
                }

                else // Ta bort fienden för det är dött
                {
                    powerUpList.Remove(puwl);
                }
            }

            

            // Gå igenom hela listan med existerande EnemyBossJarJar
            foreach (EnemyBossJarJar ebjj in enemyBossList.ToList())
            {
                if (ebjj.IsAlive) // Kontrollera om fienden lever
                {
                    // gd.Update(), kollar om fienden har blivit för gammalt för att få leva vidare:
                    ebjj.Update(gameTime, window);

                    // Kontrollera om fienden har kolliderat med spelaren:
                    if (ebjj.CheckCollision(player))
                    {
                        player.IsAlive = false;
                    }
                }

                else // Ta bort fienden för det är dött
                {
                    enemyBossList.Remove(ebjj);
                }
            }

            // Gå igenom alla fiender för att se om spelaren har kolliderat med bullets
            foreach (EnemyBossJarJar ebjj in enemyBossList.ToList())
            {
             
                    foreach (Bullet b in player.Bullets)
                    {

                        if (ebjj.CheckCollision(b)) // Kollision uppstod
                        {
                            ebjj.IsDamaged++; // fienden har blivit skadad

                            b.IsAlive = false;

                            if (ebjj.IsDamaged > 20) // Kollision uppstod
                            {
                                ebjj.IsAlive = false; // Döda fiende

                                ebjj.IsDestroyed = true; // Triggar death sprite

                                player.Points = player.Points + 20000; // Ge spelaren poäng

                                jarJarIsDead = true;

                                deathSpriteTimereference = gameTime.TotalGameTime.TotalMilliseconds;

                            }
                        }

                        if (ebjj.IsDestroyed == true && gameTime.TotalGameTime.TotalMilliseconds < deathSpriteTimereference + 200)
                        {
                            bool newCrashSite = true;

                            if (newCrashSite == true) // Ok, ny krasch ska uppstå
                            {
                                // Var ska den uppstå
                                float posX = ebjj.X;
                                float posY = ebjj.Y;

                                // Lägg till i listan
                                crashAnimationList.Add(new CrashAnimation(crashSprite, posX, posY, gameTime));
                            }


                        }

                   


                        foreach (CrashAnimation ca in crashAnimationList.ToList())
                        {
                            if (ca.IsAlive) 
                            {
                                
                                ca.Update(gameTime);
                            }

                            else 
                            {
                                crashAnimationList.Remove(ca);
                            }

                        }


                    }

            }



            if (gameTime.TotalGameTime.TotalMilliseconds > deathSpriteTimereference + 250)
            {
                crashAnimationList.Clear();
            }

            // Gå igenom hela listan med existerande TripodEnemy
            foreach (EnemyTripodGreen etg in tripodGreenList.ToList())
            {
                if (etg.IsAlive) // Kontrollera om fienden lever
                {
                    // gd.Update(), kollar om fienden har blivit för gammalt för att få leva vidare:
                    etg.Update(gameTime, window);

                    // Kontrollera om fienden har kolliderat med spelaren:
                    if (etg.CheckCollision(player))
                    {
                        player.IsAlive = false;
                    }
                }

                else // Ta bort fienden för det är dött
                {
                    tripodGreenList.Remove(etg);
                }
            }

            // Gå igenom alla fiender för att se om spelaren har kolliderat med bullets
            foreach (EnemyTripodGreen etg in tripodGreenList.ToList())
            {
                foreach (Bullet b in player.Bullets)
                {

                    if (etg.CheckCollision(b)) // Kollision uppstod
                    {
                        etg.IsDamaged++; // fienden har blivit skadad

                        b.IsAlive = false;

                        if (etg.IsDamaged > 0) // Kollision uppstod
                        {
                            etg.IsAlive = false; // Döda fiende

                            etg.IsDestroyed = true; // Triggar death sprite

                            player.Points = player.Points + 1; // Ge spelaren poäng

                            deathSpriteTimereference = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }

                    if (etg.IsDestroyed == true && gameTime.TotalGameTime.TotalMilliseconds < deathSpriteTimereference + 50)
                    {
                        bool newCrashSite = true;

                        if (newCrashSite == true) // Ok, ny krasch ska uppstå
                        {
                            // Var ska den uppstå
                            float posX = etg.X;
                            float posY = etg.Y;

                            // Lägg till i listan
                            crashAnimationList.Add(new CrashAnimation(crashSprite, posX, posY, gameTime));
                        }


                    }

                    
                    foreach (CrashAnimation ca in crashAnimationList.ToList())
                    {
                        if (ca.IsAlive)
                        {
                            
                            ca.Update(gameTime);
                        }

                        else
                        {
                            crashAnimationList.Remove(ca);
                        }

                    }


                }

            }


            // Gå igenom hela listan med existerande TripodEnemyRed
            foreach (EnemyTripodRed etr in tripodRedList.ToList())
            {
                if (etr.IsAlive) // Kontrollera om fienden lever
                {
                    // gd.Update(), kollar om fienden har blivit för gammalt för att få leva vidare:
                    etr.Update(gameTime, window);

                    // Kontrollera om fienden har kolliderat med spelaren:
                    if (etr.CheckCollision(player))
                    {
                        player.IsAlive = false;
                    }
                }

                else // Ta bort fienden för det är dött
                {
                    tripodRedList.Remove(etr);
                }
            }

            // Gå igenom alla fiender för att se om spelaren har kolliderat med bullets
            foreach (EnemyTripodRed etr in tripodRedList.ToList())
            {
                foreach (Bullet b in player.Bullets)
                {

                    if (etr.CheckCollision(b)) // Kollision uppstod
                    {
                        etr.IsDamaged++; // fienden har blivit skadad

                        b.IsAlive = false;

                        if (etr.IsDamaged > 1) // Kollision uppstod
                        {
                            etr.IsAlive = false; // Döda fiende

                            etr.IsDestroyed = true; // Triggar death sprite

                            player.Points = player.Points + 2; // Ge spelaren poäng

                            deathSpriteTimereference = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }

                    if (etr.IsDestroyed == true && gameTime.TotalGameTime.TotalMilliseconds < deathSpriteTimereference + 50)
                    {
                        bool newCrashSite = true;

                        if (newCrashSite == true) // Ok, ny krasch ska uppstå
                        {
                            // Var ska den uppstå
                            float posX = etr.X;
                            float posY = etr.Y;

                            // Lägg till i listan
                            crashAnimationList.Add(new CrashAnimation(crashSprite, posX, posY, gameTime));
                        }


                    }

                    
                    foreach (CrashAnimation ca in crashAnimationList.ToList())
                    {
                        if (ca.IsAlive) // Kontrollera om Crashspriten lever
                        {
                            // ca.Update(), kollar om crashspriten har blivit för gammalt för att få leva vidare:
                            ca.Update(gameTime);                          
                        }

                        else // Ta bort crashspriten för det är dött
                        {
                            crashAnimationList.Remove(ca);
                        }

                    }


                }

            }


           
            foreach (MineEnemy me in mineList.ToList())
            {
                if (me.IsAlive) 
                {
                    
                    me.Update(gameTime, window);

                    // Kontrollera om fienden har kolliderat med spelaren:
                    if (me.CheckCollision(player))
                    {
                        player.IsAlive = false;
                    }
                }

                else // Ta bort fienden för det är dött
                {
                    mineList.Remove(me);
                }
            }

            // Gå igenom alla fiender för att se om spelaren har kolliderat med bullets
            foreach (MineEnemy me in mineList.ToList())
            {
                foreach (Bullet b in player.Bullets)
                {

                    if (me.CheckCollision(b)) // Kollision uppstod
                    {
                        me.IsDamaged++; // fienden har blivit skadad

                        b.IsAlive = false;

                        if (me.IsDamaged > 0) // Kollision uppstod
                        {
                            me.IsAlive = false; // Döda fiende

                            me.IsDestroyed = true; // Triggar death sprite

                            player.Points = player.Points + 1; // Ge spelaren poäng

                            deathSpriteTimereference = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }

                    if (me.IsDestroyed == true && gameTime.TotalGameTime.TotalMilliseconds < deathSpriteTimereference + 50)
                    {
                        bool newCrashSite = true;

                        if (newCrashSite == true) // Ok, ny krasch ska uppstå
                        {
                            // Var ska den uppstå
                            float posX = me.X;
                            float posY = me.Y;

                            // Lägg till i listan
                            crashAnimationList.Add(new CrashAnimation(crashSprite, posX, posY, gameTime));
                        }


                    }

                  
                    foreach (CrashAnimation ca in crashAnimationList.ToList())
                    {
                        if (ca.IsAlive)
                        {
                            
                            ca.Update(gameTime);
                        }

                        else
                        {
                            crashAnimationList.Remove(ca);
                        }

                    }


                }

            }


            // Gå igenom hela listan med existerande guldmynt
            foreach (GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive) // Kontrollera om guldmyntet lever
                {
                    // gd.Update(), kollar om guldmyntet har blivit för gammalt för att få leva vidare:
                    gc.Update(gameTime);

                    // Kontrollera om guldmyntet har kolliderat med spelaren:
                    if (gc.CheckCollision(player))
                    {
                        // Ta bort myntet vid kollision:
                        goldCoins.Remove(gc);
                        player.Points = player.Points + Settings.PlayerPointsGoldCoin; // ... och ge spelaren poäng
                    }
                }

                else // Ta bort guldmyntet för det är dött
                {
                    goldCoins.Remove(gc);
                }
            }

           
        }
    
      

        public static void GenerateGoldCoins(GameWindow window, ContentManager content, GameTime gameTime, int chanceToSpawn, int existanceFromMilliseconds, int existanceToMilliseconds)
        {

            if (gameTime.TotalGameTime.TotalMilliseconds > spawnTimeReference + existanceFromMilliseconds && gameTime.TotalGameTime.TotalMilliseconds < spawnTimeReference + existanceToMilliseconds)
            {
                // Guldmynten ska uppstå slumpmässigt, en chans på 200:                
                int newCoin = random.Next(1, chanceToSpawn);

                if (newCoin == 1) // Ok, nytt guldmynt ska uppstå
                {
                    // Var ska guldmyntet uppstå:
                    int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);

                    // Lägg till guldmyntet i listan GoldCoins
                    goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
                }


           
            }
        }

    }
}

