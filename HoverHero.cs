using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class HoverHero : Hero
    {
        public HoverHero(Vector2 pos, Texture2D texture)
            : base(pos, texture, new Color(0, 0, 0, 250), new Color(0, 0, 0, 0), 30f, 100f, new List<Enemy>(), 0)
        {

        }
        public override void Attack(GameTime gameTime)
        {

        }
    }
}