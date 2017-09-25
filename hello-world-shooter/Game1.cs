using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace hello_world_shooter
{
    public class Game1 : Game
    {
        enum GameStates {Won, Lost, Playing};
        // FIXME, not sure how to get this to work: GameState State = GameStates.Playing;
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

            var center = (GraphicsDevice.Viewport.Width / 2) - (PLAYER_WIDTH / 2);
            enemy = new Enemy(GraphicsDevice, center, 0, GraphicsDevice.Viewport.Width - (PLAYER_WIDTH / 2), 0, PLAYER_WIDTH, PLAYER_WIDTH, Color.WhiteSmoke);
            player = new Player(GraphicsDevice, center, GraphicsDevice.Viewport.Height - PLAYER_WIDTH, 0, 0, PLAYER_WIDTH, PLAYER_WIDTH, Color.WhiteSmoke);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (player.Bullets.Exists(bullet => GameObjectsIntersect(bullet, enemy)))
            {
                Console.WriteLine("ENEMY WAS HIT");
                // TODO: Change game state to "won"
            };
            // Tie goes to the player (check player bullets first). Odds are probably incredibly low that both bullets will hit in the same frame.
            if (enemy.Bullets.Exists(bullet => GameObjectsIntersect(bullet, player)))
            {
                Console.WriteLine("PLAYER WAS HIT");
                // TODO: Change game state to "lost"
            };

            player.Update(gameTime);
            enemy.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
