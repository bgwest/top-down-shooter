using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class AttackableObject : Basic2d
    {
        public bool dead;

        public int ownerId;

        public float speed, hitDist, health, healthMax;

        public AttackableObject(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS)
        {
            ownerId = OWNER_ID;
            dead = false;
            speed = 2.0f;

            health = 1;
            healthMax = health;

            hitDist = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET);
        }

        public virtual void GetHit(float DAMAGE)
        {
            health -= DAMAGE;

            if (health <= 0)
            {
              dead = true;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
