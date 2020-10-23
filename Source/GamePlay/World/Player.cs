using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Player
    {
        public int id;
        // later will probably end up making this a List
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
        public List<Building> buildings = new List<Building>();

        public Player(int ID, XElement DATA)
        {
            id = ID;

            LoadData(DATA);
        }

        public virtual void Update(Player ENEMY, Vector2 OFFSET)
        {
            if (hero != null)
            {
              hero.Update(OFFSET);
            }

            // could be used as a skill to aid the player like "spawn box with mechs"
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(OFFSET);

                if (spawnPoints[i].dead)
                {
                    spawnPoints.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Update(OFFSET, ENEMY);

                if (units[i].dead)
                {
                    // in future killCount should be tightly coupled with a "death animation" 
                    ChangeScore(1);
                    units.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Update(OFFSET, ENEMY);

                if (buildings[i].dead)
                {
                    buildings.RemoveAt(i);
                    i--;
                }
            }

        }

        public virtual void AddBuilding(object INFO)
        {
            buildings.Add((Building)INFO);
        }

        public virtual void AddSpawnPoint(object INFO)
        {
            // consider this codeblock for saftey if bugs are happening frequently.
            //SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;
            //tempSpawnPoint.ownerId = id;
            //spawnPoints.Add(tempSpawnPoint);

            spawnPoints.Add((SpawnPoint)INFO);
        }

        public virtual void AddUnit(object INFO)
        {
            // consider this codeblock for saftey if bugs are happening frequently.
            //Unit tempUnit = (Unit)INFO;
            //tempUnit.ownerId = id;
            //units.Add(tempUnit);

            units.Add((Unit)INFO);
        }

        public virtual void ChangeScore(int SCORE)
        {

        }

        public virtual List<AttackableObject> GetAllAttackableObjects() {
            // if we start using this to be called more than once per frame, consider moing this up higher
            List<AttackableObject> tempObjects = new List<AttackableObject>();

            tempObjects.AddRange(units.ToList<AttackableObject>());
            tempObjects.AddRange(spawnPoints.ToList<AttackableObject>());
            tempObjects.AddRange(buildings.ToList<AttackableObject>());

            return tempObjects;
        }

        public virtual void LoadData(XElement DATA)
        {
            List<XElement> spawnList = (from typeMatch in DATA.Descendants("SpawnPoint") select typeMatch).ToList<XElement>();

            Type sType = null;

            for (int i = 0; i < spawnList.Count; i++)
            {
                sType = Type.GetType("top_down_shooter."+ spawnList[i].Element("type").Value, true);

                // Hard coded method: less expensive but less flexible
                //spawnPoints.Add(new Portal(new Vector2(Convert.ToInt32(spawnList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(spawnList[i].Element("Pos").Element("y").Value, Globals.culture)), id));

                // Dynamic method: much more flexible but -can be- 2 to 3x more expensive if used poorly
                // it is recommened to do it this way almost always, but may need to hardcode if use case is causing bad lag
                spawnPoints.Add((SpawnPoint)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(spawnList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(spawnList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1, 1), id, spawnList[i])));
            }

            List<XElement> buildingList = (from typeMatch in DATA.Descendants("Building") select typeMatch).ToList<XElement>();

            for (int i = 0; i < buildingList.Count; i++)
            {
                sType = Type.GetType("top_down_shooter." + buildingList[i].Element("type").Value, true);

                // hard code method
                //buildings.Add(new Tower(new Vector2(Convert.ToInt32(buildingList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(buildingList[i].Element("Pos").Element("y").Value, Globals.culture)), id));

                // dnyamic method
                buildings.Add((Building)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(buildingList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(buildingList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1,1), id)));

            }

            if (DATA.Element("Hero") != null)
            {
                hero = new Hero("2d/Units/Hero/HeroSheet", new Vector2(Convert.ToInt32(DATA.Element("Hero").Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(DATA.Element("Hero").Element("Pos").Element("y").Value, Globals.culture)), new Vector2(64, 64), new Vector2(4, 1), id);
            }
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if (hero != null)
            {
                hero.Draw(OFFSET);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Draw(OFFSET);
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Draw(OFFSET);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(OFFSET);
            }
        }
    }
}
