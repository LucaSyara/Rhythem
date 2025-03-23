using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Rhythem.Songs;
using Rhythem.Play;
using Rhythem.TrackEditor;
using System.Collections;

namespace Rhythem.Tracks
{
    public abstract class HighwayController : MonoBehaviour
    {
        public static HighwayController s;

        [Title("Notes Setup")]
        public GameObject notePrefab;
        public int activeNoteLimit = 200;
        public int measuresPerRotation = 8;
        public float failTime = 1.5f;
        public Transform ringPivot;
        public Transform notesSpawnStart;

        public UnityEvent OnSongStarted;
        public UnityEvent OnSongEnded;
        public UnityEvent OnSongWon;
        public UnityEvent OnSongFailed;
        protected IEnumerator _songStartAsync;

        protected Player player;
        protected NoteManager _noteManager;
        protected AudioManager audioManager;
        protected Beatmap _beatmap;
        protected Song _song;


        protected virtual void Awake()
        {
            audioManager = AudioManager.Instance;
            //songSession = SessionsManager.Instance.GetCurrentSession<SongSession>();

            if(s != null)
            {
                Destroy(s);
                s = null;
            }
            s = this;
        }

        protected virtual void Start()
        {
            if (_noteManager == null)
            {
                _noteManager = GetComponentInChildren<NoteManager>();
            }
            SetupSong();

            player = GameManager.Instance.player;

            SubscribeToSongSessionActions();
            
        }
        public virtual void SubscribeToSongSessionActions()
        {

        }

        public virtual void UnsubscribeToSongSessionActions()
        {

        }

        protected virtual void OnDisable()
        {
            UnsubscribeToSongSessionActions();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeToSongSessionActions();
        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            UpdateRing();
        }

        public virtual void SetupSong()
        {
            var sm = SessionsManager.Instance.GetCurrentSession<SongSession>();
            _beatmap = sm.beatmap;
            _song = sm.song;

            _noteManager.song = _song;
            _noteManager.Cleanup();
            _noteManager.InitializeNoteList(notePrefab, ringPivot, notesSpawnStart, activeNoteLimit);
            _songStartAsync = DoSongStartWithDelay(_beatmap.audioFile);
            //Debug.Log("Trying to start async function...");
            StartCoroutine(_songStartAsync);
        }

        protected virtual void UpdateRing()
        {
            if (ringPivot == null && (ringPivot = GameObject.Find("Ring").transform) == null)
            {
                return;
            }
            ringPivot.Rotate(new Vector3(0, (_song.bpm / 60f) * 360f / _song.beatsPerMeasure / measuresPerRotation * Time.fixedDeltaTime, 0));
        }

        protected virtual IEnumerator DoSongStartWithDelay(AudioClip songFile)
        {
            if (songFile == null)
            {
                Debug.LogError("NO SONG TO PLAY");
                yield return null;
            }

            //FMOD VERSION
            Debug.Log("Waiting to start...");
            yield return new WaitForSeconds(_song.startWaitTime); //this doesn't belong here, we need to pass the NoteManager the song.startWaitTime in a new coroutine that handles ALL measures' note spawning so we can delay that process by the appropriate amount, instead of handling each measure as a coroutine
            _noteManager.InitializeMeasures();
            var waitTime = _song.bpm / 60f * (measuresPerRotation / 4f);
            yield return new WaitForSeconds(waitTime);
            Debug.Log("attempting to start song...");
            audioManager.StartSong(songFile);
            OnSongStarted.Invoke();
        }
    }
}

