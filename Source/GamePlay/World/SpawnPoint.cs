using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class SpawnPoint : AttackableObject
    {
        public McTimer spawnTimer = new McTimer(2200);

        public SpawnPoint(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, OWNER_ID)
        {
            dead = false;

            health = 3;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            spawnTimer.UpdateTimer();

            if (spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET);
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Imp(new Vector2(position.X, position.Y), ownerId));
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
