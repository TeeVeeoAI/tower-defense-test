using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public abstract class Enemy
    {
        protected Circle hitbox;
        protected Vector2 pos;
        protected Color color;
        protected Texture2D texture;
        protected int currentWaypointIndex;
        protected float progress;
        protected Vector2 velocity;
        protected Track track;
        protected int hp;
        protected EnemyType type;
        protected bool attEnd;

        public EnemyType EnemyType      { get => type; }
        public Circle Hitbox            { get => hitbox; }
        public Vector2 Pos              { get => pos; }
        public Color Color              { get => color; }
        public int HP                   { get => hp; }
        public int CurrentWaypointIndex { get => currentWaypointIndex; }
        public float Progress           { get => progress; }
        public bool AttEnd              { get => attEnd; }


        public Enemy(Vector2 pos, Texture2D texture, Vector2 velocity, Track track, Color color, int hp, EnemyType type, int currentWaypointIndex)
        {
            this.pos = pos;
            this.texture = texture;
            this.track = track;
            this.velocity = velocity;
            this.color = color;
            this.hp = hp;
            this.type = type;
            this.currentWaypointIndex = currentWaypointIndex;
            this.progress = currentWaypointIndex / track.Waypoints.Count-1;
            this.attEnd = false;

            this.hitbox = new Circle(pos, 20);
        }

        public void Update(GameTime gameTime){

            progress = currentWaypointIndex / track.Waypoints.Count-1;

            if (currentWaypointIndex >= track.Waypoints.Count)
            {
                attEnd = true;
                return;
            }
            if (pos.X >= 1920 + hitbox.Radius+10 || pos.Y >= 1080 + hitbox.Radius+10 || pos.X <= 0 - hitbox.Radius-10 || pos.Y <= 0 - hitbox.Radius-10)
            {
                attEnd = true;
                return;
            }

            int totalWaypoints = track.Waypoints.Count - 1;

            float segmentProgress = CalculateSegmentProgress();

            float waypointFraction = (float)CurrentWaypointIndex / totalWaypoints;
            progress = waypointFraction + (segmentProgress / totalWaypoints);

            progress = MathHelper.Clamp(Progress, 0f, 1f);

            Vector2 target = track.Waypoints[currentWaypointIndex];
            Vector2 direction = target - pos;

            if (direction.Length() < 1f){
                currentWaypointIndex++;
                return;
            }

            direction.Normalize();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            pos += direction * velocity * delta;

            hitbox.ChangePos(pos, "Enemy");
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
        }

        public void Hit(int damage){
            hp -= damage;
        }
        
        private float CalculateSegmentProgress(){
            if (CurrentWaypointIndex >= track.Waypoints.Count - 1) return 1f;

            Vector2 startPos = track.Waypoints[CurrentWaypointIndex];
            Vector2 endPos = track.Waypoints[CurrentWaypointIndex + 1];

            float segmentLength = Vector2.Distance(startPos, endPos);
            float distanceCovered = Vector2.Distance(startPos, pos);

            // Avoid division by zero
            return segmentLength > 0 ? MathHelper.Clamp(distanceCovered / segmentLength, 0f, 1f) : 0f;
        }
    }
}