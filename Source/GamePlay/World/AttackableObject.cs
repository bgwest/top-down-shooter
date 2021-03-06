﻿using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class AttackableObject : Animated2d
    {
        public bool dead;

        public int ownerId;

        public float speed, hitDist, health, healthMax;

        public AttackableObject(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Vector2 FRAMES, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, FRAMES, Color.White)
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

            Globals.normalEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);
        }
    }
}
