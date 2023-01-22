using System.Collections.Generic;
using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Managers.Controller;
using Managers.Interfaces;
using Settings;
using UiElements;
using UiElements.Popups;

namespace Managers
{
    public class GameplayManager : IService, IGameplayManager
    {
        private IUIManager _uIManager;
        private List<IController> _controllers;
        public Enumerators.AppState CurrentState { get; private set; }
        public bool IsPause { get; private set; }
        public bool EndGame { get; private set; }

        public void Update()
        {
            foreach (var item in _controllers)
                item.Update();
        }

        public void Dispose()
        {
            foreach (var item in _controllers)
                item.Dispose();
        }

        public T GetController<T>() where T : IController
        {
            return (T)_controllers.Find(controller => controller is T);
        }

        public async UniTask Init()
        {
            _uIManager = GameClient.Get<IUIManager>();

            FillControllers();
        }

        private void FillControllers()
        {
            _controllers = new List<IController>()
            {
                new EnvironmentController(),
                new CameraController(),
                new LevelController(),
                new EnemyController(),
                new PlayerController()
            };

            foreach (var item in _controllers)
                item.Init();
        }

        public void EnablePause()
        {
            IsPause = true;
            GetController<EnvironmentController>().DisposeAllBullet();
            _uIManager.GetPopup<PausePopupsPresenter>().Show(() => IsPause = false);
        }

        public void RefreshGameplay()
        {
            StopGameplay();
            _uIManager.HideAllPopups();
            StartGameplay().Forget();
        }

        public async UniTask StartGameplay()
        {
            IsPause = false;
            EndGame = false;

            await GetController<PlayerController>().CreateNewShips();
            await GetController<EnemyController>().StartWar();
            GetController<LevelController>().CreateLevel();
            EnablePause();
        }

        public void StopGameplay()
        {
            IsPause = true;
            EndGame = true;

            foreach (var item in _controllers)
                item.ResetAll();

            _uIManager.ResetAll();
        }

        public void ChangeAppState(Enumerators.AppState stateTo)
        {
            CurrentState = stateTo;
            switch (stateTo)
            {
                case Enumerators.AppState.AppStart:
                    _uIManager.HideAllPopups();
                    _uIManager.SetPage<SignInPagePresenter>();
                    break;
                case Enumerators.AppState.InGame:
                    StartGameplay().Forget();
                    //_uIManager.HideAllPopups();
                    _uIManager.SetPage<GameplayPagePresenter>();
                    break;
            }
        }
    }
}