using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Hero : Basic2d
    {
        public float speed;

        public Hero(string PATH, Vector2 POSITION, Vector2 DIMENSIONS) :base(PATH, POSITION, DIMENSIONS)
        {
            speed = 2.0f;
        }

        public override void Update()
        {
            // MUST USE IF/ELSE if you want to prevent diagomal movement
            if (Globals.keyboard.GetPress("X"))
            {
                position = new Vector2(position.X - speed, position.Y);
            }

            if (Globals.keyboard.GetPress("V"))
            {
                position = new Vector2(position.X + speed, position.Y);
            }

            if (Globals.keyboard.GetPress("D"))
            {
                position = new Vector2(position.X, position.Y - speed);
            }

            if (Globals.keyboard.GetPress("C"))
            {
                position = new Vector2(position.X, position.Y + speed);
            }

            // rotate hero towards the mouse
            rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));

            base.Update();
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
