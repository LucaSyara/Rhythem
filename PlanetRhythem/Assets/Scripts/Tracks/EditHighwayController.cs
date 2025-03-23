using UnityEngine;

namespace Rhythem.Tracks
{
    public class EditHighwayController : HighwayController
    {
        [SerializeField] private float timePerSong = 0f;
        [SerializeField] private float timePerMeasure = 0f;
        [SerializeField] private float timePerBeat = 0f;
        [SerializeField] private float timePerNote = 0f;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            timePerSong = _beatmap.audioFile.length - _beatmap.silenceAtStartOfTrack;
            timePerMeasure = timePerSong / _beatmap.bPM / 60f;
            timePerBeat = timePerMeasure / _beatmap.beatsPerMeasure;
            timePerNote = timePerBeat / _beatmap.subdivisionsPerBeat;
        }

        protected override void Update()
        {
            base.Update();

        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void SubscribeToSongSessionActions()
        {
            base.SubscribeToSongSessionActions();
        }

        public override void UnsubscribeToSongSessionActions()
        {
            base.UnsubscribeToSongSessionActions();
        }


    }
}