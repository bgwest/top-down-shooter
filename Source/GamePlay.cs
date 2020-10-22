using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    // will eventually have threading and reflections at a later date
    public class GamePlay
    {
        int playState;

        World world;

        PassObject ChangeGameState;

        public GamePlay(PassObject CHANGE_GAME_STATE)
        {
            playState = 0;

            ChangeGameState = CHANGE_GAME_STATE;

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
            world = new World(ResetWorld, ChangeGameState);
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
