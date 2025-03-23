using UnityEngine;
using System.Collections.Generic;

namespace Rhythem.Songs
{
    [System.Serializable]
    public class Beat
    {
        [SerializeField]public List<Note> notes = new();
        public Note GetNoteAt(int index)
        {
            if (notes.Count > index)
            {
                return notes[index];
            }
            return null;
        }
    }
}