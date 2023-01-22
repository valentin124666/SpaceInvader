using Settings;
using UnityEngine;

namespace Ships.Enemy
{
    public class EnemyShipPresenterView : SpaceShipPresenterView<EnemyShipPresenter, EnemyShipPresenterView>
    {
        
        [SerializeField]
        private Transform _rightAnchor;
        public Transform RightAnchor => _rightAnchor;

        [SerializeField]
        private Transform _leftAnchor;
        public Transform LeftAnchor => _leftAnchor;

        public void MoveControl(Enumerators.MoveMode moveMode)
        {
            _moveMode = moveMode;
        }
  
    }
}
