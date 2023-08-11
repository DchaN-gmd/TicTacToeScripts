using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class FinishPanelView : MonoBehaviour
    {
        [SerializeField] protected FinishPanel _finishPanel;

        protected virtual void OnEnable()
        {
            _finishPanel.Showing += Show;
            _finishPanel.SettingDraw += OnSettingDraw;
        }

        protected virtual void OnDisable()
        {
            _finishPanel.Showing -= Show;
            _finishPanel.SettingDraw -= OnSettingDraw;
        }

        protected abstract void Show();

        protected abstract void OnSettingDraw();
    }
}