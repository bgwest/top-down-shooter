using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class World
    {
        public Hero hero;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Vector2 offset;

        public World()
        {
            hero = new Hero("2d/Hero", new Vector2(300, 300), new Vector2(48, 48));

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;

            offset = new Vector2(0, 0);

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(50,50), new Vector2(35,35)));

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35,35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35,35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
        }

        public virtual void Update()
        {
            hero.Update(offset);

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(offset);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, mobs.ToList<Unit>());

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(offset, hero);

                if (mobs[i].dead)
                {
                    mobs.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob)INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            hero.Draw(OFFSET);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }

        }
    }
}
