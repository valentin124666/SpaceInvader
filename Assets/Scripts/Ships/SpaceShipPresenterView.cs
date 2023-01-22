using System;
using Core;
using Managers.Controller;
using Managers.Interfaces;
using Objects;
using Settings;
using Ships.Interfaces;
using UnityEngine;

namespace Ships
{
    [PrefabInfo("Ship/")]
    public class SpaceShipPresenterView<TP, TV> : SimplePresenterView<TP, TV>, ISpaceShipView where TP : SpaceShipPresenter<TP, TV> where TV : SpaceShipPresenterView<TP, TV>
    {
        public ISpaceShip Ship => Presenter;

        [SerializeField] private Transform _shootPos;

        protected IGameplayManager _gameplayManager;

        protected EnvironmentController _environmentController;
        protected CharacteristicsShot _characteristicsShot;
        protected CameraController _cameraController;
        [SerializeField] protected Enumerators.MoveMode _moveMode;

        [SerializeField] public Enumerators.ShipType _shipType;
        public Enumerators.ShipType ShipType => _shipType;

        protected float _delayShooting;
        protected float _speedShip;
        protected bool _disableMoveControl;

        public override void Init()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();
            _cameraController = _gameplayManager.GetController<CameraController>();
            _environmentController = _gameplayManager.GetController<EnvironmentController>();

            SetCharacteristics();
            MainApp.Instance.FixedUpdateEvent += MoveShip;
        }
        
        protected void Shot()
        {
            Bullet bullet = _environmentController.CreateBullet();

            bullet.InitBulletShoting(_characteristicsShot);
            bullet.transform.position = _shootPos.position;
        }

        private void MoveShip()
        {
            if (_moveMode == Enumerators.MoveMode.None || _gameplayManager.IsPause)
                return;
            
            Vector3 posShip = transform.position;

            if (_moveMode == Enumerators.MoveMode.Left)
            {
                posShip += (Vector3.left * _speedShip);

                if (posShip.x < -_cameraController.Border && !_disableMoveControl)
                {
                    posShip.x = -_cameraController.Border;
                }
            }
            else if (_moveMode == Enumerators.MoveMode.Right)
            {
                posShip += (Vector3.right * _speedShip);

                if (posShip.x > _cameraController.Border && !_disableMoveControl)
                {
                    posShip.x = _cameraController.Border;
                }
            }

            transform.position = posShip;
        }
        public virtual void DestroyShip()
        {
            MainApp.Instance.FixedUpdateEvent -= MoveShip;
        }
        public void MoveAcceleration(float acceleration)
        {
            _speedShip += acceleration;
        }

        private void SetCharacteristics()
        {
            var characteristicsShip = MainApp.Instance.GameData.GetShipsData(ShipType);

            _speedShip = characteristicsShip.speed;
            _delayShooting = characteristicsShip.characteristicsShot.delayShooting;
            _characteristicsShot = characteristicsShip.characteristicsShot;
        }
    }
}