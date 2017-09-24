using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : GameObject
{
    private TimeSpan BulletDelay = new TimeSpan(0, 0, 1);
    private TimeSpan LastBulletTime;
    public List<Bullet> Bullets = new List<Bullet>();
    public Player(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
     : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {
    }

    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            this.Shoot(gameTime);
        Bullets.ForEach(bullet => bullet.Update(gameTime));
    }

    private void Shoot(GameTime gameTime)
    {
        if (LastBulletTime == TimeSpan.Zero || gameTime.TotalGameTime.Subtract(LastBulletTime).CompareTo(BulletDelay) >= 0)
        {
            Bullets.Add(new Bullet(GraphicsDevice, x: 400, y: 360, vx: 0, vy: 10, width: 10, height: 10, color: Color.Red));
            LastBulletTime = gameTime.TotalGameTime;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Bullets.ForEach(bullet => bullet.Draw(spriteBatch));
    }
}
