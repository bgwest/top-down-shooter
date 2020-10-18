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

            timer = new McTimer(1500);
        }

        public virtual void Update(Vector2 OFFSET, List<AttackableObject> ATTACKABLE_OBJECTS)
        {
            position += direction * speed;

            timer.UpdateTimer();

            if (timer.Test())
            {
                done = true;
            }

            if (HitSomething(ATTACKABLE_OBJECTS))
            {
                done = true;
            }
        }

        public virtual bool HitSomething(List<AttackableObject> ATTACKABLE_OBJECTS)
        {
            for (int i = 0; i < ATTACKABLE_OBJECTS.Count; i++)
            {
                // if this were a "ball of healing" could change to == to only heal things that are on your team!
                if (owner.ownerId != ATTACKABLE_OBJECTS[i].ownerId && Globals.GetDistance(position, ATTACKABLE_OBJECTS[i].position) < ATTACKABLE_OBJECTS[i].hitDist)
                {
                    // 1 damage is good enough for now
                    ATTACKABLE_OBJECTS[i].GetHit(1);
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
