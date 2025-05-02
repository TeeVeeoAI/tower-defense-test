using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Green : Enemy
    {
        public Green(float radius, Vector2 pos, Texture2D texture, Track track)
            : base(radius, pos, texture, new Vector2(100, 100), track, Color.Green, 2, EnemyType.Green){

        }
    }
}