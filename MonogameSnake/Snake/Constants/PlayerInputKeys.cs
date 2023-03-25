using System;

namespace MonogameSnake.Snake.Constants
{
    public partial class GameConstants
    {
        public const int NUM_OF_PLAYER_KEYS = 9;

     
        public enum PlayerInputKeys
        {
            ENTER = 0,
            BACK = 1,
            LEFT = 2,
            UP= 3,
            RIGHT = 4,
            DOWN = 5,
            BOOST= 6,
            SLOW_DOWN = 7,
            BREAK = 8
        }
    }
}
