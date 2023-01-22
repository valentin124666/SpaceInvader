using Core;
using Managers.Interfaces;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UiElements.Popups
{
    [PrefabInfo("EndGame")]

    public class EndGamePopupPresenterView : SimplePresenterView<EndGamePopupPresenter, EndGamePopupPresenterView>
    {
        [SerializeField]
        private Text _messageText;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        public override void Init()
        {
            var gameplayManager = GameClient.Get<IGameplayManager>();
            _restartButton.onClick.AddListener(() => gameplayManager.RefreshGameplay());
            _mainMenuButton.onClick.AddListener(() => gameplayManager.ChangeAppState(Enumerators.AppState.AppStart));
        }

        public void SetMessage(string text) => _messageText.text = text;
    }
}