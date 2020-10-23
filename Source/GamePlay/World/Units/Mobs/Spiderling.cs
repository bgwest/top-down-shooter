using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Spiderling : Mob
    {
        public McTimer spawnTimer;

        public Spiderling(Vector2 POSITION, Vector2 FRAMES, int OWNER_ID)
            :base("2d/Units/Mobs/Spider", POSITION, new Vector2(25, 25), FRAMES, OWNER_ID)
        {
            speed = 1.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET, ENEMY);
        }

        public override void AI(Player ENEMY)
        {
            Building temp = null;
            for (int i = 0; i < ENEMY.buildings.Count; i++)
            {
                // tower will eventually be probably loaded from a file, but for now this is fine
                if (ENEMY.buildings[i].GetType().ToString() == "top_down_shooter.Tower")
                {
                    temp = ENEMY.buildings[i];
                }
            }

            if (temp != null)
            {
                position += Globals.RadialMovement(temp.position, position, speed);
                rotation = Globals.RotateTowards(position, temp.position);

                // bounding boxes will come later, good enough for now
                if (Globals.GetDistance(position, temp.position) < 15)
                {
                    temp.GetHit(1);
                    dead = true;
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
