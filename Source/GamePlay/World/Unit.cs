using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Unit : AttackableObject
    {
        public Unit(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, OWNER_ID)
        {
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
