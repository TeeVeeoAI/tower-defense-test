using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tower_defense__Priv;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Color[] herosCo;
    private Circle heroSelectSM, heroSelectG;
    private Texture2D pixel;
    private SpriteFont font;
    private Track track;
    private List<Enemy> enemies = new List<Enemy>();
    private List<Hero> heroes = new List<Hero>();
    private KeyboardState kState, oldKState;
    private MouseState mState, oldMState;
    private double[] lastBloon = new double[3];
    private Vector2 spawnPoint;
    private Hero hovering;
    private Keys gunnerK, swordsmanK, unselectK, placeK;
    private Vector2 hoverPos;
    private double money = 0;
    

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
        font = Content.Load<SpriteFont>("Font");

        gunnerK = Keys.D1;
        swordsmanK = Keys.D2;
        unselectK = Keys.U;
        placeK = Keys.P;
        hoverPos = new Vector2(2000, 1100);
        hovering = new HoverHero(hoverPos, pixel);

        herosCo = [
            new Color(40,70,30),
            new Color(30, 40, 70)
        ];
        heroSelectSM = new Circle(new Vector2(1000, 1000), 30);
        heroSelectG = new Circle(new Vector2(400, 500), 20);
        

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
        heroes.Add(new Gunner(new Vector2(400, 400), pixel, enemies));
        heroes.Add(new Gunner(new Vector2(700, 400), pixel, enemies));
        heroes.Add(new Swordsman(new Vector2(460, 400), pixel, enemies));
    }

    protected override void Update(GameTime gameTime)
    {
        kState = Keyboard.GetState();
        mState = Mouse.GetState();

        enemies.Sort((a, b) => b.Progress.CompareTo(a.Progress));
        if (hovering is Swordsman){
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Swordsman(hoverPos, pixel);
            if (kState.IsKeyDown(placeK) && oldKState.IsKeyUp(placeK) && money > 1000){
                heroes.Add(new Swordsman(hoverPos, pixel, enemies));
                money -= 1000;
            }
        }
        if (hovering is Gunner){
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Gunner(hoverPos, pixel);
            if (kState.IsKeyDown(placeK) && oldKState.IsKeyUp(placeK) && money > 500){
                heroes.Add(new Gunner(hoverPos, pixel, enemies));
                money -= 500;
            }
        }

        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)EnemyType.Green - 1] + 1){
            lastBloon[(int)EnemyType.Green - 1] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Green(20, spawnPoint, pixel, track));
        }

        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)EnemyType.Blue-1] + 3){
            lastBloon[(int)EnemyType.Blue - 1] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Blue(20, spawnPoint, pixel, track));
        }

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

        if (kState.IsKeyDown(gunnerK)) {
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Gunner(hoverPos, pixel);
        } else if (kState.IsKeyDown(swordsmanK)){
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Swordsman(hoverPos, pixel);
        } else if (kState.IsKeyDown(unselectK)){
            hoverPos = new Vector2(2000, 1100);
            hovering = new HoverHero(hoverPos, pixel);
        }

        // TODO: Add your update logic here

        oldKState = kState;
        oldMState = mState;

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
        _spriteBatch.DrawString(font, "$" + money.ToString(), new Vector2(0, 0), Color.White);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    public void HitCheck(GameTime gameTime){
        for (int j = 0; j < heroes.Count; j++){
            for (int k = 0; k < heroes[j].Weapons.Count; k++){
                for (int i = 0; i < enemies.Count; i++){
                    if (enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].Hitbox) || enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].TureHitbox.Corners)){
                        
                        enemies[i].Hit(heroes[j].Weapons[k].Damage);
                        heroes[j].Weapons[k].Kill(enemies[i]);

                        if (enemies[i].HP <= 0){
                            money += 20;

                            if(enemies[i] is Blue) {
                                enemies.Add(new Green(20, new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                                money += 40;
                            } else if(enemies[i] is Green){
                                enemies.Add(new Red(20, new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                                money += 20;
                            }
                            enemies.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (heroes[j].Weapons[k].IsAlive == false) {
                    heroes[j].Weapons.RemoveAt(k);
                    k--;
                }
            }
        }
    }
    public void DrawHeroSelect(GameTime gameTime){
        _spriteBatch.Draw(pixel, new Rectangle(1520, 0, 400, 1080), new Color(20, 20, 20, 100));
        
        heroSelectSM.DrawCircle(herosCo[0] ,_spriteBatch, pixel);
        heroSelectG.DrawCircle(herosCo[1], _spriteBatch, pixel);
        
        
        if (!(hovering is HoverHero)){
            hovering.Draw(_spriteBatch);
        }
    }
}
