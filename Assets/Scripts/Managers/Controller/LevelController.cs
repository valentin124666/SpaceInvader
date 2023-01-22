using Managers.Interfaces;
using UiElements.Popups;
using UnityEngine;

namespace Managers.Controller
{
    public class LevelController : IController
    {
        private GameObject _selfLevel;
        private IGameplayManager _gameplayManager;
        private IUIManager _uIManager;

        public void Init()
        {
            _uIManager = GameClient.Instance.GetService<IUIManager>();
            _gameplayManager = GameClient.Instance.GetService<IGameplayManager>();

        }
        public void CreateLevel()
        {
            _selfLevel = MonoBehaviour.Instantiate(MainApp.Instance.LevelData.curentLevel.PrefabLevel);
        }
        public void SetStateLeve(bool state)
        {
            if (_selfLevel != null)
                _selfLevel.SetActive(state);
        }
        public void ResetAll()
        {
            Dispose();
        }
        public void GameOver()
        {
            _uIManager.GetPopup<EndGamePopupPresenter>().Show();
            _uIManager.GetPopup<EndGamePopupPresenter>().ReportResult("Game Over");

            _gameplayManager.StopGameplay();        
        }
        public void GameWin()
        {
            _uIManager.GetPopup<EndGamePopupPresenter>().Show();
            _uIManager.GetPopup<EndGamePopupPresenter>().ReportResult("Game Won");

            _gameplayManager.StopGameplay();
        }
        public void Update()
        {
        }
        public void Dispose()
        {
            MainApp.Destroy(_selfLevel);
        }

    }
}
