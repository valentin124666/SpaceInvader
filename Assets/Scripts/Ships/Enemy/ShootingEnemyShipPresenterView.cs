using Core;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Ships.Enemy
{
    [OverrideTypes(typeof(ShootingEnemyShipPresenter), typeof(ShootingEnemyShipPresenterView))]
    public class ShootingEnemyShipPresenterView : EnemyShipPresenterView
    {
        private ShootingEnemyShipPresenter _shipPresenter => ((ShootingEnemyShipPresenter)Presenter);

        private float _timerShooting;
        private bool _isShoot;

        public override void Init()
        {
            base.Init();
            _timerShooting = _delayShooting;
        }

        private void FixedUpdate()
        {
            Shooting();
        }

        private void Shooting()
        {
            if (_gameplayManager.IsPause || !_shipPresenter.IsShoot)
                return;

            if (_timerShooting <= 0)
            {
                Shot();
                _timerShooting = _delayShooting;
            }
            else
            {
                _timerShooting -= Time.deltaTime;
            }
        }
    }
}