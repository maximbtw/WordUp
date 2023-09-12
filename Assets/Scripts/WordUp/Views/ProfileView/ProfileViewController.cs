using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Views.ProfileView.Control;
using Zenject;

namespace WordUp.Views.ProfileView
{
    public class ProfileViewController : SceneControllerBase
    {
        [SerializeField] private ProfileViewLabel learnedWordsCount;
        [SerializeField] private ProfileViewLabel hardWordsCount;

        [Inject] private IWordService _wordService;

        protected override void LateStart()
        {
            List<WordDto> models = _wordService.GetModels().ToList();
            
            learnedWordsCount.SetText(models.Count(x=>x.IsLearned).ToString());
            hardWordsCount.SetText(models.Count(x=>x.IsHard).ToString());
        }
    }
}