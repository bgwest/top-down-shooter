using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Spider : Mob
    {
        public McTimer spawnTimer;

        public Spider(Vector2 POSITION, int OWNER_ID)
            :base("2d/Units/Mobs/Spider", POSITION, new Vector2(45, 45), OWNER_ID)
        {
            speed = 1.5f;

            health = 3;
            healthMax = health;

            spawnTimer = new McTimer(8000);
            // 4 seconds to spawn the first egg and 8 seconds to spawn the next
            spawnTimer.AddToTimer(4000);
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            spawnTimer.UpdateTimer();

            if (spawnTimer.Test())
            {
                SpawnEggSac();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET, ENEMY);
        }

        public virtual void SpawnEggSac()
        {
            GameGlobals.PassSpawnPoint(new SpiderEggSac(new Vector2(position.X, position.Y), ownerId, null));
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
