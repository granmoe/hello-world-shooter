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
        int PLAYER_WIDTH = 40;

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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Where is the best place to do this?
            // if player.bullets.any(intersects(enemy)
            //    GameState = GameStates.Won
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
