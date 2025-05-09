using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Gunner : Hero
    {
        public Gunner(Vector2 pos, Texture2D texture, Color color, Color rangeColor, List<Enemy> enemies)
            : base(pos, texture, color, rangeColor, 20f, 400f, enemies, 1){
            
        }

        public override void Attack(GameTime gameTime){
            foreach(Enemy enemy in enemies){
                if (range.Intersects(enemy.Hitbox) && gameTime.TotalGameTime.TotalSeconds - timeWhenShoot > shootDelay){
                    Fire(gameTime, enemy);
                    break;
                }
            }
        }


        public void Fire(GameTime gameTime, Enemy enemy){
            int count = 0;
            foreach(Bullet bullet in weapons){
                if (bullet.Target == enemy) count++;
                if (count >= 4) return;
            }
            weapons.Add(new Bullet
                (
                    new Rectangle((int)pos.X-5, (int)pos.Y-5, 10, 10), 
                    texture, 
                    pos, 
                    new Vector2(1500, 1500), // VELOCITY (x, y)
                    enemy,
                    2
                ));
            timeWhenShoot = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}