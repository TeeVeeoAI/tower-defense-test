using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Green : Enemy
    {
        public Green(Vector2 pos, Texture2D texture, Track track, int currentWaypointIndex)
            : base(pos, texture, new Vector2(100, 100), track, Color.Green, 2, EnemyType.Green, currentWaypointIndex){

        }
        public Green(Vector2 pos, Texture2D texture, Track track)
            : base(pos, texture, new Vector2(100, 100), track, Color.Green, 2, EnemyType.Green, 0){

        }
    }
}