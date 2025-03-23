using Rhythem.Play;
using Rhythem.Songs;
using UnityEngine;
using System.Collections;

namespace Rhythem.Tracks
{
    public class PlayHighwayController : HighwayController
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

        }

        protected override void Update()
        {
            base.Update();
            if (SessionsManager.Instance.GetCurrentSession<SongSession>().IsSongFailed())
            {
                DoSongFail();
            }
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
            player.leftHand.OnNoteHit.AddListener(DoNoteHit);
            player.rightHand.OnNoteHit.AddListener(DoNoteHit);
            _noteManager.deadzoneController.OnNoteMissed.AddListener(player.OnMissedNoteAction);
        }

        public override void UnsubscribeToSongSessionActions()
        {
            base.UnsubscribeToSongSessionActions();
            player.leftHand.OnNoteHit.RemoveListener(DoNoteHit);
            player.rightHand.OnNoteHit.RemoveListener(DoNoteHit);
            _noteManager.deadzoneController.OnNoteMissed.RemoveListener(player.OnMissedNoteAction);
        }

        public void DoNoteHit(DesiredHand hand, ScorableNote note, ScoreZone scoreZone)
        {
            Debug.Log(scoreZone.ToString());

            if (scoreZone == ScoreZone.Miss)
            {
                _noteManager.deadzoneController.OnNoteMissed.Invoke(note);
            }
        }

        public void DoSongWin()
        {
            OnSongWon.Invoke();
            StartCoroutine(SongWin());
        }

        public void DoSongFail()
        {
            OnSongFailed.Invoke();
            //StartCoroutine(SongFail());
        }

        public IEnumerator SongWin()
        {
            audioManager.PlayOneShot(audioManager.songCompleteSFXEvent, Camera.main.transform.position);
            //YOU WIN MENU
            yield return null;
        }

        public IEnumerator SongFail()
        {
            //slow the highway down over time and the music to match
            //fade to black, load song end scene
            audioManager.activeSong.setParameterByName("Song failed", 1f);
            audioManager.PlayOneShot(audioManager.songFailSFXEvent, player.head.transform.position);
            yield return new WaitForSeconds(failTime);
            //YOU FAILED MENU
        }
    }
}