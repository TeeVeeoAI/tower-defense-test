using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Red : Enemy
    {
        public Red(Vector2 pos, Texture2D texture, Track track, int currentWaypointIndex)
            : base(pos, texture, new Vector2(50, 50), track, Color.Red, 1, EnemyType.Red, currentWaypointIndex){

        }
        public Red(Vector2 pos, Texture2D texture, Track track)
            : base(pos, texture, new Vector2(50, 50), track, Color.Red, 1, EnemyType.Red, 0){

        }
    }
}