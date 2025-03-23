using Rhythem.Songs;
using UnityEngine;
using UnityEngine.Events;

namespace Rhythem.Tracks
{
    public class NoteDeadzoneController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<ScorableNote> OnNoteMissed;
        private SongSession songSession;
        void Start()
        {
            songSession = SessionsManager.Instance.GetCurrentSession<SongSession>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Note")
            {
                ScorableNote scorable = other.gameObject.GetComponent<ScorableNote>();
                OnNoteMissed.Invoke(scorable);
                scorable.DisableNote();
                songSession.AddToEnergy(GameManager.Instance.scoreProfile.energyLossFromMiss);
                AudioManager.Instance.PlayOneShot(AudioManager.Instance.missSFXEvent, scorable.transform.position);
            }
        }
    }
}