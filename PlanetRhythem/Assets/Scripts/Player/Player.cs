using Rhythem.Core;
using Rhythem.Songs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;

namespace Rhythem.Play
{
    public class Player : MonoBehaviour
    {
        [Title("Player-Specific References")]
        public TrackedPoseDriver head;
        public PlayerWand leftHand;
        public PlayerWand rightHand;
        [SerializeField]private InputModule _inputModule;

        [Title("Per-hand Color setup")]
        public Color leftHandColor;
        public Color rightHandColor;

        private SongSession songSession
        {
            get
            {
                return SessionsManager.Instance.GetCurrentSession<SongSession>();
            }
        }
        public UnityEvent<ScorableNote, ScoreZone> OnNoteHit;

        public void SetInputModule<T>() where T : InputModule
        {
            if (_inputModule != null)
            {
                Destroy(_inputModule);
            }
            _inputModule = gameObject.AddComponent<T>();
            _inputModule.head = head;
            _inputModule.rightHand = rightHand;
            _inputModule.leftHand = leftHand;
        }

        public T GetInputModule<T>() where T: InputModule
        {
                return (T)_inputModule;
        }

        public InputModule GetInputModule()
        {
            return _inputModule;
        }

        void Start()
        {
            if (SessionsManager.Instance.GetCurrentSession<SongSession>() != null)
            {
                DoSongStartPlayerSetup();
            }
            else if (SessionsManager.Instance.GetCurrentSession<EditorSession>() != null)
            {
                DoEditStartPlayerSetup();
            }
        }

        void Update()
        {

        }

        private void DoSongStartPlayerSetup()
        {
            leftHand.OnNoteHit.AddListener(OnHitNoteAction);
            rightHand.OnNoteHit.AddListener(OnHitNoteAction);
        }

        private void DoEditStartPlayerSetup()
        {

        }

        public void OnHitNoteAction(DesiredHand hand, ScorableNote note, ScoreZone zone)
        {
            if(note.noteType == NoteType.Obstacle)
            {
                songSession.AddToEnergy(GameManager.Instance.scoreProfile.EnergyLossFromObstacle);
                songSession.AddToScore(GameManager.Instance.scoreProfile.scoreNoteObstacle);
            }
            else if (note.noteType == NoteType.Note)
            {
                int scoreAdd = 0;
                if (hand == note.noteHand)
                {
                    songSession.AddToEnergy(GameManager.Instance.scoreProfile.energyRegain);

                    switch (zone)
                    {
                        case ScoreZone.Stellar:
                            scoreAdd = GameManager.Instance.scoreProfile.scoreNoteStellar;
                            break;
                        case ScoreZone.Great:
                            scoreAdd = GameManager.Instance.scoreProfile.scoreNoteGreat;
                            break;
                        case ScoreZone.Good:
                            scoreAdd = GameManager.Instance.scoreProfile.scoreNoteGood;
                            break;
                        case ScoreZone.Close:
                            scoreAdd = GameManager.Instance.scoreProfile.scoreNoteClose;
                            break;
                        case ScoreZone.Miss:
                            scoreAdd = GameManager.Instance.scoreProfile.scoreNoteMiss;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    songSession.AddToEnergy(-GameManager.Instance.scoreProfile.energyRegain);
                    scoreAdd /= 10;
                }
                songSession.AddToScore(scoreAdd);
            }
            note.DisableNote();
            if (songSession.IsSongFailed())
            {

            }
        }
   
        public void OnMissedNoteAction(ScorableNote note)
        {
            songSession.AddToEnergy(GameManager.Instance.scoreProfile.energyLossFromMiss);
            note.DisableNote();
        }
    }
}