using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Mob : Unit
    {
        public Mob(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            speed = 2.0f;
        }

        public virtual void Update(Vector2 OFFSET, Hero HERO)
        {
            AI(HERO);

            base.Update(OFFSET);
        }

        public virtual void AI(Hero HERO)
        {
            position += Globals.RadialMovement(HERO.position, position, speed);
            rotation = Globals.RotateTowards(position, HERO.position);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
