using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class User : Player
    {
        public User(int ID, XElement DATA) : base(ID, DATA)
        {
            //hero = new Hero("2d/Hero", new Vector2(300, 300), new Vector2(48, 48), id);

            //buildings.Add(new Tower(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 40), id));
        }

        public override void Update(Player ENEMY, Vector2 OFFSET)
        {
            base.Update(ENEMY, OFFSET);
        }
    }
}
