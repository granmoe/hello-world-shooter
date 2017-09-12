using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : Game
{
    SpriteBatch spriteBatch;
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

    public Player(float x, float y, float vx, float vy, float width, float height, Color color)
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        X = x;
        Y = y;
        Vx = vx;
        Vy = vy;
        Width = width;
        Height = height;
        Color = color;

        var center = (GraphicsDevice.Viewport.Width / 2) - (WIDTH / 2);
        Rectangle = new Rectangle(x: center, y: GraphicsDevice.Viewport.Height - WIDTH, width: WIDTH, height: WIDTH);
        Color[] data = new Color[WIDTH * WIDTH];
        for (int i = 0; i < data.Length; ++i)
            data[i] = Color.PapayaWhip;
        Texture = new Texture2D(GraphicsDevice, WIDTH, WIDTH);
        Texture.SetData(data);
    }

    //public void Update(GameTime gameTime)
    //{

    //}

    public override void Draw(GameTime gameTime)
    {
        var position = new Vector2(X, Y);
        spriteBatch.Draw(Texture, position, Color);
    }
}
