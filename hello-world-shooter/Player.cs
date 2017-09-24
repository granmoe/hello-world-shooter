using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : GameObject
{
    private TimeSpan BulletDelay = new TimeSpan(0, 0, 0, 0, 500);
    private TimeSpan LastBulletTime;
    public List<Bullet> Bullets = new List<Bullet>();

    public Player(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
     : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {
    }

    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            this.Shoot(gameTime);
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            X = Math.Max(X - (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), 0);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            X = Math.Min(X + (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), GraphicsDevice.Viewport.Width - Width);
        }

        Bullets.ForEach(bullet => bullet.Update(gameTime));
    }

    private void Shoot(GameTime gameTime)
    {
        if (LastBulletTime == TimeSpan.Zero || gameTime.TotalGameTime.Subtract(LastBulletTime).CompareTo(BulletDelay) >= 0)
        {
            int bulletWidth = 10;
            float bulletX = X + (Width / 2) - (bulletWidth / 2);
            float bulletY = Y - bulletWidth;
            Bullets.Add(new Bullet(GraphicsDevice, x: bulletX, y: bulletY, vx: 0, vy: 80, width: bulletWidth, height: bulletWidth, color: Color.Blue));
            LastBulletTime = gameTime.TotalGameTime;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Bullets.ForEach(bullet => bullet.Draw(spriteBatch));
    }
}
