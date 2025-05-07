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

            weapons.Add(new Sword
                (
                    5, 
                    new Rectangle(0,0,0,0), 
                    texture, 
                    range,
                    0
                ));
        }

        public override void Attack(GameTime gameTime){
            foreach(Sword sword in weapons){
                sword.Update(gameTime, pos, swordRotaion);
            }
            if (swordRotaion >= 360f){
                swordRotaion = 0;
            } else {
                swordRotaion += 0.1f;
            }
        }
    }
}