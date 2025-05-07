using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Swordsman : Hero
    {
        private float swordRotaion;

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
            foreach(Sword sword in weapons){
                sword.Update(gameTime, pos, swordRotaion);
            }
            if (swordRotaion >= 360f){
                swordRotaion = 0;
            } else {
                swordRotaion += 0.1f;
            }
        }

        public void Slice(GameTime gameTime, Enemy enemy){
            int count = 0;
            foreach(Sword Sword in weapons){
                if (Sword.Target == enemy) count++;
                if (count >= 1) return;
            }
            weapons.Add(new Sword
                (
                    enemy, 
                    5, 
                    new Rectangle((int)pos.X, (int)pos.Y, 5, 5), 
                    texture, 
                    range,
                    0
                ));
            timeWhenShoot = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}