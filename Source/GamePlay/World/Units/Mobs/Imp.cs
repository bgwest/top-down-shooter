using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Imp : Mob
    {
        public Imp(Vector2 POSITION) :base("2d/Units/Mobs/Imp", POSITION, new Vector2(40, 40))
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
