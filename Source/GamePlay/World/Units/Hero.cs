using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Hero : Unit
    {
        public Hero(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Vector2 FRAMES, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, FRAMES, OWNER_ID)
        {
            speed = 2.0f;

            health = 5;
            healthMax = health;

            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 4, 133, 0, "Walk"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 133, 0, "Stand"));
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

            if (Globals.keyboard.GetSinglePress("D1"))
            {
                // temporary placement right above hero -- will update in near future to be more robust
                GameGlobals.PassBuilding(new ArrowTower(new Vector2(position.X, position.Y - 30), new Vector2(1,1), ownerId));
            }

            if (checkScroll)
            {
                GameGlobals.CheckScroll(position);

                SetAnimationByName("Walk");
            }
            else
            {
                SetAnimationByName("Stand");
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
