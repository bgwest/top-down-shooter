using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class QuantityDisplayBar
    {
        public int border;

        public Basic2d bar, barBackground;

        public Color color;

        public QuantityDisplayBar(Vector2 DIMENSIONS, int BORDER, Color COLOR)
        {
            border = BORDER;
            color = COLOR;

            bar = new Basic2d("2d/misc/solid", new Vector2(0, 0), new Vector2(DIMENSIONS.X - border * 2, DIMENSIONS.Y - border * 2));
            barBackground = new Basic2d("2d/misc/shade", new Vector2(0, 0), new Vector2(DIMENSIONS.X, DIMENSIONS.Y));
        }

        public virtual void Update(float CURRENT, float MAX)
        {
            bar.dimensions = new Vector2(CURRENT / MAX * (barBackground.dimensions.X - border * 2 ), bar.dimensions.Y);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            // since text needs to be pixel for pixel we override the shader loop and say "don't skip any pixels" basically
            Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Black.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            barBackground.Draw(OFFSET, new Vector2(0, 0), Color.Black);

            Globals.normalEffect.Parameters["filterColor"].SetValue(color.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            // the origin set here is making the math super simple
            bar.Draw(OFFSET + new Vector2(border, border), new Vector2(0, 0), color);
        }
    }
}
