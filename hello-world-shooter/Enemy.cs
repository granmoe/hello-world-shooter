using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Enemy : GameObject
{
    public List<Bullet> Bullets = new List<Bullet>();
    private TimeSpan BulletDelay = new TimeSpan(0, 0, 0, 0, 500);
    private TimeSpan LastBulletTime;
    private int[] BulletDelayTimes = { 500, 650, 750, 1000, 1300 };
    private Random randomGen = new Random();

    public Enemy(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
     : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {
        // TODO: Create movement array
        // Movement array should have strings of random [15,30,60,90,120] movements which should roughly translate to 1/4 sec, 1/2 sec, 1 sec, etc
    }

    public void Update(GameTime gameTime)
    {
        // Shoot
        if (LastBulletTime == TimeSpan.Zero || gameTime.TotalGameTime.Subtract(LastBulletTime).CompareTo(BulletDelay) >= 0)
        {
            LastBulletTime = gameTime.TotalGameTime;
            // Set next bullet delay
            int nextBulletDelay = BulletDelayTimes[randomGen.Next(0, BulletDelayTimes.Length - 1)];
            BulletDelay = TimeSpan.FromMilliseconds(nextBulletDelay);

            int bulletWidth = 10;
            float bulletX = X + (Width / 2) - (bulletWidth / 2);
            float bulletY = Y + Height;
            Bullets.Add(new Bullet(GraphicsDevice, x: bulletX, y: bulletY, vx: 0, vy: -80, width: bulletWidth, height: bulletWidth, color: Color.Red));
        }

        Bullets.ForEach(bullet => bullet.Update(gameTime));

        // Move
        //if (Keyboard.GetState().IsKeyDown(Keys.Left))
        //{
        //    X = Math.Max(X - (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), 0);
        //}
        //else if (Keyboard.GetState().IsKeyDown(Keys.Right))
        //{
        //    X = Math.Min(X + (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), GraphicsDevice.Viewport.Width - Width);
        //}
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Bullets.ForEach(bullet => bullet.Draw(spriteBatch));
    }
}
