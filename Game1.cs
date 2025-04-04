using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tower_defense__Priv;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D pixel;
    private Circle cir;
    private Track track;
    private Color boxColor;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.PreferredBackBufferWidth = 1920;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        pixel = Content.Load<Texture2D>("Pixel");
        cir = new Circle(new Vector2(400, 400), 20);

        track = new Track(
            new Rectangle[]
            {
                new Rectangle(200, 0, 50, 200),
                new Rectangle(200, 200, 300, 50),
                new Rectangle(500, 200, 50, 300),
                new Rectangle(500, 500, 300, 50),
                new Rectangle(800, 350, 50, 200),
                new Rectangle(800, 300, 300, 50),
                new Rectangle(1100, 300, 50, 600),
                new Rectangle(1100, 900, 300, 50),
                new Rectangle(1400, 650, 50, 300),
                new Rectangle(1400, 650, 520, 50)
            }, pixel
        );

        boxColor = Color.White;

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        DrawCircle(cir.Pos, cir.Radius, Color.Green);
        track.DrawTrack(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    void DrawCircle(Vector2 center, float radius, Color color)
    {
        int r = (int)radius;
        int cx = (int)center.X;
        int cy = (int)center.Y;

        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r) // Check if inside circle
                {
                    _spriteBatch.Draw(pixel, new Vector2(cx + x, cy + y), color);
                }
            }
        }
    }
}
