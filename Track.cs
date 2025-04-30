using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Track
    {
        public Rectangle[] track { get; private set; }
        public Texture2D pixel { get; private set; }
        public List<Vector2> Waypoints { get; private set; }

        public Track(Rectangle[] track, Texture2D pixel){
            this.track = track;
            this.pixel = pixel;

            Waypoints = new List<Vector2>();

            // Use corners of each rectangle to define path directionally
            for (int i = 0; i < track.Length; i++)
            {
                var rect = track[i];

                // Choose entry and exit corners: for top-down or left-right flow
                // Simple logic: from previous rect to this one
                if (i == 0){
                    Waypoints.Add(new Vector2(rect.Left+25, rect.Top+25));
                }
                else{
                    Vector2 last = Waypoints[Waypoints.Count - 1];
                    // If moving right
                    if (rect.Left > last.X) Waypoints.Add(new Vector2(rect.Left+25, last.Y));
                    // If moving down
                    if (rect.Top > last.Y) Waypoints.Add(new Vector2(last.X, rect.Top+25));
                    // If moving left
                    if (rect.Right < last.X) Waypoints.Add(new Vector2(rect.Right-25, last.Y));
                    // If moving up
                    if (rect.Bottom < last.Y) Waypoints.Add(new Vector2(last.X, rect.Bottom-25));
                }
            }

            // Add final exit corner
            Rectangle lastRect = track[track.Length - 1];
            Waypoints.Add(new Vector2(lastRect.Right-25, lastRect.Bottom-25));
        }

        public void UpdateTrack(GameTime gameTime){
            
        }

        public void DrawTrack(SpriteBatch spriteBatch){
            for(int i = 0; i < track.Length; i++){
                spriteBatch.Draw(pixel, track[i], new Color(20+i*i,20+i*i,20+i*i));
            }
        }
    }
}