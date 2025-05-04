using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Sword : Weapon
    {
        public Sword(Enemy target, int damage, Rectangle hitbox, Texture2D texture) 
            : base(target, damage, hitbox, texture){ 
            
            
        }

        public override void Kill(Enemy target){
            
        }
    }
}