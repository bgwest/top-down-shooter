using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class World
    {
        public UI ui;

        public User user;
        public AIPlayer aiPlayer;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<AttackableObject> allObjects = new List<AttackableObject>();

        public Vector2 worldOffset;

        PassObject PassResetWorld;

        public World(PassObject RESET_WORLD)
        {
            PassResetWorld = RESET_WORLD;

            user = new User(1);
            aiPlayer = new AIPlayer(2);

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;
            GameGlobals.CheckScroll = CheckScroll;

            worldOffset = new Vector2(0, 0);

            ui = new UI();
        }

        public virtual void Update()
        {

            if (!user.hero.dead && user.buildings.Count > 0)
            {
                // this is not the most efficient way to do this, this is just a dirt way
                // to get things going
                // will be optomized as the needs arise/change
                allObjects.Clear();
                allObjects.AddRange(user.GetAllAttackableObjects());
                allObjects.AddRange(aiPlayer.GetAllAttackableObjects());

                user.Update(aiPlayer, worldOffset);
                aiPlayer.Update(user, worldOffset);

                // currently we won't know what set of units to pass in
                // in near future will be testing in projectile for this
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(worldOffset, allObjects);

                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                // eventually this can just be a bool like "lost"
                if (Globals.keyboard.GetPress("Enter") && (user.hero.dead || user.buildings.Count <= 0))
                {
                    PassResetWorld(null);
                }
            }

            // important for ui to be at the bottom
            ui.Update(this);
        }

        public virtual void AddMob(object INFO)
        {
            Unit tempUnit = (Unit)INFO;

            // this system would be atrocious if we had MANY players
            // but since this will likely be 1-2 player game, this is fine
            if (user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if (aiPlayer.id == tempUnit.ownerId)
            {
                aiPlayer.AddUnit(tempUnit);
            }

            aiPlayer.AddUnit((Mob)INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d)INFO);
        }

        public virtual void AddSpawnPoint(object INFO)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;

            // this system would be atrocious if we had MANY players
            // but since this will likely be 1-2 player game, this is fine
            if (user.id == tempSpawnPoint.ownerId)
            {
                user.AddSpawnPoint(tempSpawnPoint);
            }
            else if (aiPlayer.id == tempSpawnPoint.ownerId)
            {
                aiPlayer.AddSpawnPoint(tempSpawnPoint);
            }
        }

        public virtual void CheckScroll(object INFO)
        {
            Vector2 tempPosition = (Vector2)INFO;

            if (tempPosition.X < -worldOffset.X + (Globals.screenWidth * .4f))
            {
                worldOffset = new Vector2(worldOffset.X + user.hero.speed * 2, worldOffset.Y);
            }

            if (tempPosition.X > -worldOffset.X + (Globals.screenWidth * .6f))
            {
                worldOffset = new Vector2(worldOffset.X - user.hero.speed * 2, worldOffset.Y);
            }

            if (tempPosition.Y < -worldOffset.Y + (Globals.screenHeight * .4f))
            {
                worldOffset = new Vector2(worldOffset.X, worldOffset.Y + user.hero.speed * 2);
            }

            if (tempPosition.Y > -worldOffset.Y + (Globals.screenHeight * .6f))
            {
                worldOffset = new Vector2(worldOffset.X, worldOffset.Y - user.hero.speed * 2);
            }
        }

        public virtual void Draw(Vector2 MAIN_OFFSET)
        {
            user.Draw(worldOffset);
            aiPlayer.Draw(worldOffset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(worldOffset);
            }

            // important for ui to be at the bottom
            ui.Draw(this);
        }
    }
}
