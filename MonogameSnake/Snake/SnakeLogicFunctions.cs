using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonogameSnake.Snake.Classes;
using static MonogameSnake.Snake.Constants.GameConstants;

namespace MonogameSnake.Snake
{
    public partial class SnakeLogic
    {
        public void SpawnFruit()
        {
            Point pos = new Point(rnd.Next(level.Width), rnd.Next(level.Height));
            GameObj o = new GameObj(pos) { ObjID = GameObjects.FRUIT};
            bool isOverlapping = false;
            foreach (var p in players)
            {
                foreach(var b in p.Body)
                {
                    if (o.CollidesWith(b))
                    {
                        isOverlapping = true;
                        break;
                    }
                }
                if (isOverlapping)
                    break;
            }
            objects.Add(o);
        }
    }
}
