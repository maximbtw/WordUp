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
            UIHelper.DestroyAllChildren(columnContentTransform);

            Transform currentRow = null;

            for (int index = 0; index < items.Count; index++)
            {
                if (index % 3 == 0)
                {
                    currentRow = Instantiate(rowContentTransform, columnContentTransform);
                    UIHelper.DestroyAllChildren(currentRow);
                    currentRow.gameObject.name = $"ContentRow_{index / 3 + 1}";
                }

                LearnMenuGroupItem item = UIHelper.CreateInstantiate(itemPrefab, currentRow);
                item.gameObject.name = $"Item_{index + 1}";

                item.Data = items[index];
                var itemButton = item.GetComponent<Button>();
                itemButton.onClick.AddListener(() => onItemClick(item));
            }
        }
    }
}