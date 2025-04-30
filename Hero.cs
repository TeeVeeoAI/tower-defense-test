using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Hero
    {
        private Circle hitbox;
        private Circle range;
        private List<Bullet> bullets = new List<Bullet>();
        private Vector2 pos;
        private Color color;
        private Color rangeColor;
        private Texture2D texture;
        private List<Enemy> enemies;
        private double shootDelay = 1.25;
        private double timeWhenShoot = 0;

        public Circle Hitbox { get => hitbox;}
        public Circle Range { get => range;}
        public List<Bullet> Bullets { get => bullets; }
        
        public Hero(Vector2 pos, Texture2D texture, Color color, Color rangeColor, float size, float rangeRadius, List<Enemy> enemies){
            this.pos = pos;
            this.texture = texture;
            this.color = color;
            this.rangeColor = rangeColor;
            this.enemies = enemies;

            this.hitbox = new Circle(this.pos, size);
            this.range = new Circle(this.pos, rangeRadius);
        }

        public void Update(GameTime gameTime){
            Shoot(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
            range.DrawCircle(rangeColor, spriteBatch, texture);
        }

        public void Shoot(GameTime gameTime){
            foreach(Enemy enemy in enemies){
                if (range.Intersects(enemy.Hitbox) && gameTime.TotalGameTime.TotalSeconds - timeWhenShoot > shootDelay){
                    Fire(gameTime, enemy);
                }
            }
        }

        public void Fire(GameTime gameTime, Enemy enemy){
            int count = 0;
            foreach(Bullet bullet in bullets){
                if (bullet.Target == enemy){
                    count++;
                    timeWhenShoot = gameTime.TotalGameTime.TotalSeconds;
                } 
                if (count >= 4) return;
            }
            bullets.Add(new Bullet
                (
                    new Rectangle((int)pos.X-5, (int)pos.Y-5, 10, 10), 
                    texture, 
                    pos, 
                    new Vector2(40, 40), 
                    enemy
                ));
        }
    }
}