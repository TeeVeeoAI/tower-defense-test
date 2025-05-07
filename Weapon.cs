using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public abstract class Weapon
    {
        protected Texture2D texture;
        protected Rectangle hitbox; 
        protected Enemy target;
        protected int damage;
        protected bool isAlive = true;

        public Enemy Target {get => target; }
        public int Damage { get => damage; }
        public bool IsAlive { get => isAlive; }
        public Rectangle Hitbox { get => hitbox; }

        public Weapon(Enemy target, int damage, Rectangle hitbox, Texture2D texture){
            this.target = target; 
            this.damage = damage;
            this.hitbox = hitbox; 
            this.texture = texture;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Kill(Enemy target);
    }
}