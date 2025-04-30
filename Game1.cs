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
    private Track track;
    private List<Enemy> enemies = new List<Enemy>();
    private List<Hero> heroes = new List<Hero>();
    private KeyboardState kState;
    private MouseState mState;

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

        enemies.Add(new Enemy(20, new Vector2(track.TrackHB[0].Location.X+25, track.TrackHB[0].Location.Y+25), pixel, new Vector2(3, 3), track, Color.Red));
        heroes.Add(new Hero(new Vector2(400, 400), pixel, new Color(30, 40, 70), new Color(20,20,20, 100), 70f, 200f, enemies));
        heroes.Add(new Hero(new Vector2(700, 400), pixel, new Color(30, 40, 70), new Color(20,20,20, 100), 70f, 200f, enemies));
    }

    protected override void Update(GameTime gameTime)
    {

        kState = Keyboard.GetState();
        mState = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
            Exit();

        

        foreach(Enemy enemy in enemies){
            enemy.Update(gameTime);
        }

        foreach(Hero hero in heroes){
            hero.Update(gameTime);
            foreach(Bullet bullet in hero.Bullets){
                bullet.Update(gameTime);
            }
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        track.DrawTrack(_spriteBatch);
        foreach (Enemy enemy in enemies){
            enemy.Draw(_spriteBatch);
        }
        foreach (Hero hero in heroes){
            hero.Draw(_spriteBatch);
            foreach (Bullet bullet in hero.Bullets){
                bullet.Draw(_spriteBatch);
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
