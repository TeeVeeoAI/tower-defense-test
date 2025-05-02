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
        protected int currentWaypointIndex = 0;
        protected Vector2 velocity;
        protected Track track;
        protected int hp;
        protected EnemyType type;
        public enum EnemyType{
            Red = 1,
            Green,
            Blue
        }

        public Circle Hitbox { get => hitbox; }
        public Vector2 Pos { get => pos; }
        public Color Color { get => color; }
        public int HP { get => hp; }


        public Enemy(float radius, Vector2 pos, Texture2D texture, Vector2 velocity, Track track, Color color, int hp, EnemyType type){
            this.pos = pos;
            this.texture = texture;
            this.track = track;
            this.velocity = velocity;
            this.color = color;
            this.hp = hp;

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
            pos += direction * velocity * delta;

            hitbox.ChangePos(pos, "Enemy");
        }

        public void Draw(SpriteBatch spriteBatch){
            hitbox.DrawCircle(color, spriteBatch, texture);
        }

        public void Hit(int damage){
            hp -= damage;
            DownGrade();
        }

        protected void DownGrade(){
            switch (hp){
                case (int)EnemyType.Red:
                    type = EnemyType.Red;
                    break;
                case (int)EnemyType.Green:
                    type = EnemyType.Green;
                    break;
                case (int)EnemyType.Blue:
                    type = EnemyType.Blue;
                    break;
            }
        }
    }
}