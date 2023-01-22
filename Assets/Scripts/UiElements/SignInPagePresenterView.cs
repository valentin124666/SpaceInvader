using Core;
using Managers.Interfaces;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UiElements
{
    [PrefabInfo("StartMenu")]
    public class SignInPagePresenterView : SimplePresenterView<SignInPagePresenter, SignInPagePresenterView>
    {
        [SerializeField] private Button _startButton;

        public override void Init()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            GameClient.Get<IGameplayManager>().ChangeAppState(Enumerators.AppState.InGame);
        }
    }
}