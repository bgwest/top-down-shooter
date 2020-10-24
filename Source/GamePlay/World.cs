using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class World
    {
        public UI ui;

        public User user;
        public AIPlayer aiPlayer;

        public SquareGrid grid;

        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<AttackableObject> allObjects = new List<AttackableObject>();

        public Vector2 worldOffset;

        PassObject PassResetWorld, ChangeGameState;

        public World(PassObject RESET_WORLD, PassObject CHANGE_GAME_STATE)
        {
            PassResetWorld = RESET_WORLD;
            ChangeGameState = CHANGE_GAME_STATE;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.PassBuilding = AddBuilding;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;
            GameGlobals.CheckScroll = CheckScroll;

            GameGlobals.paused = false;

            worldOffset = new Vector2(0, 0);

            LoadData(1);

            grid = new SquareGrid(new Vector2(25,25), new Vector2(-100, -100), new Vector2(Globals.screenWidth + 200, Globals.screenHeight + 200));

            ui = new UI(PassResetWorld);
        }

        public virtual void Update()
        {

            if (!user.hero.dead && user.buildings.Count > 0 && !GameGlobals.paused)
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

            if (grid != null)
            {
                grid.Update(worldOffset);
            }

            if (Globals.keyboard.GetSinglePress("Back"))
            {
                PassResetWorld(null);
                ChangeGameState((int)GameStates.MainMenu);
            }

            if (Globals.keyboard.GetSinglePress("Space"))
            {
                GameGlobals.paused = !GameGlobals.paused;
            }

            if (Globals.keyboard.GetSinglePress("G"))
            {
                grid.showGrid = !grid.showGrid;
            }

            // important for ui to be at the bottom
            ui.Update(this);
        }

        public virtual void AddBuilding(object INFO)
        {
            Building tempBuilding = (Building)INFO;

            if (user.id == tempBuilding.ownerId)
            {
                user.AddBuilding(tempBuilding);
            }
            else if (aiPlayer.id == tempBuilding.ownerId)
            {
                aiPlayer.AddBuilding(tempBuilding);
            }
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

        public virtual void LoadData(int LEVEL)
        {
            XDocument xml = XDocument.Load("XML/Levels/Level"+LEVEL+".xml");

            XElement tempElement = null;

            if (xml.Element("Root").Element("User") != null)
            {
                tempElement = xml.Element("Root").Element("User");
            }

            user = new User(1, tempElement);

            tempElement = null;

            if (xml.Element("Root").Element("AIPlayer") != null)
            {
                tempElement = xml.Element("Root").Element("AIPlayer");
            }

            aiPlayer = new AIPlayer(2, tempElement);
        }

        public virtual void Draw(Vector2 MAIN_OFFSET)
        {
            grid.DrawGrid(worldOffset);

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
