using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject
{
    Rectangle Rectangle;
    Texture2D Texture { get; }
    protected GraphicsDevice GraphicsDevice;
    public float X { get; set; }
    public float Y { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    int Width { get; set; }
    int Height { get; set; }
    Color Color;

    public GameObject(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, int width, int height, Color color)
    {
        GraphicsDevice = graphicsDevice;
        X = x;
        Y = y;
        Vx = vx;
        Vy = vy;
        Width = width;
        Height = height;
        Color = color;

        var center = (graphicsDevice.Viewport.Width / 2) - (width / 2);
        Rectangle = new Rectangle(x: center, y: graphicsDevice.Viewport.Height - width, width: width, height: width);
        Color[] data = new Color[width * width];
        for (int i = 0; i < data.Length; ++i)
            data[i] = Color.PapayaWhip;
        Texture = new Texture2D(graphicsDevice, width, width);
        Texture.SetData(data);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(X, Y);
        spriteBatch.Draw(Texture, position, Color);
    }
}
