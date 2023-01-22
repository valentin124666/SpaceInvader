using UnityEngine;

namespace Ships.Enemy
{
    public class MotherShipPresenter : EnemyShipPresenter
    {
        public MotherShipPresenter(MotherShipPresenterView view, Vector3 startPos) : base(view, startPos)
        {
        }
    }
}