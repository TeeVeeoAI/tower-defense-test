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
    private double[] lastBloon = new double[3];
    private Vector2 spawnPoint;

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

        spawnPoint = new Vector2(track.TrackHB[0].Location.X+25, track.TrackHB[0].Location.Y+25);

        enemies.Add(new Green(20, spawnPoint, pixel, track));
        enemies.Add(new Blue(20, spawnPoint, pixel, track));
        heroes.Add(new Gunner(new Vector2(400, 400), pixel, new Color(30, 40, 70), new Color(20,20,20, 100), enemies));
        heroes.Add(new Gunner(new Vector2(700, 400), pixel, new Color(30, 40, 70), new Color(20,20,20, 100), enemies));
        heroes.Add(new Swordsman(new Vector2(460, 400), pixel, new Color(40,70,30), new Color(20,20,20, 100), enemies));
    }

    protected override void Update(GameTime gameTime)
    {

        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)Enemy.EnemyType.Green-1] + 1){
            lastBloon[(int)Enemy.EnemyType.Green-1] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Green(20, spawnPoint, pixel, track));
        }

        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)Enemy.EnemyType.Blue-1] + 3){
            lastBloon[(int)Enemy.EnemyType.Blue-1] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Blue(20, spawnPoint, pixel, track));
        }

        kState = Keyboard.GetState();
        mState = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
            Exit();

        

        foreach(Enemy enemy in enemies){
            enemy.Update(gameTime);
        }

        foreach(Hero hero in heroes){
            hero.Update(gameTime);
            foreach(Weapon weapon in hero.Weapons){
                weapon.Update(gameTime);
            }
        }

        HitCheck(gameTime);

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
            foreach (Weapon weapon in hero.Weapons){
                weapon.Draw(_spriteBatch);
            }
        }
        DrawHeroSelect(gameTime);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    public void HitCheck(GameTime gameTime){
        for (int i = 0; i < enemies.Count; i++){
            for (int j = 0; j < heroes.Count; j++){
                for (int k = 0; k < heroes[j].Weapons.Count; k++){
                    if (enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].Hitbox) || enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].TureHitbox.Corners)){
                        
                        enemies[i].Hit(heroes[j].Weapons[k].Damage);
                        heroes[j].Weapons[k].Kill(enemies[i]);

                        if (enemies[i].HP <= 0){

                            if(enemies[i] is Blue) {
                                enemies.Add(new Green(20, new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                            } else if(enemies[i] is Green){
                                enemies.Add(new Red(20, new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                            }
                            enemies.RemoveAt(i);
                        }
                    }
                    if (heroes[j].Weapons[k].IsAlive == false) {
                        heroes[j].Weapons.RemoveAt(k);
                    }
                }
            }
        }
    }
    public void DrawHeroSelect(GameTime gameTime){
        _spriteBatch.Draw(pixel, new Rectangle(1520, 0, 400, 1080), new Color(20, 20, 20, 100));

    }
}
