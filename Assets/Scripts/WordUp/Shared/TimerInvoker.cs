using System;
using System.Collections.Generic;
using UnityEngine;

namespace WordUp.Shared
{
    public class TimerInvoker
    {
        private readonly List<TimeAction> _actions = new();
        
        private float _time;
        private int _previousCountSeconds;

        private int TotalMinutes => Mathf.FloorToInt(_time / 60);
        private int TotalSeconds => Mathf.FloorToInt(_time % 60) + TotalMinutes * 60;
        
        public void DisplayTime()
        {
            Debug.Log(TotalSeconds);
        }

        public void BindAction(float seconds, Action action)
        {
            var item = new TimeAction
            {
                Seconds = seconds,
                Action = action
            };
            
            _actions.Add(item);
        }

        public void Update()
        {
            _time += Time.deltaTime;

            if (_previousCountSeconds != TotalSeconds)
            {
                _previousCountSeconds = TotalSeconds;

                TryInvokeActions(_previousCountSeconds);
            }
        }

        private void TryInvokeActions(int secondLeft)
        {
            foreach (TimeAction item in _actions)
            {
                if (secondLeft % item.Seconds == 0)
                {
                    item.Action();
                }
            }
        }

        private class TimeAction
        {
            public float Seconds { get; set; }
            
            public Action Action { get; set; }
        }
    }
}