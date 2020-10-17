using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace top_down_shooter
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;

        World world;

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
            Globals.screenWidth = 800; //1600
            Globals.screenHeight = 500; //900

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

            Globals.keyboard = new McKeyboard();
            Globals.mouse = new McMouseControl();

            world = new World();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            world.Update();

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);

            Globals.spriteBatch.Begin();

            world.Draw(Vector2.Zero);

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
