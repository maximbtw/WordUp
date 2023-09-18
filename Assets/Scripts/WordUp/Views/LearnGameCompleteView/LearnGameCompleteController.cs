using System;
using TMPro;
using UnityEngine;

namespace WordUp.Views.LearnGameCompleteView
{
    public class LearnGameCompleteController : SceneControllerBase
    {
        [SerializeField] private TextMeshProUGUI textResult;

        private bool _again;

        public void OnAgainButtonClick()
        {
            _again = true;
            
            UnloadScene((int)SceneNumber.LearnGameCompleteView);
        }
        
        public void OnExitButtonClick()
        {
            _again = false;
            
            UnloadScene((int)SceneNumber.LearnGameCompleteView);
        }

        protected override object GetDataFromScene()
        {
            return _again;
        }

        protected override void LoadDataFromScene(object data)
        {
            if (data is LearnGameCompleteData completeData)
            {
                textResult.text = $"{completeData.successCount}/{completeData.maxCount}";
                
                return;
            }

            throw new ArgumentException(nameof(data));
        }
    }
}