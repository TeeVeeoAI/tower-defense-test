using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Black : Enemy
    {
        public Black(Vector2 pos, Texture2D texture, Track track, int currentWaypointIndex)
            : base(pos, texture, new Vector2(500, 500), track, Color.Gray, 10, EnemyType.Black , currentWaypointIndex){

        }
        public Black(Vector2 pos, Texture2D texture, Track track)
            : base(pos, texture, new Vector2(500, 500), track, Color.Gray, 10, EnemyType.Black, 0){

        }
    }
}