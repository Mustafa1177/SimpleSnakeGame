using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using static MonogameSnake.Snake.Constants.GameConstants;

namespace MonogameSnake.Snake.Classes
{
    public class Player :Entity
    {
        public string Name = "";
        public bool[] KeyState = new bool[NUM_OF_PLAYER_KEYS];
        public int BodyLength = 1;
        public Color Color = Color.Gray;
        public bool Alive = true;
        public int MovementDelay = 0;
        public GameDirection Direction;
        public List<Point> Body = new List<Point>(127);

        public Player(int spawnX, int spawnY, int initSnakeLen, string name = "") 
        {
            Point spawnPos = new Point(spawnX, spawnY);
            Pos = spawnPos;
            BodyLength = initSnakeLen;
            for(int i = 0; i < BodyLength; i++)
            {
                Body.Add(spawnPos);
            }
            Name = name;
        }

        int _lastStep = 0;
        public bool IsReadyToStep(int gameTick)
        {
            if(gameTick - _lastStep > MovementDelay)
            {
                _lastStep = gameTick;
                return true;
            }
            return false;
        }

        public void GrowUp(int value)
        {
           
            Point insetPos = BodyLength > 0?  Body[BodyLength - 1] : Pos;
            for(int i = 0; i < value; ++i)
                Body.Add(insetPos);       
            BodyLength += value;
        }
    }
}
