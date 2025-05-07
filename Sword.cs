using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Sword : Weapon
    {
        private SwordsRect trueHitbox;
        private Circle range;

        public Sword(Enemy target, int damage, Rectangle hitbox, Texture2D texture, Circle range) 
            : base(target, damage, hitbox, texture){ 
            
            this.range = range;
            trueHitbox = new SwordsRect(new Vector2(hitbox.Location.X, hitbox.Location.Y), new Vector2(hitbox.Location.X - range.Radius, hitbox.Location.Y), range.Radius);

        }

        public void Update(GameTime gameTime){
            if (trueHitbox.Max.X > range.Pos.X + range.Radius){
                isAlive = false;
            }
        }

        public override void Kill(Enemy target){
            
        }
    }
}