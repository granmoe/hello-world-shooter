using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : GameObject
{
    public Player(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, float width, float height, Color color)
    : base(graphicsDevice, x, y, vx, vy, width, height, color)
    {
    }
}
