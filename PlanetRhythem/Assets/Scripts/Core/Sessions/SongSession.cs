using Rhythem.Play;
using Rhythem.Songs;
using Rhythem.TrackEditor;
using UnityEngine;

namespace Rhythem
{
    public class SongSession : Session
    {
        public Beatmap beatmap;
        public Song song;

        public int score { get; private set; }
        public int energy { get; private set; }

        public int notesStellar;
        public int notesGreat;
        public int notesGood;
        public int notesClose;
        public int notesMiss;

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.player.SetInputModule<PlayerSongPlayInputModule>();
            beatmap = GameManager.Instance.CurrentBeatmap;
            song = beatmap.DeserializeSongData();
            score = 0;
            energy = GameManager.Instance.scoreProfile.energyStartValue;
            notesStellar = 0;
            notesGreat = 0;
            notesGood = 0;
            notesClose = 0;
            notesMiss = 0;
        }

        public override void EndSession()
        {
            base.EndSession();
            //store score in sme persistent data
        }

        public void AddToScore(int points)
        {
            score += points;
            if (score < 0)
            {
                score = 0;
            }
            if (GameManager.Instance.showDebugLogs)
            {
                Debug.Log($"points {points}: {score}");
            }
        }

        public void AddToEnergy(int changeAmount)
        {
            energy += changeAmount;
            Mathf.Clamp(energy, 0, GameManager.Instance.scoreProfile.energyStartValue);
            if (GameManager.Instance.showDebugLogs)
            {
                Debug.Log($"Energy {changeAmount}: {energy}");
            }
        }

        public bool IsSongFailed()
        {
            return energy <= 0;
        }
    }
}