using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameSnake.Snake;
using MonogameSnake.Snake.Classes;
using System;
using static MonogameSnake.Snake.Constants.GameConstants;

namespace MonogameSnake
{
    public class GameSnake : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private SnakeLogic gameLogic;

        Texture2D whiteRectangle;
        Texture2D spriteFruit;
        public GameSnake()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
       
        }

        protected override void Initialize()
        {
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //60);
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1366;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            gameLogic = new SnakeLogic();
            gameLogic.Init(32, 28, 1);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create a 1px square rectangle texture that will be scaled to the
            // desired size and tinted the desired color at draw time
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            spriteFruit = Content.Load<Texture2D>("fruit2");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
            whiteRectangle.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameLogic.players[0].KeyState[(int)PlayerInputKeys.LEFT] = Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed;
            gameLogic.players[0].KeyState[(int)PlayerInputKeys.UP] = Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed;
            gameLogic.players[0].KeyState[(int)PlayerInputKeys.RIGHT] = Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed;
            gameLogic.players[0].KeyState[(int)PlayerInputKeys.DOWN] = Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed;
            gameLogic.players[0].KeyState[(int)PlayerInputKeys.BOOST] = Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed;
            gameLogic.players[0].KeyState[(int)PlayerInputKeys.SLOW_DOWN] = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift) || GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed;
            gameLogic.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            int resW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int resH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int winW = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int winH = GraphicsDevice.PresentationParameters.BackBufferHeight;
            Point tileSize = new Point(winW / gameLogic.level.Width, winH / gameLogic.level.Height);
            spriteBatch.Begin();
            for(int x = 1; x < gameLogic.level.Width; ++x)
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(x * tileSize.X, 0, 1, winH), Color.Chocolate);
            }
            for (int y = 1; y < gameLogic.level.Height; ++y)
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, y * tileSize.Y, winW, 1), Color.Chocolate);
            }

            //Draw Objects
            foreach(var o in gameLogic.objects)
            {
                switch (o.ObjID)
                {
                    case GameObjects.FRUIT:
                        spriteBatch.Draw(spriteFruit, new Rectangle(o.Pos.X * tileSize.X, o.Pos.Y * tileSize.Y, tileSize.X, tileSize.Y), Color.White);
                        break;
                }
            }

            //Draw Players
            for (int playerID = 0; playerID < gameLogic.players.Length; ++playerID)
            {
                Player play = gameLogic.players[playerID];
                for (int i = 0; i < play.BodyLength; ++i)
                {
                    spriteBatch.Draw(whiteRectangle, new Rectangle(play.Body[i].X * tileSize.X, play.Body[i].Y * tileSize.Y, tileSize.X, tileSize.Y), Color.Blue);
                }           
            }
         
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}