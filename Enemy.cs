using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Enemy
    {
        public Circle hitbox { get; private set;}
        public Vector2 pos { get; private set; }
        public Texture2D texture { get; private set; }
        public Vector2 velocity { get; private set; }
        public Color color { get; private set; }

        public Enemy(float radius, Vector2 pos, Texture2D texture, Vector2 velocity, Color color){
            this.pos = pos;
            this.texture = texture;
            this.velocity = velocity;
            this.color = color;

            this.hitbox = new Circle(pos, radius);
        }

        public void Update(GameTime gameTime){
            pos += velocity;

            hitbox.ChangePos(pos, this.GetType().ToString().Remove(0, 20));
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
        }
    }
}