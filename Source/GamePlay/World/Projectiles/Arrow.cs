using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Arrow : Projectile2d
    {

        public Arrow(Vector2 POSITION, AttackableObject OWNER, Vector2 TARGET) : base("2d/Projectiles/Arrow", POSITION, new Vector2(8, 20), OWNER, TARGET)
        {
            speed = 10.0f;

            timer = new McTimer(800);
        }

        public override void Update(Vector2 OFFSET, List<AttackableObject> ATTACKABLE_OBJECTS)
        {
            base.Update(OFFSET, ATTACKABLE_OBJECTS);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
