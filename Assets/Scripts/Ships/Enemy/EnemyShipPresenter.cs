using Objects;
using Settings;
using UiElements;
using UnityEngine;

namespace Ships.Enemy
{
    public class EnemyShipPresenter : SpaceShipPresenter<EnemyShipPresenter, EnemyShipPresenterView>
    {
        public Transform RightAnchor => View.RightAnchor;
        public Transform LeftAnchor => View.LeftAnchor;

        public EnemyShipPresenter(EnemyShipPresenterView view, Vector3 startPos) : base(view)
        {
            View.transform.position = startPos;
        }

        public void MoveControl(Enumerators.MoveMode moveMode) => View.MoveControl(moveMode);
        
        public override void ShipDamage()
        {
            _uIManager.GetPage<GameplayPagePresenter>().AddScore(_scoreHit);
            new AccountDestruction(_scoreHit, View.transform.position);
            base.ShipDamage();
        }
    }
}