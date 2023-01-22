using Cysharp.Threading.Tasks;
using Managers.Interfaces;
using Settings;
using Ships;

namespace Managers.Controller
{
    public class PlayerController : IController
    {
        private PlayerShipPresenter _selfShips;

        private IGameplayManager _gameplayManager;

        private EnvironmentController _environmentController;
        private LevelController _levelController;


        public void Init()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();

            _environmentController = _gameplayManager.GetController<EnvironmentController>();
            _levelController = _gameplayManager.GetController<LevelController>();
        }

        public void Update()
        {
        }

        private void Died()
        {
            _levelController.GameOver();
        }

        public async UniTask CreateNewShips()
        {
            _selfShips = await _environmentController.CreateShips<PlayerShipPresenter, PlayerShipPresenterView>(Enumerators.LocationSuffix.Player.ToString());
            _selfShips.DisposEvent += Died;
        }

        public void ResetAll()
        {
            if (_selfShips == null)
                return;

            _selfShips.DisposEvent -= Died;
            Dispose();
            _selfShips = null;
        }

        public void Dispose()
        {
            _selfShips?.OnDestroy();
        }
    }
}