using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Sword : Weapon
    {
        private RotatebleHitbox trueHitbox;
        private float width = 50f;
        private Circle range;

        public Sword(Enemy target, int damage, Rectangle hitbox, Texture2D texture, Circle range, float rotation) 
            : base(target, damage, hitbox, texture){ 
            
            this.range = range;
            trueHitbox = new RotatebleHitbox(new Vector2(hitbox.X+25, hitbox.Y-10), width, range.Radius, rotation, new Vector2(width / 2, range.Radius));

        }

        public void Update(GameTime gameTime, Vector2 position, float rotation){
            trueHitbox.Update(position, width, range.Radius, rotation, new Vector2(0, range.Radius));
        }

        public override void Update(GameTime gameTime){

        }

        public override void Draw(SpriteBatch spriteBatch){
            DrawHitbox(spriteBatch);
        }

        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < trueHitbox.Corners.Length; i++)
            {
                Vector2 start = trueHitbox.Corners[i];
                Vector2 end = trueHitbox.Corners[(i + 1) % trueHitbox.Corners.Length];
                DrawLine(spriteBatch, start, end, Color.Red);
            }
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(texture,
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1),
                null,
                color,
                angle,
                Vector2.Zero,
                SpriteEffects.None,
                0);
        }

        public override void Kill(Enemy target){
            damage -= target.HP + damage;

            if (damage <= 0) isAlive = false;
        }
    }
}