using System.Security;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;

namespace tower_defense__Priv
{
    public class Circle
    {
        private Vector2 pos;
        private float radius;
        public Vector2 Pos{ get => pos; }
        public float Radius{ get => radius; }

        public Circle(Vector2 pos, float r){
            this.pos = pos;
            this.radius = r;
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
    }
}