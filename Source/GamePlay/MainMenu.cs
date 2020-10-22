using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class MainMenu
    {
        public Basic2d background;

        public PassObject PlayClickDelete, ExitClickDelete;

        public List<Button2d> buttons = new List<Button2d>();

        public MainMenu(PassObject PLAY_CLICK_DELETE, PassObject EXIT_CLICK_DELETE)
        {
            PlayClickDelete = PLAY_CLICK_DELETE;
            ExitClickDelete = EXIT_CLICK_DELETE;

            background = new Basic2d("2d/UI/Backgrounds/MainMenuBkg", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 ), new Vector2(Globals.screenWidth, Globals.screenHeight));

            buttons.Add(new Button2d("2d/misc/SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "2d/Fonts/Arial16", "Play", PlayClickDelete, 1)); ;

            buttons.Add(new Button2d("2d/misc/SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "2d/Fonts/Arial16", "Exit", ExitClickDelete, null)); ;
        }

        public virtual void Update()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(325, 600 + 45 * i));
            }
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(325, 600 + 45 * i));
            }
        }
    }
}
