using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Hero : Unit
    {
        public Hero(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, OWNER_ID)
        {
            speed = 2.0f;

            health = 5;
            healthMax = health;
        }

        public override void Update(Vector2 OFFSET)
        {
            // a camera would be a better option but also more complicated
            // might consider using monogame extendends camera or using this for now is fine
            bool checkScroll = false;

            // MUST USE IF/ELSE if you want to prevent diagomal movement
            if (Globals.keyboard.GetPress("A"))
            {
                position = new Vector2(position.X - speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("D"))
            {
                position = new Vector2(position.X + speed, position.Y);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("W"))
            {
                position = new Vector2(position.X, position.Y - speed);
                checkScroll = true;
            }

            if (Globals.keyboard.GetPress("S"))
            {
                position = new Vector2(position.X, position.Y + speed);
                checkScroll = true;
            }

            if (checkScroll)
            {
                GameGlobals.CheckScroll(position);
            }

            // rotate hero towards the mouse
            rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET);

            if (Globals.mouse.LeftClick())
            {
                // creating a new Vector2 ensures are updating a new piece of memory and not the same as our heros
                GameGlobals.PassProjectile(new Fireball(new Vector2(position.X, position.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
            }

            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
