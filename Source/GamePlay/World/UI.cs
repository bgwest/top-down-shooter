using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace top_down_shooter
{
    public class UI
    {
        public SpriteFont font;

        public QuantityDisplayBar healthBar;

        public UI()
        {
            font = Globals.content.Load<SpriteFont>("2d/Fonts/Arial16");

            // total width including the background of the bar
            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red);
        }

        // in future can make this a packet and pass in only what is needed
        // but it's only a pointer and is not taking up a ton of memory doing it this way
        public void Update(World WORLD)
        {
            healthBar.Update(WORLD.hero.health, WORLD.hero.healthMax);
        }

        public void Draw(World WORLD)
        {
            string tempString = "Kill Count: " + WORLD.killCount;
            Vector2 stringDimensions = font.MeasureString(tempString);
            Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight - 40), Color.Black);

            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));
        }
    }
}
