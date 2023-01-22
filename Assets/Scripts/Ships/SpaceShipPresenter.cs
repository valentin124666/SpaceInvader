using System;
using Core;
using Managers.Controller;
using Managers.Interfaces;
using Ships.Interfaces;

namespace Ships
{
    public class SpaceShipPresenter<TP, TV> : SimplePresenter<TP, TV>, ISpaceShip where TP : SpaceShipPresenter<TP, TV> where TV : SpaceShipPresenterView<TP, TV>
    {
        public event Action DisposEvent;
        protected IUIManager _uIManager;
        protected IGameplayManager _gameplayManager;

        protected EnvironmentController _environmentController;
        
        protected int _healthPlayer;
        protected int _scoreHit;

        public SpaceShipPresenter(TV view) : base(view)
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _uIManager = GameClient.Get<IUIManager>();
            _environmentController = _gameplayManager.GetController<EnvironmentController>();
            
            var characteristicsShip = MainApp.Instance.GameData.GetShipsData(view.ShipType);

            _healthPlayer = characteristicsShip.health;
            _scoreHit = characteristicsShip.scoreHit;

        }
        public void MoveAcceleration(float acceleration)
        {
            View.MoveAcceleration(acceleration);
        }
        public void DestroyShip()
        {
            DisposEvent?.Invoke();
            _environmentController.RemoveShip(this);
        }

        public virtual void ShipDamage()
        {
            _healthPlayer--;
            if (_healthPlayer <= 0 || _gameplayManager.EndGame)
            {
                DestroyShip();
            }
        }

        public override void Destroy()
        {
            View.DestroyShip();
            OnDestroy();
        }
    }
}