using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Swordsman : Hero
    {
        public Swordsman(Vector2 pos, Texture2D texture, Color color, Color rangeColor, List<Enemy> enemies) 
            : base(pos, texture, color, rangeColor, 30f, 100f, enemies, 5){
        }

        public override void Attack(GameTime gameTime){
            foreach(Enemy enemy in enemies){
                if (range.Intersects(enemy.Hitbox) && gameTime.TotalGameTime.TotalSeconds - timeWhenShoot > shootDelay){
                    Slice(gameTime, enemy);
                    break;
                }
            }
        }
        public void Slice(GameTime gameTime, Enemy enemy){
            int count = 0;
            foreach(Sword Sword in weapons){
                if (Sword.Target == enemy) count++;
                if (count >= 4) return;
            }
            timeWhenShoot = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}