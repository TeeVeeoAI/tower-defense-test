using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Enemy
    {
        public Circle hitbox { get; private set; }
        public Vector2 pos { get; private set; }
        public Texture2D texture { get; private set; }
        public Color color { get; private set; }
        private int currentWaypointIndex = 0;
        private float speed = 100f;
        private Track track;

        public Enemy(float radius, Vector2 pos, Texture2D texture, Vector2 velocity, Track track, Color color){
            this.pos = pos;
            this.texture = texture;
            this.track = track;
            this.color = color;

            this.hitbox = new Circle(pos, radius);
        }

        public void Update(GameTime gameTime){

            if (currentWaypointIndex >= track.Waypoints.Count)
                return;

            Vector2 target = track.Waypoints[currentWaypointIndex];
            Vector2 direction = target - pos;

            if (direction.Length() < 1f){
                currentWaypointIndex++;
                return;
            }

            direction.Normalize();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            pos += direction * speed * delta;

            hitbox.ChangePos(pos, this.GetType().ToString().Remove(0, 20));
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
        }
    }
}