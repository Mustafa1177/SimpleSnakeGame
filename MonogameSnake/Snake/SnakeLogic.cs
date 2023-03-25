using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameSnake.Snake.Classes;
using static MonogameSnake.Snake.Constants.GameConstants;

namespace MonogameSnake.Snake
{
    public partial class SnakeLogic
    {
        public Level level;
        public Player[] players = new Player[1];
        public List<GameObj> objects = new List<GameObj>();
        Random rnd = new Random();

        //Game Options
        public int playerSlowSpeed = 5;
        public int playerNormalSpeed = 3;
        public int playerFastSpeed = 1;
        public int fruitGainValue = 5;

        //General Variables
        public int gameTickCount = 0;



        public SnakeLogic() { }

        public void Init(int levelW, int levelH, int numOfPlayers)
        {
            level = new Level(levelW, levelH);
            players = new Player[numOfPlayers];
            for (int i = 0; i < numOfPlayers; i++)
            {
                players[i] = new Player(levelW / 2, levelH / 2, 24, "Player " + (i + 1));
                players[i].MovementDelay = 3;
                players[i].Direction = GameDirection.RIGHT;
            }
            SpawnFruit();
        }

        public void Update()
        {
            for (int playerID = 0; playerID < players.Length; ++playerID)
            {
                Player play = players[playerID];

                play.MovementDelay = play.KeyState[(int)PlayerInputKeys.BOOST] ? playerFastSpeed : (play.KeyState[(int)PlayerInputKeys.SLOW_DOWN] ? playerSlowSpeed : playerNormalSpeed);

                if (play.KeyState[(int)PlayerInputKeys.BOOST])
                {

                }

                    if (play.IsReadyToStep(gameTickCount))
                {
                    if (play.KeyState[(int)PlayerInputKeys.LEFT])
                    {
                        play.Direction = GameDirection.LEFT;
                    }
                    else if (play.KeyState[(int)PlayerInputKeys.RIGHT])
                    {
                        play.Direction = GameDirection.RIGHT;
                    }

                    if (play.KeyState[(int)PlayerInputKeys.UP])
                    {
                        play.Direction = GameDirection.UP;
                    }
                    else if (play.KeyState[(int)PlayerInputKeys.DOWN])
                    {
                        play.Direction = GameDirection.DOWN;
                    }

                    int dx = play.Direction == GameDirection.RIGHT ? 1 : (play.Direction == GameDirection.LEFT ? -1 : 0);
                    int dy = play.Direction == GameDirection.DOWN ? 1 : (play.Direction == GameDirection.UP ? -1 : 0);
                    int newX = play.Pos.X + dx, newY = play.Pos.Y + dy;
                    newX = newX < 0 ? level.Width - 1 : (newX >= level.Width ? 0 : newX);
                    newY = newY < 0 ? level.Height - 1 : (newY >= level.Height ? 0 : newY);
                    Point newPos = new Point(newX, newY);
                    play.Pos = newPos;
                    play.Body.RemoveAt(play.BodyLength - 1);
                    play.Body.Insert(0, newPos);
                }
            }

            for (int i = 0; i < objects.Count; ++i)
            {
                var o = objects[i];
                switch (o.ObjID)
                {
                    case GameObjects.FRUIT:
                    case GameObjects.GOLDEN_FRUIT:
                        for (int playerID = 0; playerID < players.Length; ++playerID)
                        {
                            if(players[playerID].CollidesWith(o))
                            {
                                objects.Remove(o);
                                players[playerID].GrowUp(fruitGainValue);
                                SpawnFruit();
                                i --;
                                break;
                            }
                        }

                            break;
                }
            }

            gameTickCount++;
        }
    }
}
