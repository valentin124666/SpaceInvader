using System;
using Core;
using Settings;
using UnityEngine;

namespace Ships.Enemy
{
    [OverrideTypes(typeof(MotherShipPresenter), typeof(MotherShipPresenterView))]

    public class MotherShipPresenterView : EnemyShipPresenterView
    {
        private float _timerShooting;

        public override void Init()
        {
            base.Init();
            _timerShooting = _delayShooting;
            _disableMoveControl = true;
            MainApp.Instance.FixedUpdateEvent += Shooting;
            MainApp.Instance.LateUpdateEvent += ControlPosition;

        }
        
        private void Shooting()
        {
            if (_gameplayManager.IsPause)
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

        public override void DestroyShip()
        {
            base.DestroyShip();
            MainApp.Instance.FixedUpdateEvent -= Shooting;
            MainApp.Instance.LateUpdateEvent -= ControlPosition;

        }

        private void ControlPosition()
        {
            if ((_moveMode == Enumerators.MoveMode.Left && RightAnchor.position.x < _cameraController.MinPos.x) ||
                (_moveMode == Enumerators.MoveMode.Right && LeftAnchor.position.x > _cameraController.MaxPos.x))
            {
                Presenter.DestroyShip();
            }
        }
    }
}