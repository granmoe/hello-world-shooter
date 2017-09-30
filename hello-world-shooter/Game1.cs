using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace hello_world_shooter
{
    public class Game1 : Game
    {
        SpriteFont font;
        GameState State;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Enemy enemy;
        Player player;
        int PLAYER_WIDTH = 50;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "2D Shooter";

            State = GameState.Playing;
            var center = (GraphicsDevice.Viewport.Width / 2) - (PLAYER_WIDTH / 2);
            enemy = new Enemy(GraphicsDevice, center, 0, GraphicsDevice.Viewport.Width - (PLAYER_WIDTH / 2), 0, PLAYER_WIDTH, PLAYER_WIDTH, Color.WhiteSmoke);
            player = new Player(GraphicsDevice, center, GraphicsDevice.Viewport.Height - PLAYER_WIDTH, 0, 0, PLAYER_WIDTH, PLAYER_WIDTH, Color.WhiteSmoke);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
        }

        // start and end must be sorted low to high
        private bool LinesIntersect(float lineOneStart, float lineOneEnd, float lineTwoStart, float lineTwoEnd)
        {
            return (Math.Min(lineOneEnd, lineTwoEnd) - Math.Max(lineOneStart, lineTwoStart)) > 0;
        }

        private bool GameObjectsIntersect(GameObject first, GameObject second)
        {
            bool yIntersects = LinesIntersect(first.X, first.X + first.Width, second.X, second.X + second.Width);
            bool xIntersects = LinesIntersect(first.Y, first.Y + first.Height, second.Y, second.Y + second.Height);
            return yIntersects && xIntersects;
        }

        protected override void Update(GameTime gameTime)
        {
            if (State != GameState.Playing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    this.Initialize();
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (player.Bullets.Exists(bullet => GameObjectsIntersect(bullet, enemy)))
                State = GameState.Won;

            if (enemy.Bullets.Exists(bullet => GameObjectsIntersect(bullet, player)))
                State = GameState.Lost;

            player.Update(gameTime);
            enemy.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (State == GameState.Playing)
            {
                spriteBatch.Begin();
                enemy.Draw(spriteBatch);
                player.Draw(spriteBatch);
                spriteBatch.End();
            } else {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, $"{State.ToString()}", new Vector2(20, 100), Color.Black);
                spriteBatch.DrawString(font, "Press R to restart", new Vector2(20, 200), Color.Black);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
