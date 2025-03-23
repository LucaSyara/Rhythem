using Rhythem.Play;
using Rhythem.Songs;
using Rhythem.TrackEditor;

namespace Rhythem
{
    public class EditorSession : Session
    {
        public Beatmap editorBeatmap;
        public Song song;

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.player.SetInputModule<PlayerTrackEditInputModule>();
        }
    }
}