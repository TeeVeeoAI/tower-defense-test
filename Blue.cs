using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Blue : Enemy
    {
        public Blue(float radius, Vector2 pos, Texture2D texture, Track track, int currentWaypointIndex)
            : base(radius, pos, texture, new Vector2(200, 200), track, Color.Blue, 4, EnemyType.Blue, currentWaypointIndex){

        }
        public Blue(float radius, Vector2 pos, Texture2D texture, Track track)
            : base(radius, pos, texture, new Vector2(200, 200), track, Color.Blue, 4, EnemyType.Blue, 0){

        }
    }
}