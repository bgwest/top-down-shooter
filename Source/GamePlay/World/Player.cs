using System.Collections.Generic;
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

        public Player(int ID)
        {
            id = ID;
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

        }

        public virtual void AddUnit(object INFO)
        {
            // consider this codeblock for saftey if bugs are happening frequently.
            //Unit tempUnit = (Unit)INFO;
            //tempUnit.ownerId = id;
            //units.Add(tempUnit);

            units.Add((Unit)INFO);
        }

        public virtual void AddSpawnPoint(object INFO)
        {
            // consider this codeblock for saftey if bugs are happening frequently.
            //SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;
            //tempSpawnPoint.ownerId = id;
            //spawnPoints.Add(tempSpawnPoint);

            spawnPoints.Add((SpawnPoint)INFO);
        }

        public virtual void ChangeScore(int SCORE)
        {

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

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(OFFSET);
            }
        }
    }
}
