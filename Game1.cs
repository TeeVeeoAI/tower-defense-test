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
    private List<Enemy> enemies = new List<Enemy>();

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
            [
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
            ], pixel
        );

        enemies.Add(new Enemy(20, new Vector2(track.track[0].Location.X, track.track[0].Location.Y), pixel, new Vector2(3, 3), track, Color.Red));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        

        foreach(Enemy enemy in enemies){

            enemy.Update(gameTime);
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        cir.DrawCircle(Color.Green, _spriteBatch, pixel);
        track.DrawTrack(_spriteBatch);
        foreach (Enemy enemy in enemies){
            enemy.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
