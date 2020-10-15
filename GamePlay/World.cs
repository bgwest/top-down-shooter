using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class World
    {
        public Hero hero;

        public World()
        {
            hero = new Hero("2d/Hero", new Vector2(300, 300), new Vector2(48, 48));
        }

        public virtual void Update()
        {
            hero.Update();
        }

        public virtual void Draw()
        {
            hero.Draw();
        }
    }
}
