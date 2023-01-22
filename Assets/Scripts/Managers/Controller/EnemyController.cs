using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers.Interfaces;
using Settings;
using Ships;
using Ships.Enemy;
using UnityEngine;

namespace Managers.Controller
{
    public class EnemyController : IController
    {
        private List<EnemyShipPresenter> _enemyShips;
        private List<EnemyShipPresenter> _enemyShipsGroup;

        private bool[] _probabilities;

        private IGameplayManager _gameplayManager;

        private EnvironmentController _environmentController;
        private CameraController _cameraController;
        private LevelController _levelController;

        private bool _isMotherShipCreete = false;
        private float _shipAcceleration = 0.001f;
        private float _delaySpawnMothership = 4, _timerSpawnMothership;


        public Enumerators.MoveMode ShipsMoveMode;

        public void Init()
        {
            _gameplayManager = GameClient.Get<IGameplayManager>();

            _environmentController = _gameplayManager.GetController<EnvironmentController>();
            _cameraController = _gameplayManager.GetController<CameraController>();
            _levelController = _gameplayManager.GetController<LevelController>();

            _enemyShips = new List<EnemyShipPresenter>();
            _enemyShipsGroup = new List<EnemyShipPresenter>();
            _timerSpawnMothership = _delaySpawnMothership;
            CreatePossibility();
        }

        private void RemoveEnemyShips(EnemyShipPresenter enemy)
        {
            _enemyShips.Remove(enemy);
            if (_enemyShips.Count == 0)
            {
                _levelController.GameWin();
            }
        }

        private void CreateMotherShip()
        {
            CreateMotherShipTask().Forget();
        }

        private async UniTask CreateMotherShipTask()
        {
            if (_gameplayManager.IsPause || _isMotherShipCreete)
                return;

            if (_timerSpawnMothership <= 0)
            {
                _isMotherShipCreete = true;
                if (_probabilities[Random.Range(0, 100)])
                {
                    Vector3 posSpawn = new Vector3(_cameraController.MinPos.x - 2, _cameraController.MaxPos.y - 0.5f, 0);

                    var ship = await CreateEnemy(Enumerators.LocationSuffix.Mothership.ToString(), posSpawn);
                    ship.MoveControl(Enumerators.MoveMode.Right);
                }
                _isMotherShipCreete = false;

                _timerSpawnMothership = _delaySpawnMothership;
            }
            else
            {
                _timerSpawnMothership -= Time.deltaTime;
            }
        }

        private async UniTask CreateEnemyGroup()
        {
            int numberLine = MainApp.Instance.LevelData.curentLevel.NumberEnemies.NumberLine;
            int numberRow = MainApp.Instance.LevelData.curentLevel.NumberEnemies.NumberRow;

            float stepAside = 0.6f;
            float stetpDown = 0.5f;
            Vector3 posSpawn = new Vector3(_cameraController.Camera.transform.position.x - stepAside / 2, _cameraController.MaxPos.y - 1, 0);
            posSpawn.x += stepAside * ((float)numberRow / 2);

            for (int i = 0; i < numberLine; i++)
            {
                for (int j = 0; j < numberRow; j++)
                {
                    bool isShooting = (i == 1 || i == 3) && (j == 0 || j == numberRow - 1);

                    var ship = await CreateEnemy(Enumerators.LocationSuffix.Enemy.ToString(), posSpawn, isShooting);
                    _enemyShipsGroup.Add(ship);
                    ship.DisposEvent += () => { _enemyShipsGroup.Remove(ship); };

                    posSpawn.x -= stepAside;
                }

                posSpawn.x += stepAside * numberRow;
                posSpawn.y -= stetpDown;
            }

            MainApp.Instance.LateUpdateEvent += DirectionControlGroup;
            DirectionMoveOfShipsGroup();
        }

        private void CreatePossibility()
        {
            _probabilities = new bool[100];
            int percentageTruth = 100;
            List<int> selected = new List<int>();
            while (percentageTruth > 0)
            {
                int i = Random.Range(0, 100);
                if (!selected.Contains(i))
                {
                    _probabilities[i] = true;
                    selected.Add(i);
                    percentageTruth--;
                }
            }
        }

        public async UniTask StartWar()
        {
            MainApp.Instance.FixedUpdateEvent += CreateMotherShip;
            await CreateEnemyGroup();
        }

        public async UniTask<EnemyShipPresenter> CreateEnemy(string locationSuffix, params object[] args)
        {
            var ship = await _environmentController.CreateShips<EnemyShipPresenter, EnemyShipPresenterView>(locationSuffix, args);

            _enemyShips.Add(ship);

            ship.DisposEvent += () => { RemoveEnemyShips(ship); };

            return ship;
        }

        public void DirectionControlGroup()
        {
            foreach (var item in _enemyShipsGroup)
            {
                if (ShipsMoveMode == Enumerators.MoveMode.Right && item.RightAnchor.position.x > _cameraController.Border)
                {
                    ChangeDirectionGroup();
                }
                else if (ShipsMoveMode == Enumerators.MoveMode.Left && item.LeftAnchor.position.x < -_cameraController.Border)
                {
                    ChangeDirectionGroup();
                }
            }
        }

        public void DirectionMoveOfShipsGroup()
        {
            ShipsMoveMode = ShipsMoveMode == Enumerators.MoveMode.Left ? Enumerators.MoveMode.Right : Enumerators.MoveMode.Left;

            foreach (var item in _enemyShipsGroup)
            {
                item.MoveControl(ShipsMoveMode);
            }
        }

        private void ChangeDirectionGroup()
        {
            DirectionMoveOfShipsGroup();
            float step = 0.3f;
            foreach (var item in _enemyShipsGroup)
            {
                item.Translate(Vector3.down * step);
                item.MoveAcceleration(_shipAcceleration);
            }
        }

        public void ResetAll()
        {
            _enemyShips.Clear();
            _enemyShipsGroup.Clear();
            MainApp.Instance.FixedUpdateEvent -= CreateMotherShip;
            MainApp.Instance.LateUpdateEvent -= DirectionControlGroup;
        }

        public void Dispose()
        {
            _enemyShips.Clear();
            _enemyShipsGroup.Clear();
            MainApp.Instance.FixedUpdateEvent -= CreateMotherShip;
            MainApp.Instance.LateUpdateEvent -= DirectionControlGroup;
        }

        public void Update()
        {
        }
    }
}