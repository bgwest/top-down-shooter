using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Unit : Basic2d
    {
        public float speed;

        public bool dead;

        public float hitDist;

        public Unit(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            dead = false;
            speed = 2.0f;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }

        public virtual void GetHit()
        {
            dead = true;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
