using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Unit : Basic2d
    {
        public float speed;

        public Unit(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            speed = 2.0f;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
