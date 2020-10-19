using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace top_down_shooter
{
    public class UI
    {
        public Basic2d pauseOverlay;

        public SpriteFont font;

        public QuantityDisplayBar healthBar;

        public UI()
        {
            pauseOverlay = new Basic2d("2d/misc/PauseOverlay", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(300, 300));

            font = Globals.content.Load<SpriteFont>("2d/Fonts/Arial16");

            // total width including the background of the bar
            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red);
        }

        // in future can make this a packet and pass in only what is needed
        // but it's only a pointer and is not taking up a ton of memory doing it this way
        public void Update(World WORLD)
        {
            healthBar.Update(WORLD.user.hero.health, WORLD.user.hero.healthMax);
        }

        public void Draw(World WORLD)
        {
            // TODO: Shader needs to be re-processed in order to run on this version of monogame
            //       uncomment and try running after-reprocessing
            // since text needs to be pixel for pixel we override the shader loop and say "don't skip any pixels" basically
            //Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            //Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            //Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            //Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            //Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            //Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            string tempString = "Score: " + GameGlobals.score;
            Vector2 stringDimensions = font.MeasureString(tempString);
            Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight - 40), Color.Black);

            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            if (WORLD.user.hero.dead || WORLD.user.buildings.Count <= 0)
            {
                tempString = "Press Enter to Restart";
                stringDimensions = font.MeasureString(tempString);
                Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight / 2), Color.Black);
            }

            if (GameGlobals.paused)
            {
                pauseOverlay.Draw(Vector2.Zero);
            }
        }
    }
}
