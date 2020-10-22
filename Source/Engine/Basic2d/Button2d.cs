using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace top_down_shooter
{
    public class Button2d : Basic2d
    {
        public bool isPressed, isHovered;
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public object info;

        PassObject ButtonClicked;

        public Button2d(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, string FONTPATH, string TEXT, PassObject BUTTON_CLICKED, object INFO)
            :base(PATH, POSITION, DIMENSIONS)
        {
            info = INFO;
            text = TEXT;
            ButtonClicked = BUTTON_CLICKED;

            if (FONTPATH != "")
            {
                font = Globals.content.Load<SpriteFont>(FONTPATH);
            }

            isPressed = false;
            hoverColor = new Color(200, 230, 255);
        }

        public override  void Update(Vector2 OFFSET)
        {
            if (Hover(OFFSET))
            {
                isHovered = true;

                if (Globals.mouse.LeftClick())
                {
                    isHovered = false;
                    isPressed = true;
                }
                else if (Globals.mouse.LeftClickRelease())
                {
                    RunButtonClick();
                }
            }
            else
            {
                isHovered = false;
            }

            if (!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
            {
                isPressed = false;
            }

            base.Update(OFFSET);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunButtonClick()
        {
            ButtonClicked?.Invoke(info);

            Reset();
        }

        public override void Draw(Vector2 OFFSET)
        {
            Color tempColor = Color.White;
            Color textColor = Color.Gray;

            if (isPressed)
            {
                tempColor = Color.Gray;
            }
            else if (isHovered)
            {
                tempColor = hoverColor;
                textColor = hoverColor;
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);

            Vector2 stringDimensions = font.MeasureString(text);
            Globals.spriteBatch.DrawString(font, text, position + OFFSET + new Vector2(-stringDimensions.X / 2, -stringDimensions.Y / 2), Color.Black);
        }
    }
}
