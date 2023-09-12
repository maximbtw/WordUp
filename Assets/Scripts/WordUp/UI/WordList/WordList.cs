using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WordUp.Service.Contracts.Word;
using WordUp.Shared.StaticShared;

namespace WordUp.UI.WordList
{
    public class WordList : UIBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private WordListItem contentItemPrefab;
        [Space]
        [SerializeField] private WordListSortingSettings sortingSettings;
        
        public UnityEvent<WordDto> onItemClick;
        
        private readonly Dictionary<Guid, WordListItem> _items = new();
        
        public WordListSortingSettings SortingSettings
        {
            get => sortingSettings;
            set
            {
                sortingSettings = value;
                Refresh();
            }
        }

        public void LoadItems(List<WordDto> items)
        {
            ClearAll();

            AddMany(items);
        }
        
        public void Delete(Guid itemGuid)
        {
            WordListItem deletedItem = _items[itemGuid];

            Destroy(deletedItem.gameObject);
            _items.Remove(itemGuid);

            Refresh();
        }

        public void CreateOrUpdateItem(WordDto newItem)
        {
            if (!_items.TryGetValue(newItem.Guid, out WordListItem updatedItem))
            {
                Add(newItem, sortItems: true);
            }
            else
            {
                updatedItem.Word = newItem;
            
                Refresh();   
            }
        }

        public void AddMany(List<WordDto> items)
        {
            items.ForEach(x => Add(x, sortItems: false));
            Refresh();
        }

        public void Add(WordDto item, bool sortItems = true)
        {
            WordListItem newItem = UIHelper.CreateInstantiate(contentItemPrefab, content);

            newItem.BindClick(() => onItemClick?.Invoke(item));

            newItem.UpdateBindings(item);

            _items.Add(item.Guid, newItem);

            if (sortItems)
            {
                Refresh();
            }
        }
        
        public void ClearAll()
        {
            _items.Clear();

            foreach (WordListItem item in GetComponentsInChildren<WordListItem>())
            {
                Destroy(item.gameObject);
            }
        }

        private void Refresh()
        {
            var visibleItems = _items
                .Where(WordMatched)
                .OrderBy(x => x.Value.Word.NameEn)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (KeyValuePair<Guid, WordListItem> contentViewDictionaryWordItem in _items)
            {
                WordListItem prefab = contentViewDictionaryWordItem.Value;
                
                prefab.gameObject.SetActive(false);
            }
            
            int index = 0;
            foreach (KeyValuePair<Guid, WordListItem> item in visibleItems)
            {
                WordListItem prefab = item.Value;

                prefab.transform.gameObject.SetActive(true);
                prefab.transform.SetSiblingIndex(index);

                index++;
            }
        }

        private bool WordMatched(KeyValuePair<Guid, WordListItem> keyValuePair)
        {
            WordDto word = keyValuePair.Value.Word;

            return (!sortingSettings.showOnlyLearned || word.IsLearned) &&
                   (!sortingSettings.showOnlyHard || word.IsHard) &&
                   (sortingSettings.text == null || word.ToString().Contains(sortingSettings.text));
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            this.SortingSettings = sortingSettings;
        }
#endif
    }
}