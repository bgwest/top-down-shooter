using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Spiderling : Mob
    {
        public McTimer spawnTimer;

        public Spiderling(Vector2 POSITION, int OWNER_ID)
            :base("2d/Units/Mobs/Spider", POSITION, new Vector2(25, 25), OWNER_ID)
        {
            speed = 1.0f;
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
