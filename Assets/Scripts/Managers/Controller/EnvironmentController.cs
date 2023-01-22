using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Managers.Interfaces;
using Objects;
using Ships;
using Ships.Interfaces;
using UnityEngine;

namespace Managers.Controller
{
    public class EnvironmentController : IController
    {
        private List<Bullet> _bulletList;
        private List<ISpaceShip> _spaceShips;

        private Transform _bulletContainer;
        private Transform _shipsModelContainer;

        public void Init()
        {
            _bulletList = new List<Bullet>();
            _spaceShips = new List<ISpaceShip>();
            _bulletContainer = new GameObject().transform;
            _bulletContainer.name = "[BulletContainer]";
            _shipsModelContainer = new GameObject().transform;
            _shipsModelContainer.name = "[ShipsModelContainer]";
        }

        public Bullet CreateBullet()
        {
            Bullet bullet = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetGameplayElements("Bullet")).GetComponent<Bullet>();

            bullet.transform.SetParent(_bulletContainer);
            _bulletList.Add(bullet);
            return bullet;
        }

        public void DisposeBullet(Bullet bullet)
        {
            _bulletList.Remove(bullet);
            MonoBehaviour.Destroy(bullet.gameObject);
        }

        public void Dispose()
        {
            DisposeAllBullet();
            foreach (var item in _spaceShips)
            {
                item.Destroy();
            }

            _spaceShips.Clear();
        }

        public void DisposeAllBullet()
        {
            foreach (var item in _bulletList)
            {
                if (item != null)
                    MonoBehaviour.Destroy(item.gameObject);
            }

            _bulletList.Clear();
        }

        public async UniTask<TP> CreateShips<TP, TV>(string locationSuffix = null, params object[] args) where TP : SpaceShipPresenter<TP, TV> where TV : SpaceShipPresenterView<TP, TV>
        {
            var ship = await ResourceLoader.Instantiate<TP, TV>(_shipsModelContainer, locationSuffix, args);
            _spaceShips.Add(ship);

            return ship;
        }
        
        public ISpaceShip GetShip(ISpaceShip ship)
        {
            return _spaceShips.Find(item => item == ship);
        }

        public void RemoveShip(ISpaceShip ship)
        {
            if (ship == null)
                return;

            _spaceShips.Remove(ship);
            ship.Destroy();
        }

        public void ResetAll()
        {
            Dispose();
        }

        public void Update()
        {
        }
    }
}