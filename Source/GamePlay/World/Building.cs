using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class Building : AttackableObject
    {
        public Building(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Vector2 FRAMES, int OWNER_ID)
            :base(PATH, POSITION, DIMENSIONS, FRAMES, OWNER_ID)
        {
        }

        public virtual void Update(Vector2 OFFSET, Player ENEMY)
        {
            base.Update(OFFSET);
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
