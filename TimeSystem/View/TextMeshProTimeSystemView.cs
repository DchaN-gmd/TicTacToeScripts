using TMPro;
using System;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems.View
{
    public abstract class TextMeshProTimeSystemView<TUpdateable> : TimeView<TUpdateable> where TUpdateable : Component, IUpdateable
    {
        public enum FormatType
        {
            Seconds,
            SecondsAndMinutes,
            SecondsAndMinuteAndHours,
        }

        #region Inspector fields
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] protected FormatType _format;
        #endregion

        public FormatType TypeOfFormat => _format;

        protected sealed override void UpdateTime(float time) => _text.text = Format(time);

        private string Format(float seconds)
        {
            switch (_format)
            {
                case FormatType.Seconds: return $"{Mathf.CeilToInt(seconds)}";

                case FormatType.SecondsAndMinutes:
                    {
                        var minutes = (int)(seconds / 60);
                        seconds = Mathf.CeilToInt(seconds - minutes * 60);

                        var text = minutes < 10 ? $"0{minutes}:" : $"{minutes}:";
                        text += seconds < 10 ? $"0{seconds}" : $"{seconds}";
                        
                        return text;
                    }
                    
                case FormatType.SecondsAndMinuteAndHours:
                    {
                        var hours = (int)(seconds / 60 / 60);
                        seconds -= hours * 60 * 60;
                        
                        var minutes = (int)(seconds / 60);
                        seconds = Mathf.CeilToInt(seconds - minutes * 60);
                        
                        var text = hours < 10 ? $"0{hours}:" : $"{hours}:";
                        text += minutes < 10 ? $"0{minutes}:" : $"{minutes}:";
                        text += seconds < 10 ? $"0{seconds}" : $"{seconds}";

                        return text;
                    }

                default: throw new ArgumentOutOfRangeException($"{_format}");
            }
        }
    }
}
