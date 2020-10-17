using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    // will eventually have threading and reflections at a later date
    public class GamePlay
    {
        int playState;

        World world;

        public GamePlay()
        {
            playState = 0;

            ResetWorld(null);
        }

        public virtual void Update()
        {
            if (playState == 0)
            {
                world.Update();
            }
        }

        public virtual void ResetWorld(object INFO)
        {
            world = new World(ResetWorld);
        }

        public virtual void Draw()
        {
            if (playState == 0)
            {
                world.Draw(Vector2.Zero);
            }
        }
    }
}
