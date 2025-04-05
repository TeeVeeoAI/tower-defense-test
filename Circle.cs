using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Circle
    {
        private Vector2 pos;
        private float radius;
        public Vector2 Pos{ get => pos; }
        public float Radius{ get => radius; }
        public void ChangePos(Vector2 pos, string pas){ 
            if (pas == "Enemy")
                this.pos = pos;
        }

        public Circle(Vector2 pos, float radius){
            this.pos = pos;
            this.radius = radius;
        }

        public bool ContainsPoint(Point point)
        {
            Vector2 temp = new Vector2(point.X - pos.X, point.Y - pos.Y);
            return temp.Length() <= radius;
        }


        // Intersect for just Recatangle
        public bool Intersects(Rectangle rectangle){

            Point[] corners =
            [
                new Point(rectangle.Left, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            ];

            foreach (Point corner in corners){
                if (ContainsPoint(corner))
                    return true;
            }

            if (pos.X - radius > rectangle.Right || pos.X + radius < rectangle.Left)
                return false;

            if (pos.Y - radius > rectangle.Bottom || pos.Y + radius < rectangle.Top)
                return false;

            return true;
        }

        // Intersect for just Circle
        public bool Intersects(Circle circle){
            
            return Vector2.Distance(circle.pos, pos) < radius + circle.Radius;

        }

        public void DrawCircle(Color color, SpriteBatch _spriteBatch, Texture2D texture){
            int r = (int)radius;
            int cx = (int)pos.X;
            int cy = (int)pos.Y;

            for (int x = -r; x <= r; x++)
            {
                for (int y = -r; y <= r; y++)
                {
                    if (x * x + y * y <= r * r) // Check if inside circle
                    {
                        _spriteBatch.Draw(texture, new Vector2(cx + x, cy + y), color);
                    }
                }
            }
        }
    }
}