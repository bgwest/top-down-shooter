using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class SpawnPoint : AttackableObject
    {
        public List<MobChoice> mobChoices = new List<MobChoice>();

        public McTimer spawnTimer = new McTimer(2400);

        public SpawnPoint(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, int OWNER_ID, XElement DATA)
            :base(PATH, POSITION, DIMENSIONS, OWNER_ID)
        {
            dead = false;

            health = 3;
            healthMax = health;

            LoadData(DATA);

            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            spawnTimer.UpdateTimer();

            if (spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET);
        }

        public virtual void LoadData(XElement DATA)
        {
            if (DATA != null)
            {
                spawnTimer.AddToTimer(Convert.ToInt32(DATA.Element("timerAdd").Value, Globals.culture));

                List<XElement> mobList = (from typeMatch in DATA.Descendants("mob") select typeMatch).ToList<XElement>();

                for (int i = 0; i < mobList.Count; i++)
                {
                    mobChoices.Add(new MobChoice(mobList[i].Value, Convert.ToInt32(mobList[i].Attribute("rate").Value, Globals.culture)));
                }
            }
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Imp(new Vector2(position.X, position.Y), ownerId));
        }

        public override void Draw(Vector2 OFFSET)
        {
            // TODO: Shader needs to be re-processed in order to run on this version of monogame
            //       uncomment and try running after-reprocessing
            //Globals.normalEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            //Globals.normalEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            //Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dimensions.X));
            //Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dimensions.Y));
            //Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            //Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);
        }
    }
}
