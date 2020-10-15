using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace top_down_shooter
{
    public class Basic2d
    {
        public Vector2 position, dimensions;

        public Texture2D myModel;



        public Basic2d(string PATH, Vector2 POSITION, Vector2 DIMENSIONS)
        {
            position = POSITION;
            dimensions = DIMENSIONS;

            myModel = Globals.content.Load<Texture2D>(PATH);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(position.X), (int)(position.Y), (int)(dimensions.X), (int)(dimensions.Y)), null, Color.White, 0.0f, new Vector2(myModel.Bounds.Width/2, myModel.Bounds.Height/2), new SpriteEffects(), 0);
            }
        }
    }
}
