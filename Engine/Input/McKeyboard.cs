using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace top_down_shooter
{
    public class McKeyboard
    {

        public KeyboardState newKeyboard, oldKeyboard;

        public List<McKey> pressedKeys = new List<McKey>(), previousPressedKeys = new List<McKey>();

        public McKeyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();

            GetPressedKeys();

        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<McKey>();
            for(int i=0;i<pressedKeys.Count;i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }


        public bool GetPress(string KEY)
        {

            for(int i=0;i<pressedKeys.Count;i++)
            {

                if(pressedKeys[i].key == KEY)
                {
                    return true;
                }

            }
            

            return false;
        }


        public virtual void GetPressedKeys()
        {
            bool found = false;

            pressedKeys.Clear();
            for(int i=0; i<newKeyboard.GetPressedKeys().Length; i++)
            {

                    pressedKeys.Add(new McKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
  
            }
        }

    }
}
