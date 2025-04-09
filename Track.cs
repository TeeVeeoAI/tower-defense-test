using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tower_defense__Priv
{
    public class Track
    {
        public Rectangle[] track { get; private set; }
        public Texture2D pixel { get; private set; }

        public Track(Rectangle[] track, Texture2D pixel){
            this.track = track;
            this.pixel = pixel;
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