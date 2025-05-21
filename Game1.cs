using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tower_defense__Priv;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //textures & fonts
    private Texture2D pixel, GAr, CAr, YAr;
    private SpriteFont font, font2;

    //track
    private Track track;

    //heros & hero select
    private List<Hero> heroes = new List<Hero>();
    private Color[] herosCo;
    private Circle heroSelectSM, heroSelectG;
    private Vector2 heroSelectSMPos, heroSelectGPos;
    private Rectangle heroSelectSMRec, heroSelectGRec;

    //input
    private KeyboardState kState, oldKState;
    private MouseState mState, oldMState;

    //enemies
    private List<Enemy> enemies = new List<Enemy>();
    private double[] lastBloon = new double[3];
    private Vector2 spawnPoint;

    //hover & keys
    private Hero hovering;
    private Keys gunnerK, swordsmanK, unselectK, placeK, startWaveK;
    private Vector2 hoverPos;

    //money
    private double[] cost;
    private double money, endWaveMoney;

    //wave & stat board
    private bool waveStarted, waveEnded, cantPlace;
    private int currWaveNum;
    private Vector2 statBoardPos;

    //losing & wining
    private int lives;
    private bool gameOver, gameWon;

    //screen
    private float sHeight, sWidth;


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
        GAr = Content.Load<Texture2D>("green_ar");
        CAr = Content.Load<Texture2D>("crimson_ar");
        YAr = Content.Load<Texture2D>("yellow_ar");


        font = Content.Load<SpriteFont>("Font");
        font2 = Content.Load<SpriteFont>("Font2");


        swordsmanK = Keys.D1;
        gunnerK = Keys.D2;
        unselectK = Keys.U;
        placeK = Keys.K;
        startWaveK = Keys.P;
        cantPlace = false;
        lives = 200;
        waveStarted = false;
        waveEnded = false;
        currWaveNum = 1;
        sHeight = _graphics.PreferredBackBufferHeight;
        sWidth = _graphics.PreferredBackBufferWidth;
        gameOver = false;
        gameWon = false;

        money = 500;
        endWaveMoney = 1000;
        cost = [
            1000,
            500
        ];

        hoverPos = new Vector2(sWidth + 200, sHeight + 200);
        hovering = new HoverHero(hoverPos, pixel);

        herosCo = [
            new Color(40,70,30),
            new Color(30, 40, 70)
        ];

        heroSelectSMPos = new Vector2(sWidth - 400, 0);
        heroSelectGPos = new Vector2(sWidth - 200, 0);
        statBoardPos = new Vector2(10, 880);

        heroSelectSM = new Circle(heroSelectSMPos + new Vector2(100, 100), 30);
        heroSelectG = new Circle(heroSelectGPos + new Vector2(100, 100), 20);

        heroSelectSMRec = new Rectangle((int)heroSelectSMPos.X, (int)heroSelectSMPos.Y, 200, 200);
        heroSelectGRec = new Rectangle((int)heroSelectGPos.X, (int)heroSelectGPos.Y, 200, 200);

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
                new Rectangle(1400, 650, 520, 50),

                new Rectangle(1920, 650, 50, 50) //extra part to win
                
            ], pixel
        );

        spawnPoint = new Vector2(track.TrackHB[0].Location.X + 25, track.TrackHB[0].Location.Y);

        //enemies.Add(new Green(20, spawnPoint, pixel, track));
        enemies.Add(new Blue(spawnPoint, pixel, track));
        //heroes.Add(new Gunner(new Vector2(400, 400), pixel, enemies));
        //heroes.Add(new Gunner(new Vector2(700, 400), pixel, enemies));
        //heroes.Add(new Swordsman(new Vector2(460, 400), pixel, enemies));
    }

    /*
    //
    //
    //
    //
    //Update
    //
    //
    //
    //
    */

    protected override void Update(GameTime gameTime)
    {
        kState = Keyboard.GetState();
        mState = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.RightControl) || kState.IsKeyDown(Keys.Escape))
            Exit();


        if (gameOver)
        {
            GameOver(gameTime);
            return;
        }



        if (kState.IsKeyDown(startWaveK) && oldKState.IsKeyUp(startWaveK) && !waveStarted) waveStarted = true;

        enemies.Sort((a, b) => b.Progress.CompareTo(a.Progress));

        LosingCondition(gameTime);

        CantPlace(gameTime);

        if (hovering is Swordsman)
        {
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Swordsman(hoverPos, pixel);
            if (((kState.IsKeyDown(placeK) && oldKState.IsKeyUp(placeK)) || (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)) && money >= cost[(int)HeroTypes.Swordsman] && !cantPlace)
            {
                heroes.Add(new Swordsman(hoverPos, pixel, enemies));
                money -= 1000;
                hoverPos = new Vector2(2000, 1100);
                hovering = new HoverHero(hoverPos, pixel);

            }
        }

        if (hovering is Gunner)
        {
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Gunner(hoverPos, pixel);
            if (((kState.IsKeyDown(placeK) && oldKState.IsKeyUp(placeK)) || (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)) && money >= cost[(int)HeroTypes.Gunner] && !cantPlace)
            {
                heroes.Add(new Gunner(hoverPos, pixel, enemies));
                money -= 500;
                hoverPos = new Vector2(2000, 1100);
                hovering = new HoverHero(hoverPos, pixel);
            }
        }

        SpawnEnemy(gameTime);


        foreach (Enemy enemy in enemies)
        {
            enemy.Update(gameTime);
        }

        foreach (Hero hero in heroes)
        {
            hero.Update(gameTime);
            foreach (Weapon weapon in hero.Weapons)
            {
                weapon.Update(gameTime);
            }
        }

        HitCheck(gameTime);

        HeroSelect(gameTime);

        // TODO: Add your update logic here

        oldKState = kState;
        oldMState = mState;

        base.Update(gameTime);
    }

    public void HitCheck(GameTime gameTime)
    {
        for (int j = 0; j < heroes.Count; j++)
        {
            for (int k = 0; k < heroes[j].Weapons.Count; k++)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].Hitbox) || enemies[i].Hitbox.Intersects(heroes[j].Weapons[k].TureHitbox.Corners))
                    {

                        enemies[i].Hit(heroes[j].Weapons[k].Damage);
                        heroes[j].Weapons[k].Kill(enemies[i]);

                        if (enemies[i].HP <= 0)
                        {
                            money += 20;

                            if (enemies[i] is Blue)
                            {
                                enemies.Add(new Green(new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                                money += 40;
                            }
                            else if (enemies[i] is Green)
                            {
                                enemies.Add(new Red(new Vector2(enemies[i].Pos.X, enemies[i].Pos.Y), pixel, track, enemies[i].CurrentWaypointIndex));
                                money += 20;
                            }
                            enemies.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (heroes[j].Weapons[k].IsAlive == false)
                {
                    heroes[j].Weapons.RemoveAt(k);
                    k--;
                }
            }
        }
    }

    public void LosingCondition(GameTime gameTime)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].AttEnd)
            {
                lives -= enemies[i].HP;
                enemies.RemoveAt(i);
                i--;
            }
        }
        if (lives <= 0)
        {
            gameOver = true;
        }
    }

    public void SpawnEnemy(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)EnemyType.Green] + 1 && waveStarted)
        {
            lastBloon[(int)EnemyType.Green] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Green(spawnPoint, pixel, track));

        }

        if (gameTime.TotalGameTime.TotalSeconds > lastBloon[(int)EnemyType.Blue] + 3 && waveStarted)
        {
            lastBloon[(int)EnemyType.Blue] = gameTime.TotalGameTime.TotalSeconds;
            enemies.Add(new Blue(spawnPoint, pixel, track));
        }
    }

    public void NewWave(GameTime gameTime)
    {
        if (waveEnded)
        {
            money += Math.Floor(endWaveMoney * (currWaveNum * 0.7f));
        }
    }

    public void EndWave(GameTime gameTime)
    {

    }

    public void CantPlace(GameTime gameTime)
    {
        foreach (Rectangle r in track.TrackHB)
        {
            if (hovering.Hitbox.Intersects(r))
            {
                cantPlace = true;
                return;
            }
            else cantPlace = false;
        }
        foreach (Hero h in heroes)
        {
            if (hovering.Hitbox.Intersects(h.Hitbox))
            {
                cantPlace = true;
                return;
            }
            else cantPlace = false;
        }
    }

    public void HeroSelect(GameTime gameTime)
    {
        if (kState.IsKeyDown(gunnerK) || (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released && new Circle(new Vector2(mState.X, mState.Y), 1).Intersects(heroSelectGRec)))
        {
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Gunner(hoverPos, pixel);
        }
        else if (kState.IsKeyDown(swordsmanK) || (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released && new Circle(new Vector2(mState.X, mState.Y), 1).Intersects(heroSelectSMRec)))
        {
            hoverPos = new Vector2(mState.X, mState.Y);
            hovering = new Swordsman(hoverPos, pixel);
        }
        else if (kState.IsKeyDown(unselectK) || (mState.RightButton == ButtonState.Pressed && oldMState.RightButton == ButtonState.Released))
        {
            hoverPos = new Vector2(2000, 1100);
            hovering = new HoverHero(hoverPos, pixel);
        }
    }

    public void GameOver(GameTime gameTime)
    {

    }

    /*
    //
    //
    //
    //
    //Drawing
    //
    //
    //
    //
    */
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        if (gameOver)
        {
            _spriteBatch.Begin();

            DrawGameOver(gameTime, _spriteBatch);

            _spriteBatch.End();
            return;
        }

        _spriteBatch.Begin();
        track.DrawTrack(gameTime, _spriteBatch);
        foreach (Enemy enemy in enemies)
        {
            enemy.Draw(_spriteBatch);
        }
        foreach (Hero hero in heroes)
        {
            hero.Draw(_spriteBatch, new Circle(new Vector2(mState.X, mState.Y), 1).Intersects(hero.Hitbox));
            foreach (Weapon weapon in hero.Weapons)
            {
                weapon.Draw(_spriteBatch);
            }
        }
        DrawHeroSelect(gameTime);
        DrawStatsBoard(gameTime);
        _spriteBatch.Draw(CAr, new Rectangle(1420, 980, 100, 100), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void DrawHeroSelect(GameTime gameTime)
    {

        //boxes
        _spriteBatch.Draw(pixel, heroSelectSMRec, new Color((int)herosCo[0].R, (int)herosCo[0].G, (int)herosCo[0].B, 100));
        _spriteBatch.Draw(pixel, heroSelectGRec, new Color((int)herosCo[1].R, (int)herosCo[1].G, (int)herosCo[1].B, 100));

        //heros
        heroSelectSM.DrawCircle(herosCo[0], _spriteBatch, pixel);
        heroSelectG.DrawCircle(herosCo[1], _spriteBatch, pixel);

        //text
        //cost
        _spriteBatch.DrawString(font, "Swordsman", heroSelectSMPos + new Vector2(10, 0), Color.Black);
        _spriteBatch.DrawString(font, "Gunner", heroSelectGPos + new Vector2(10, 0), Color.Black);
        _spriteBatch.DrawString(font, "$" + cost[(int)HeroTypes.Swordsman].ToString(), heroSelectSMPos + new Vector2(10, 150), Color.Black);
        _spriteBatch.DrawString(font, "$" + cost[(int)HeroTypes.Gunner].ToString(), heroSelectGPos + new Vector2(10, 150), Color.Black);

        //hover
        if (!(hovering is HoverHero))
        {
            hovering.Draw(_spriteBatch, true);
        }
    }

    public void DrawStatsBoard(GameTime gameTime)
    {

        //board
        _spriteBatch.Draw(pixel, new Rectangle((int)statBoardPos.X, (int)statBoardPos.Y, 175, 175), new Color(10, 10, 10));
        DrawOutline(gameTime, new Rectangle((int)statBoardPos.X, (int)statBoardPos.Y, 175, 175), 2, new Color(255, 200, 140));

        //stats
        _spriteBatch.DrawString(font2, "lives: " + lives.ToString(), statBoardPos + new Vector2(10, 0), Color.White);
        _spriteBatch.DrawString(font2, "wave: " + currWaveNum.ToString(), statBoardPos + new Vector2(10, 30), Color.White);
        _spriteBatch.DrawString(font2, "$" + money.ToString(), statBoardPos + new Vector2(10, 60), Color.White);
    }

    public void DrawOutline(GameTime gameTime, Rectangle rectangle, int width, Color color)
    {
        //left
        _spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, width, rectangle.Height), color);

        //right
        _spriteBatch.Draw(pixel, new Rectangle(rectangle.Right - width, rectangle.Top, width, rectangle.Height), color);

        //top
        _spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, width), color);

        //bottom
        _spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom - width, rectangle.Width, width), color);
    }

    public void DrawGameOver(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "lost", new Vector2(sWidth / 2, sHeight / 2), Color.Black);
    }
}
