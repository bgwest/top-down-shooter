using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Fireball : Projectile2d
    {

        public Fireball(Vector2 POSITION, Unit OWNER, Vector2 TARGET) : base("2d/Projectiles/Fireball", POSITION, new Vector2(20, 20), OWNER, TARGET)
        {

        }

        public override void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            base.Update(OFFSET, UNITS);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
