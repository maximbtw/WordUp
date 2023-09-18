using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WordUp.Shared.StaticShared;

namespace WordUp.Views.LearnMenuView
{
    public class LearnMenuGroupContentView : UIBehaviour
    {
        [SerializeField] private LearnMenuGroupItem itemPrefab;
        [SerializeField] private Transform rowContentTransform;
        [SerializeField] private Transform columnContentTransform;

        public void CreateItems(List<LearnMenuGroupItemData> items, UnityAction<LearnMenuGroupItem> onItemClick)
        {
            UIHelper.DestroyAllChildren(rowContentTransform);
            
            Transform currentRow = rowContentTransform;
            currentRow.gameObject.name = "ContentRow_1";
            
            for (int i = 0; i < items.Count; i++)
            {
                LearnMenuGroupItemData itemData = items[i];

                if (i % 3 == 0 && i != 0)
                {
                    currentRow = Instantiate(rowContentTransform, columnContentTransform);
                    UIHelper.DestroyAllChildren(currentRow);
                    currentRow.gameObject.name = $"ContentRow_{(i / 3) + 1}";
                }

                LearnMenuGroupItem item = UIHelper.CreateInstantiate(itemPrefab, currentRow);
                item.gameObject.name = $"Item_{i+1}";
                
                item.Data = itemData;
                var itemButton = item.GetComponent<Button>();
                itemButton.onClick.AddListener(() => onItemClick(item));
            }
        }
    }
}