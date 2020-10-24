using Microsoft.Xna.Framework;

namespace top_down_shooter
{
    public class GridLocation
    {
        // filled = filled by something...like a building
        // impassable = you cannot walk through it && it counts as being filled
        // unPathable = you cannot walk through it... but doesn't always mean it's filled
        //              meaning.. you can build on it but the mobs can never walk through it
        //              maybe that could be a raised platform
        public bool filled, impassable, unPathable;

        // fScore = the cost of getting to a node
        // cost = cost of traveling through a node
        // currentDistance = total distance you've traveled to get to a node
        public float fScore, cost, currentDistance;

        public Vector2 parent, position;

        public GridLocation(float COST, bool FILLED)
        {
            cost = COST;
            filled = FILLED;

            unPathable = false;
            impassable = false;
        }

        public virtual void SetToFilled(bool IMPASSABLE)
        {
            filled = true;
            impassable = IMPASSABLE;
        }
    }
}
