using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Bullet : GameObject
{
    public Bullet(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
    : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {
    }

    public void Update(GameTime gameTime)
    {
        // TODO: Move bullet towards top of screen
        var delta = (float)gameTime.ElapsedGameTime.Milliseconds;
        Y -= Vy * delta / 100;
    }
}
