using System.Collections.Generic;
using UnityEngine;
using Rhythem.Songs;

namespace Rhythem.TrackEditor
{
    /// <summary>
    /// Contains logic for converting user input into serializable JSON data and storing it in a JSON object
    /// </summary>
    public class InputRecorder : MonoBehaviour
    {
        private Measure _currentMeasure;
        private List<Measure> _createdMeasures = new();

        private int _currentBeat = 0;
        private int _currentNote = 0;

        private Beatmap _currentBeatmap;
        private Song _currentSong;

        void Start()
        {
            var gm = GameManager.Instance;
            _currentBeatmap = gm.CurrentBeatmap;
            _currentSong = _currentBeatmap.DeserializeSongData();
            SessionsManager.Instance.GetCurrentSession<EditorSession>();
            SubscribeToPlayerInput();
        }

        private void OnDestroy()
        {
            UnsubscribeToPlayerInput();
        }

        private void OnDisable()
        {
            UnsubscribeToPlayerInput();
        }

        void Update()
        {

        }

        private void SubscribeToPlayerInput()
        {
            GameManager.Instance.player.leftHand.OnNoteCreated.AddListener(OnNoteCreatedAction);
            GameManager.Instance.player.rightHand.OnNoteCreated.AddListener(OnNoteCreatedAction);
        }

        private void UnsubscribeToPlayerInput()
        {
            GameManager.Instance.player.leftHand.OnNoteCreated.RemoveListener(OnNoteCreatedAction);
            GameManager.Instance.player.rightHand.OnNoteCreated.RemoveListener(OnNoteCreatedAction);
        }

        public void OnNoteCreatedAction(ScorableNote newNote, Vector2 handPosition)
        {
            /* what does this class need to do?
            *-intake player inputs and coordinates of player controllers
            *
            * - track the current measure, beat, and subdivision of the track
            * - UTILITY: determine which measure and beat we are on by using the current position in the song
            * - create instance of a Song
            * - create a number of measures equal to the beatmap's listing
            * - create a number of beats equal to the beatmap's listing
            * - convert inputs and coordinates into Notes
            * - Assign notes to the correct positions in the correct beat
            * - Determine which subdivision is the correct one based on the time of the input and that time's distance to the nearest beat
            */

        }

        private void OnMeasureComplete(bool songFinished = false)
        {
            _createdMeasures.Add(_currentMeasure);
            if (!songFinished)
            {
                _currentMeasure = new Measure();
            }
            else
            {
                _currentMeasure = null;
                OnSongComplete();
            }

        }

        private void OnSongComplete()
        {

        }
    }
}