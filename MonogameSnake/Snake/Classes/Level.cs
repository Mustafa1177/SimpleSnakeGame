using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogameSnake.Snake.Classes
{
    public class Level
    {
        int _width = 0;
        public int Width { get { return _width; } }
        int _height = 0;
        public int Height { get { return _height; } }

        public Level(int lvlW, int lvlH)
        {
            _width = lvlW;
            _height = lvlH;
        }
    }
}
