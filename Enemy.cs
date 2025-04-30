using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Enemy
    {
        private Circle hitbox;
        private Vector2 pos;
        private Color color;
        private Texture2D texture;
        private int currentWaypointIndex = 0;
        private float speed = 100f;
        private Track track;

        public Circle Hitbox { get => hitbox; }
        public Vector2 Pos { get => pos; }
        public Color Color { get => color; }


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