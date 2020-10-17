using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Mob : Unit
    {
        public Mob(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            speed = 2.0f;
        }

        public override void Update(Vector2 OFFSET, Player ENEMY)
        {
            AI(ENEMY.hero);

            base.Update(OFFSET);
        }

        public virtual void AI(Hero HERO)
        {
            position += Globals.RadialMovement(HERO.position, position, speed);
            rotation = Globals.RotateTowards(position, HERO.position);

            // bounding boxes will come later, good enough for now
            if (Globals.GetDistance(position, HERO.position) < 15)
            {
                HERO.GetHit(1);
                dead = true;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
