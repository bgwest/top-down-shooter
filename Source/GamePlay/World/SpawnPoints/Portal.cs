using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Portal : SpawnPoint
    {

        public Portal(Vector2 POSITION, Vector2 FRAMES, int OWNER_ID, XElement DATA)
            :base("2d/SpawnPoints/Portal", POSITION, new Vector2(45,45), FRAMES, OWNER_ID, DATA)
        {
            health = 15;
            healthMax = health;
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }

        public override void SpawnMob()
        {
            int num = Globals.random.Next(0, 100 + 1);

            int total = 0;

            Mob tempMob = null;

            for (int i = 0; i < mobChoices.Count; i++)
            {
                total += mobChoices[i].rate;

                if (num < total)
                {
                    Type sType = Type.GetType("top_down_shooter." + mobChoices[i].mobString, true);

                    // Note: The activator classes do not find changes in variable/params... just a helpful reminder when building!
                    tempMob = (Mob)(Activator.CreateInstance(sType, new Vector2(position.X, position.Y), new Vector2(1, 1), ownerId));

                    break;
                }

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
