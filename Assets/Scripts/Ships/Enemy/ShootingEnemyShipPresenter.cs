using UnityEngine;

namespace Ships.Enemy
{
    public class ShootingEnemyShipPresenter : EnemyShipPresenter
    {
        public bool IsShoot { get;}

        public ShootingEnemyShipPresenter(ShootingEnemyShipPresenterView view,Vector3 startPos, bool isShoot) : base(view,startPos)
        {
            IsShoot = isShoot;
        }
    }
}