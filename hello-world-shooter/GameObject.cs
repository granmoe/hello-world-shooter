using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject
{
    Rectangle Rectangle;
    Texture2D Texture { get; }
    float X { get; set; }
    float Y { get; set; }
    float Vx { get; set; }
    float Vy { get; set; }
    float Width { get; set; }
    float Height { get; set; }
    const int WIDTH = 100;
    Color Color;

    public GameObject(GraphicsDevice graphicsDevice, float x, float y, float vx, float vy, float width, float height, Color color)
    {
        X = x;
        Y = y;
        Vx = vx;
        Vy = vy;
        Width = width;
        Height = height;
        Color = color;

        var center = (graphicsDevice.Viewport.Width / 2) - (WIDTH / 2);
        Rectangle = new Rectangle(x: center, y: graphicsDevice.Viewport.Height - WIDTH, width: WIDTH, height: WIDTH);
        Color[] data = new Color[WIDTH * WIDTH];
        for (int i = 0; i < data.Length; ++i)
            data[i] = Color.PapayaWhip;
        Texture = new Texture2D(graphicsDevice, WIDTH, WIDTH);
        Texture.SetData(data);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(X, Y);
        spriteBatch.Draw(Texture, position, Color);
    }
}
