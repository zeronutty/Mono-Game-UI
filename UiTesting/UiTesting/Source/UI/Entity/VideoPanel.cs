using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace UiTesting.Source
{
    public class VideoPanel : Entity
    {
        Video video;
        VideoPlayer VideoPlayer;

        Sprite PreviousTexture;

        public VideoPanel(VideoPanelProps videoPanelProps) : base(videoPanelProps)
        {
            video = videoPanelProps.Video;
            VideoPlayer = new VideoPlayer();

            PreviousTexture = videoPanelProps.empty;
        }

        public void PlayVideo()
        {
            if(VideoPlayer.State == MediaState.Stopped)
            {
                VideoPlayer.IsLooped = false;
                VideoPlayer.Play(video);
            }         
        }

        public override void DrawEntity(SpriteBatch spriteBatch, bool? BaseDraw = null)
        {
            //if (VideoPlayer.State != MediaState.Stopped)
            //{
            //    BackgroundTexture = VideoPlayer.GetTexture();
            //}

           
            base.DrawEntity(spriteBatch);
        }
    }
}
