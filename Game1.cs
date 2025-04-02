using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tower_defense__Priv;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D pixel;
    private Texture2D cirT;
    private Circle cir;
    private Rectangle box;
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

        cirT = Content.Load<Texture2D>("cir");
        pixel = Content.Load<Texture2D>("Pixel");
        cir = new Circle(new Vector2(400, 400), 20);
        box = new Rectangle(430, 400, 20, 20);
        boxColor = Color.White;

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if (cir.Intersects(box)){
            boxColor = Color.Red;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        _spriteBatch.Draw(pixel, box, boxColor);
        _spriteBatch.Draw(cirT, new Rectangle((int)cir.Pos.X, (int)cir.Pos.Y, (int)(cir.Radius*2), (int)(cir.Radius*2)), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
