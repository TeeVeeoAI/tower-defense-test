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
            trueHitbox = new SwordsRect(new Vector2(hitbox.Location.X, hitbox.Location.Y), new Vector2())

            float circle = MathHelper.Pi * 2;
            rotationAngle %= circle;
        }

        public override void Kill(Enemy target){
            
        }
    }
}