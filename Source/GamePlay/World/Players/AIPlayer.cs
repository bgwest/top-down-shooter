using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class AIPlayer : Player
    {
        public AIPlayer(int ID) : base(ID)
        {
            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(50, 50), new Vector2(35, 35), id));

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35, 35), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new SpawnPoint("2d/misc/circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
        }

        public override void Update(Player ENEMY, Vector2 OFFSET)
        {
            base.Update(ENEMY, OFFSET);
        }

        public override void ChangeScore(int SCORE)
        {
            GameGlobals.score += SCORE;
        }
    }
}
