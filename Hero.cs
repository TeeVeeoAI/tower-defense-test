using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public abstract class Hero
    {
        protected Circle hitbox;
        protected Circle range;
        protected List<Weapon> weapons = new List<Weapon>();
        protected Vector2 pos;
        protected Color color;
        protected Color rangeColor;
        protected Texture2D texture;
        protected List<Enemy> enemies;
        protected double shootDelay;
        protected double timeWhenShoot = 0;

        public Circle Hitbox { get => hitbox; }
        public Circle Range { get => range; }
        public List<Weapon> Weapons { get => weapons; }
        
        public Hero(Vector2 pos, Texture2D texture, Color color, Color rangeColor, float size, float rangeRadius, List<Enemy> enemies, double shootDelay){
            this.pos = pos;
            this.texture = texture;
            this.color = color;
            this.rangeColor = rangeColor;
            this.enemies = enemies;
            this.shootDelay = shootDelay;

            this.hitbox = new Circle(this.pos, size);
            this.range = new Circle(this.pos, rangeRadius);
        }

        public void Update(GameTime gameTime){
            Attack(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
            range.DrawCircle(rangeColor, spriteBatch, texture);
        }

        public abstract void Attack(GameTime gameTime);
    }
}