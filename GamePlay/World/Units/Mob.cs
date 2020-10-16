using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Mob : Unit
    {
        public Mob(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            AI();

            base.Update(OFFSET);
        }

        public virtual void AI()
        {

        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
