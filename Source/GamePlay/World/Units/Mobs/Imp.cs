using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Imp : Mob
    {
        public Imp(Vector2 POSITION, Vector2 FRAMES, int OWNER_ID)
            :base("2d/Units/Mobs/Imp", POSITION, new Vector2(40, 40), FRAMES, OWNER_ID)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET, ENEMY);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
