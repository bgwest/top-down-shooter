using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Mob : Unit
    {
        public Mob(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Vector2 FRAMES, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, FRAMES, OWNER_ID)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            AI(ENEMY);

            base.Update(OFFSET);
        }

        public virtual void AI(Player ENEMY)
        {
            position += Globals.RadialMovement(ENEMY.hero.position, position, speed);
            rotation = Globals.RotateTowards(position, ENEMY.hero.position);

            // bounding boxes will come later, good enough for now
            if (Globals.GetDistance(position, ENEMY.hero.position) < 15)
            {
                ENEMY.hero.GetHit(1);
                dead = true;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
