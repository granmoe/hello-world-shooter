using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace hello_world_shooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle enemy;
        Player player;
        Texture2D rectTexture;
        int PLAYER_WIDTH = 100;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "2D Shooter";

            var center = (GraphicsDevice.Viewport.Width / 2) - (PLAYER_WIDTH / 2);
            enemy = new Rectangle(x: center, y: 0, width: PLAYER_WIDTH, height: PLAYER_WIDTH);
            player = new Player(center, GraphicsDevice.Viewport.Height - PLAYER_WIDTH, 0, 0, PLAYER_WIDTH, PLAYER_WIDTH, Color.WhiteSmoke);

            Color[] data = new Color[PLAYER_WIDTH * PLAYER_WIDTH];
            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.PapayaWhip;
            rectTexture = new Texture2D(GraphicsDevice, PLAYER_WIDTH, PLAYER_WIDTH);
            rectTexture.SetData(data);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var enemyPosition = new Vector2(enemy.Left, enemy.Top);

            spriteBatch.Begin();
            spriteBatch.Draw(rectTexture, enemyPosition, Color.White);
            player.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
