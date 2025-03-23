using UnityEngine;
using System.Collections.Generic;

namespace Rhythem.Songs
{
    [System.Serializable]
    public class Measure
    {
        [SerializeField] public List<Beat> beats = new();
        public Beat GetBeatAt(int index)
        {
            if (beats.Count > index)
            {
                return beats[index];
            }
            return null;
        }
    }

}