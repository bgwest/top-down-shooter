using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class ArrowTower : Building
    {
        int range;

        McTimer shotTimer = new McTimer(1200);

        public ArrowTower(Vector2 POSITION, int OWNER_ID)
            :base("2d/Buildings/ArrowTower", POSITION, new Vector2(50, 50), OWNER_ID)
        {
            range = 220;

            health = 10;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            shotTimer.UpdateTimer();

            if (shotTimer.Test())
            {
                ShootArrow(ENEMY);
                shotTimer.ResetToZero();
            }

            base.Update(OFFSET);
        }

        // optimimzed for processor vs memory
        public virtual void ShootArrow(Player ENEMY)
        {
            float closetDistance = range, currentDistance = 0;
            Unit closest = null;

            for (int i = 0; i < ENEMY.units.Count; i++)
            {
                currentDistance = Globals.GetDistance(position, ENEMY.units[i].position);

                if (closetDistance > currentDistance)
                {
                    closetDistance = currentDistance;
                    closest = ENEMY.units[i];
                }
            }

            if (closest != null)
            {
                GameGlobals.PassProjectile(new Arrow(new Vector2(position.X, position.Y), this, new Vector2(closest.position.X, closest.position.Y)));
            }

        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
