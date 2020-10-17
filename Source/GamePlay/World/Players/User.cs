using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class User : Player
    {
        public User(int ID) : base(ID)
        {
            hero = new Hero("2d/Hero", new Vector2(300, 300), new Vector2(48, 48), id);
        }

        public override void Update(Player ENEMY, Vector2 OFFSET)
        {
            base.Update(ENEMY, OFFSET);
        }
    }
}
