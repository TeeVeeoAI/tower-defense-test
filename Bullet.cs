using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace tower_defense__Priv
{
    public class Bullet : Weapon
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 direction;


        public Bullet(Rectangle hitbox, Texture2D texture, Vector2 position, Vector2 velocity, Enemy target, int damage)
            : base (target, damage, hitbox, texture){
                
            this.position = position;
            this.velocity = velocity;

            Vector2 direction = target.Pos - position;

            direction.Normalize();
            this.direction = direction;
        }

        public void Update(GameTime gameTime) {

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * velocity * delta;

            if (position.X < 0 || position.X > 1930 || position.Y < -10 || position.Y > 1080) isAlive = false;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }

        public override void Kill(Enemy target){
            damage -= target.HP + damage;

            if (damage <= 0) isAlive = false;

        }
    }
}