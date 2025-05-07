using Microsoft.Xna.Framework;

namespace tower_defense__Priv
{
    public class RotatebleHitbox
    {
        public Vector2[] Corners { get; private set; } = new Vector2[4];

        public RotatebleHitbox(Vector2 position, float width, float height, float rotation, Vector2 pivot){
            Update(position, width, height, rotation, pivot);
        }

        public void Update(Vector2 position, float width, float height, float rotation, Vector2 pivot){
            // The pivot point is now the bottom center of the hitbox

            Vector2 topLeft = new Vector2(-width / 2, -height);
            Vector2 topRight = new Vector2(width / 2, -height);
            Vector2 bottomRight = new Vector2(width / 2, 0);
            Vector2 bottomLeft = new Vector2(-width / 2, 0);

            // Adjust position so the pivot is at the specified position
            Vector2 adjustedPosition = position;

            // Apply rotation matrix
            Matrix rotationMatrix = Matrix.CreateRotationZ(rotation);

            Corners[0] = Vector2.Transform(topLeft, rotationMatrix) + adjustedPosition;
            Corners[1] = Vector2.Transform(topRight, rotationMatrix) + adjustedPosition;
            Corners[2] = Vector2.Transform(bottomRight, rotationMatrix) + adjustedPosition;
            Corners[3] = Vector2.Transform(bottomLeft, rotationMatrix) + adjustedPosition;
        }
    }
}