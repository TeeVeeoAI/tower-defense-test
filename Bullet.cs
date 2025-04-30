using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Bullet
    {
        private Rectangle hitbox;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Enemy target;

        public Enemy Target {get => target; }
        public Rectangle Hitbox { get => hitbox; }

        public Bullet(Rectangle hitbox, Texture2D texture, Vector2 position, Vector2 velocity, Enemy target) {
            this.hitbox = hitbox;
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.target = target;
        }

        public void Update(GameTime gameTime) {

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}