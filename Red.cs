using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Red : Enemy
    {
        public Red(float radius, Vector2 pos, Texture2D texture, Track track)
            : base(radius, pos, texture, new Vector2(50, 50), track, Color.Red, 1, EnemyType.Red){

        }
    }
}