
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Hero
    {
        public Rectangle hitbox { get; private set; }
        public Circle range { get; private set; }
        public Vector2 pos { get; private set; }
        public Texture2D texture { get; private set; }
        public Color color { get; private set; }
        private int currentWaypointIndex = 0;
        private float speed = 100f;
        private Track track;
        
        public Hero(Rectangle hitbox, Vector2 pos, Texture2D texture, Track track, Color color){
            this.hitbox = hitbox;
            this.pos = pos;
            this.texture = texture;
            this.track = track;
            this.color = color;
        }
    }

}