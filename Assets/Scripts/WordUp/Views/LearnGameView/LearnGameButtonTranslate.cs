using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordUp.Views.LearnGameView
{
    public class LearnGameButtonTranslate : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        
        private LearnGameCard _card;
        private bool _anyTimeClicked;
        
        public void OnButtonClick()
        {
            if (!_anyTimeClicked)
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(true);
            }

            _anyTimeClicked = true;
        }

        private void Awake()
        {
            _card ??= GetComponentInParent<LearnGameCard>();
            _card.onDataSet.AddListener(OnSetCardData);
        }

        private void OnSetCardData(LearnGameCardData data)
        {
            text.text = data.GetTargetName;
            
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
        }
    }
}