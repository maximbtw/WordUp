using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WordUp.Views.LearnMenuView
{
    public class LearnMenuGroupItem : UIBehaviour
    {
        [SerializeField] private LearnMenuGroupItemData data = LearnMenuGroupItemData.DefaultLearnMenuGroupItem;

        public UnityEvent<LearnMenuGroupItemData> onDataUpdated;

        public LearnMenuGroupItemData Data
        {
            get => data;
            set
            {
                data = value;
                onDataUpdated?.Invoke(data);
            }
        }
        

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            
            this.Data = data;
        }
#endif
    }
}