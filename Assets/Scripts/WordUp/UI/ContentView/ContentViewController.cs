using UnityEngine;
using WordUp.Data;

namespace WordUp.UI.ContentView
{
    public class ContentViewController : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ContentViewWordItem contentItemPrefab;

        public void Add(WordDto word)
        {
            ContentViewWordItem newItem = Instantiate(contentItemPrefab, parent: content);

            newItem.UpdateItem(word);
        }
    }
}