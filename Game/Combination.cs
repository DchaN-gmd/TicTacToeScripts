using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [Serializable]
    public struct Combination<T> where T : MonoBehaviour
    {
        [SerializeField] private List<TapeZone<T>> _zones;
        [SerializeField] private Line _line;

        public List<TapeZone<T>> GetZones()
        {
            return _zones;
        }

        public void FillLine(float speed)
        {
            _line.Show(speed);
        }

        public void ResetLine()
        {
            _line.ResetLine();
        }
    }
}
