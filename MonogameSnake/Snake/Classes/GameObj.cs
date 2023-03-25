using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static MonogameSnake.Snake.Constants.GameConstants;

namespace MonogameSnake.Snake.Classes
{
    public class GameObj : Entity
    {
        public GameObjects ObjID;

        public GameObj() { }
        public GameObj(Point pos) { Pos = pos; }

        public GameObj(int x, int y) { Pos = new Point(x, y); }

    }
}
