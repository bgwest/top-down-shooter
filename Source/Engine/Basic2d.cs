using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace top_down_shooter
{
    public class Basic2d
    {
        public float rotation;

        // frameSize is import for zooming... eventually
        public Vector2 position, dimensions, frameSize;

        public Texture2D myModel;



        public Basic2d(string PATH, Vector2 POSITION, Vector2 DIMENSIONS)
        {
            position = new Vector2(POSITION.X, POSITION.Y);
            dimensions = new Vector2(DIMENSIONS.X, DIMENSIONS.Y);
            rotation = 0.0F;

            myModel = Globals.content.Load<Texture2D>(PATH);
        }

        public virtual void Update(Vector2 OFFSET)
        {

        }

        public virtual bool Hover(Vector2 OFFSET)
        {
            return HoverImage(OFFSET);
        }

        public virtual bool HoverImage(Vector2 OFFSET)
        {
            Vector2 mousePosition = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

            // TODO: move these intro seperate variables for readability 
            if (mousePosition.X >= (position.X + OFFSET.X) - dimensions.X / 2 && mousePosition.X <= (position.X + OFFSET.X) + dimensions.X / 2 && mousePosition.Y >= (position.Y + OFFSET.Y) - dimensions.Y / 2 && mousePosition.Y <= (position.Y + OFFSET.Y) + dimensions.Y / 2)
            {
                return true;
            }

            return false;
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(position.X + OFFSET.X), (int)(position.Y + OFFSET.Y), (int)(dimensions.X), (int)(dimensions.Y)), null, Color.White, rotation, new Vector2(myModel.Bounds.Width/2, myModel.Bounds.Height/2), new SpriteEffects(), 0);
            }
        }

        // ORIGIN is nice for when you have to draw things outside of the middle
        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(position.X + OFFSET.X), (int)(position.Y + OFFSET.Y), (int)(dimensions.X), (int)(dimensions.Y)), null, COLOR, rotation, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }
    }
}
