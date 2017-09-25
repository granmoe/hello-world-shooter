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
    private int[] MovementFrameCounts = { 10, 20, 40, 60, 80 };
    private string[] MovementTypes = { "left", "right", "freeze" };
    private List<string> Movements = new List<string>();
    private int MovementIndex = 0;
    private Random RandomGen = new Random();

    public Enemy(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
     : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {

        string lastMovementType = null;
        for (int i = 0; i < 100; i++)
        {
            string movementType = MovementTypes[RandomGen.Next(0, MovementTypes.Length - 1)];
            if (i > 0 && lastMovementType == movementType && movementType != "freeze")
            {
                if (movementType == "left")
                {
                    movementType = "right";
                }
                else
                {
                    movementType = "left";
                }
            }
            lastMovementType = movementType;
            int frameCountsToInclude;
            if (movementType == "freeze")
            {
                frameCountsToInclude = 3;
            } else
            {
                frameCountsToInclude = MovementFrameCounts.Length - 1;
            }
            int frameCount = MovementFrameCounts[RandomGen.Next(0, frameCountsToInclude)];

            for (int j = 0; j < frameCount; j++)
            {
                Movements.Add(movementType);
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        // Shoot
        if (LastBulletTime == TimeSpan.Zero || gameTime.TotalGameTime.Subtract(LastBulletTime).CompareTo(BulletDelay) >= 0)
        {
            LastBulletTime = gameTime.TotalGameTime;
            // Set next bullet delay
            int nextBulletDelay = BulletDelayTimes[RandomGen.Next(0, BulletDelayTimes.Length - 1)];
            BulletDelay = TimeSpan.FromMilliseconds(nextBulletDelay);

            int bulletWidth = 10;
            float bulletX = X + (Width / 2) - (bulletWidth / 2);
            float bulletY = Y + Height;
            Bullets.Add(new Bullet(GraphicsDevice, x: bulletX, y: bulletY, vx: 0, vy: -80, width: bulletWidth, height: bulletWidth, color: Color.Red));
        }

        Bullets.ForEach(bullet => bullet.Update(gameTime));

        string currentMovement = Movements[MovementIndex];
        if (currentMovement == "left")
        {
            X = Math.Max(X - (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), 0);
        } else if (currentMovement == "right")
        {
            X = Math.Min(X + (((float)gameTime.ElapsedGameTime.Milliseconds) * 0.8f), GraphicsDevice.Viewport.Width - Width);
        }

        MovementIndex++;
        if (MovementIndex > Movements.Count - 1)
        {
            MovementIndex = 0;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Bullets.ForEach(bullet => bullet.Draw(spriteBatch));
    }
}
