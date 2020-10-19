using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Tower : Building
    {
        public Tower(Vector2 POSITION, int OWNER_ID)
            :base("2d/Buildings/Tower", POSITION, new Vector2(50, 50), OWNER_ID)
        {
            health = 20;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
