using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Projectile2d : Basic2d
    {
        public bool done;

        public float speed;

        public Vector2 direction;

        public Unit owner;

        public McTimer timer;

        public Projectile2d(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Unit OWNER, Vector2 TARGET) : base(PATH, POSITION, DIMENSIONS)
        {
            done = false;

            speed = 5.0f;

            owner = OWNER;

            direction = TARGET - owner.position;
            direction.Normalize();

            rotation = Globals.RotateTowards(position, new Vector2(TARGET.X, TARGET.Y));

            timer = new McTimer(1200);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            position += direction * speed;

            timer.UpdateTimer();

            if (timer.Test())
            {
                done = true;
            }

            if (HitSomething(UNITS))
            {
                done = true;
            }
        }

        public virtual bool HitSomething(List<Unit> UNITS)
        {
            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
