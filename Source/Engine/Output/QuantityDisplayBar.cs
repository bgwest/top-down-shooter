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
            barBackground.Draw(OFFSET, new Vector2(0, 0), Color.Black);
            // the origin set here is making the math super simple
            bar.Draw(OFFSET + new Vector2(border, border), new Vector2(0, 0), color);
        }
    }
}
