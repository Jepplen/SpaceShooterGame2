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

    // ====================================================================================
    // Menu, används för att skapa en meny, lägga till menyval i menyn,
    // samt att ta emot tangenttryckningar för olika menyval och att rita ut menyn
    // ====================================================================================
    class Menu
    {
        List<MenuItem> menu; // Lista på menuItems
        int selected = 0; // FÖrsta valet o listan är valt

        // currentHeight används för att rita ut menuItems på olika höjd
        float currentHeight = 0;

        // lastChange används för att "pausa" tangenttryckningar, så att det inte ska gå för fort att bläddra bland menyvalen:
        double lastChange = 0;

        // Det state som representerar själva menyn
        int defaultMenuState;

       




        // ====================================================================================
        // Menu(), konstruktor som skapar listan MenuItem:s
        // ====================================================================================
        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }


        // ====================================================================================
        // AddItem(), lägger till ett menyval i listan
        // ====================================================================================
        public void AddItem(Texture2D itemTexture, int state)
        {
            // Sätt höjd på item:
            float X = 285;
            float Y = 220 + currentHeight;

            // Ändra currentHeight efter detta items höjd + 20 pixlar för lite exta mellanrum
            currentHeight += itemTexture.Height + 20;

            // Skapa ett temporärt objekt och lägg det i listan:
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        // ====================================================================================
        // AddItemBackground(), lägger till ett menyval i listan // Endast för Menybakgrunden
        // ====================================================================================
        public void AddItemBackground(Texture2D itemTexture, int state)
        {
            // Sätt höjd på item:
            float X = 0;
            float Y = 0;     

            // Skapa ett temporärt objekt och lägg det i listan:
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }


        // ====================================================================================
        // Update(), kollar om användaren tryckt någon tangent.
        // Antingen kan piltangenterna användas för att välja en viss MenuItem (utan att gå in i just det valet),
        // eller så kan ENTER användas för att gå in i den valda MenuItem:en
        // ====================================================================================
        public int Update(GameTime gameTime)
        {
            // Läs in tangenttryckningar:
            KeyboardState keyboardState = Keyboard.GetState();

            // Byte mellan olika menyval.
            // Först måste vi dock kontrollera så användaren inte precis nyligen bytte menyval.
            // Vi vill ju inte att det ska ändras 30 eller 60 gånger per sekund!
            // Därför pausar vi i 130 millisekunder:
            if (lastChange + 150 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                // Gå ett steg ned i menyn
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    // Om vi har gått utanför de möjliga valen, så vill vi att det första menyvalet ska väljas:
                    if (selected > 2)
                    {
                        selected = 2; // Det första menyvalet
                    }
                }
                // Gå ett steg upp i menyn
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    // Om vi har gått utanför de möjliga valen (alltså negative siffror), så vill vi att det sista menyvalet ska väljas:
                    if (selected < 0)
                    {
                        selected = 0; // Det sista menyvalet
                    }
                }

                // Ställ lastChange till exakt detta ögonblick:
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            //// Välj ett menyval med ENTER:
            if (keyboardState.IsKeyDown(Keys.Enter))
            {               
                return menu[selected].State; // returnera menyvalets state
            }

            //Om inget menyval har valts, så stannar vi kvar i menyn:
            return defaultMenuState;
        }


        // ====================================================================================
        // Draw(), ritar ut menyn
        // ====================================================================================
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                // Om vi har ett aktivt menyval ritar vi ut det med en speciell färgton
                if (i == selected)
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                }
                else // Annars ingen färgtoning alls
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
                }
            }
        }
        

        // ====================================================================================
        // MenuItem, container-klass för ett menyval
        // ====================================================================================
        class MenuItem
        {
            Texture2D texture; // Bilden för menyvalet
            Vector2 position; // Positionen för menyvalet på skärmen
            int currentState; // menyvalets state

            // ====================================================================================
            // MenuItem(), konstruktor som sätter värden för de olika manyvalen
            // ====================================================================================
            public MenuItem(Texture2D texture, Vector2 position, int currentState)
            {
                this.texture = texture;
                this.position = position;
                this.currentState = currentState;
            }

            // ====================================================================================
            // (Get-) egenskaper för klassen MenuItem
            // ====================================================================================
            public Texture2D Texture { get { return texture; } }
            public Vector2 Position { get { return position; } }
            public int State { get { return currentState; } }
        }

    }
}
