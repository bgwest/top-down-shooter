using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class SpiderEggSac : SpawnPoint
    {
        int maxSpawns, totalSpawns;

        public SpiderEggSac(Vector2 POSITION, int OWNER_ID)
            :base("2d/SpawnPoints/EggSac", POSITION, new Vector2(25,25), OWNER_ID)
        {
            totalSpawns = 0;
            maxSpawns = 3;
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }

        public override void SpawnMob()
        {
            Mob tempMob = new Spiderling(new Vector2(position.X, position.Y), ownerId);

            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);

                totalSpawns++;

                if(totalSpawns >= maxSpawns)
                {
                    dead = true;
                }
            }

            GameGlobals.PassMob(tempMob);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
