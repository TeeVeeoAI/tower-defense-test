using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace tower_defense__Priv
{
    public class SwordsRect
    {
        private Vector2 rotatePos, max;
        private float length;

        public Vector2 Max{ get => max; }

        public SwordsRect(Vector2 rotatePos, Vector2 max, float length){
            this.rotatePos = rotatePos;
            this.max = max;
            this.length = length;
        }

        public void Update(){
            Moving();
        }

        public void Moving(){
            max.X++;
            float diff = Vector2.Distance(rotatePos, max);
            if (diff < length){
                max.Y++;
            }
            if (diff > length){
                max.Y--;
            }
        }
    }
}