using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class World
    {
        public Hero hero;

        public UI ui;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Vector2 worldOffset;

        public int killCount;

        public World()
        {
            hero = new Hero("2d/Hero", new Vector2(300, 300), new Vector2(48, 48));

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScroll = CheckScroll;

            worldOffset = new Vector2(0, 0);

            killCount = 0;

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(50,50), new Vector2(35,35)));

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35,35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35,35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);

            ui = new UI();
        }

        public virtual void Update()
        {
            hero.Update(worldOffset);

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(worldOffset);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(worldOffset, mobs.ToList<Unit>());

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(worldOffset, hero);

                if (mobs[i].dead)
                {
                    // in future killCount should be tightly coupled with a "death animation" 
                    killCount++;
                    mobs.RemoveAt(i);
                    i--;
                }
            }

            // important for ui to be at the bottom
            ui.Update(this);
        }

        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob)INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);
        }

        public virtual void CheckScroll(object INFO)
        {
            Vector2 tempPosition = (Vector2)INFO;

            if (tempPosition.X < -worldOffset.X + (Globals.screenWidth * .4f))
            {
                worldOffset = new Vector2(worldOffset.X + hero.speed * 2, worldOffset.Y);
            }

            if (tempPosition.X > -worldOffset.X + (Globals.screenWidth * .6f))
            {
                worldOffset = new Vector2(worldOffset.X - hero.speed * 2, worldOffset.Y);
            }

            if (tempPosition.Y < -worldOffset.Y + (Globals.screenHeight * .4f))
            {
                worldOffset = new Vector2(worldOffset.X, worldOffset.Y + hero.speed * 2);
            }

            if (tempPosition.Y > -worldOffset.Y + (Globals.screenHeight * .6f))
            {
                worldOffset = new Vector2(worldOffset.X, worldOffset.Y - hero.speed * 2);
            }
        }

        public virtual void Draw(Vector2 MAIN_OFFSET)
        {
            hero.Draw(worldOffset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(worldOffset);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(worldOffset);
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(worldOffset);
            }

            // important for ui to be at the bottom
            ui.Draw(this);
        }
    }
}
