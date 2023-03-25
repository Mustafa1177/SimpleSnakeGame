using Microsoft.Xna.Framework;

namespace MonogameSnake.Snake.Classes
{
    public class Entity
    {
        public Point Pos;

        public Entity() { }

        public Entity(Point pos) { Pos = pos; }
        public Entity(int x, int y) { Pos = new Point(x, y); }

        public bool CollidesWith(Entity e)
        {
            if(Pos.X == e.Pos.X && Pos.Y == e.Pos.Y)
                return true;
            else return false;
        }

        public bool CollidesWith(Point pt)
        {
            if (Pos.X == pt.X && Pos.Y == pt.Y)
                return true;
            else return false;
        }

    }
}
