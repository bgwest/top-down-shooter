using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class SquareGrid
    {

        public bool showGrid;

        //  slotDims = how big each slot is
        //  gridDims = more helpful with arrays, but essentially it's the counts of each of the slots
        //  physicalStartPos = top left of the grid - could also call it "gridOffset"
        //  totalPhysicalDims = the total length & width of the grid
        public Vector2 slotDims, gridDims, physicalStartPos, totalPhysicalDims, currentHoverSlot;

        public Basic2d gridImg;


        // If the game starts lagging due to List processing you need to switch to Arrays
        // Lists are easier to use and more "forgiving" but at a higher cost
        // Arrays are much more efficient, but it's easier to make bugs with Arrays and harder to fix them
        // will save you 30% processing time, maybe more
        public List<List<GridLocation>> slots = new List<List<GridLocation>>();


        public SquareGrid(Vector2 SLOTDIMS, Vector2 STARTPOS, Vector2 TOTALDIMS)
        {
            showGrid = false;

            slotDims = SLOTDIMS;

            // rare instances where decimal numbers will cause issues
            // the int concatenation is a safety measure to prevent that
            physicalStartPos = new Vector2((int)STARTPOS.X, (int)STARTPOS.Y);
            totalPhysicalDims = new Vector2((int)TOTALDIMS.X, (int)TOTALDIMS.Y);

            currentHoverSlot = new Vector2(-1, -1);

            SetBaseGrid();

            // param2 note:
            // slotDims/2 because our slots location begin in the top left corner of the slot
            // this places it directly in the center
            // param3 note:
            // the -2's are because without the -2, the lines will all blur together at the point
            // in which the touch
            // the -2 gives it the spacing it needs the slots to be "grid like"
            gridImg = new Basic2d("2d/misc/shade", slotDims/2, new Vector2(slotDims.X-2, slotDims.Y-2));
        }

        public virtual void Update(Vector2 OFFSET)
        {
            // this is currently being processed every frame, but keep in mind that it can be slowed down to every 3rd frame etc.
            // to help save on processing cost
            // also, the reason why OFFSET should be negative, is the grid works in the opposite direction that the OFFSET moves
            currentHoverSlot = GetSlotFromPixel(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), -OFFSET);
        }


        public virtual GridLocation GetSlotFromLocation(Vector2 LOC)
        {
            if(LOC.X >= 0 && LOC.Y >= 0 && LOC.X < slots.Count && LOC.Y < slots[(int)LOC.X].Count)
            {
                return slots[(int)LOC.X][(int)LOC.Y];
            }

            return null;
        }

        public virtual Vector2 GetSlotFromPixel(Vector2 PIXEL, Vector2 OFFSET)
        {
            Vector2 adjustedPos = PIXEL - physicalStartPos + OFFSET;

            // max is 0 if negative presents
            Vector2 tempVec = new Vector2(Math.Min(Math.Max (0, (int)(adjustedPos.X/slotDims.X)), slots.Count-1), Math.Min(Math.Max(0, (int)(adjustedPos.Y/slotDims.Y)), slots[0].Count-1));

            return tempVec;
        }

        public virtual void SetBaseGrid()
        {
            gridDims = new Vector2((int)(totalPhysicalDims.X/slotDims.X), (int)(totalPhysicalDims.Y/slotDims.Y));

            slots.Clear();
            for(int i=0; i<gridDims.X; i++)
            {
                slots.Add(new List<GridLocation>());

                for(int j=0; j<gridDims.Y; j++)
                {
                    slots[i].Add(new GridLocation(1, false));
                }
            }
        }


        public virtual void DrawGrid(Vector2 OFFSET)
        {
            if(showGrid)
            {
                //Vector2 topLeft = GetSlotFromPixel((new Vector2(0, 0)) / Globals.zoom  - OFFSET, Vector2.Zero);
                //Vector2 bottomRight = GetSlotFromPixel((new Vector2(Globals.screenWidth, Globals.screenHeight)) / Globals.zoom  - OFFSET, Vector2.Zero);
                Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0), Vector2.Zero);
                Vector2 bottomRight = GetSlotFromPixel(new Vector2(Globals.screenWidth, Globals.screenHeight), Vector2.Zero);

                Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

                // only draws what is currently on screen! If you try to draw everything, your computer likely would break ;)
                for(int j=(int)topLeft.X;j<=bottomRight.X && j<slots.Count;j++)
                {
                    for(int k=(int)topLeft.Y;k<=bottomRight.Y && k<slots[0].Count;k++)
                    {
                        if(currentHoverSlot.X == j && currentHoverSlot.Y == k)
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

                        }
                        else
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
                        }

                        gridImg.Draw(OFFSET + physicalStartPos + new Vector2(j * slotDims.X, k * slotDims.Y));
                    }
                }
            }
        }
    }
}
