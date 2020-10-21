using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace top_down_shooter
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;

        GamePlay gamePlay;

        Basic2d cursor;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // enable to use default cursor
            //IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.screenWidth = 1600;
            Globals.screenHeight = 900;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            cursor = new Basic2d("2d/misc/CursorArrow", new Vector2(0, 0), new Vector2(28, 28));

            Globals.normalEffect = Globals.content.Load<Effect>("2d/Effects/NormalFlat");

            Globals.keyboard = new McKeyboard();
            Globals.mouse = new McMouseControl();

            gamePlay = new GamePlay();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            gamePlay.Update();

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // if your not running a shader program, SpriteSortMode.Deferred is pretty much the best
            // drawing all sprites immediately is slightly less efficient, but it allows us to run our anti-aliasing
            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            gamePlay.Draw();


            Globals.normalEffect.Parameters["xSize"].SetValue((float)cursor.myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)cursor.myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)cursor.dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)cursor.dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            cursor.Draw(new Vector2 (Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public static class Program
    {
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
}
