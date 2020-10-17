using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Portal : SpawnPoint
    {

        public Portal(Vector2 POSITION, int OWNER_ID)
            :base("2d/SpawnPoints/Portal", POSITION, new Vector2(45,45), OWNER_ID)
        {
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }

        public override void SpawnMob()
        {
            int num = Globals.random.Next(0, 10 + 1);

            Mob tempMob = null;

            if (num < 5)
            {
                tempMob = new Imp(new Vector2(position.X, position.Y), ownerId);
            } else if (num < 8)
            {
                tempMob = new Spider(new Vector2(position.X, position.Y), ownerId);
            }

            if (tempMob != null)
            {
              GameGlobals.PassMob(tempMob);
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
