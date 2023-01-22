using Settings;
using UnityEngine;

namespace Ships
{
    public class PlayerShipPresenterView : SpaceShipPresenterView<PlayerShipPresenter, PlayerShipPresenterView>
    {
        public override void Init()
        {
            base.Init();
            ToStartingPosShips();
        }

        public void Update()
        {
            if (_gameplayManager.IsPause)
                return;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _moveMode = Enumerators.MoveMode.Right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _moveMode = Enumerators.MoveMode.Left;
            }
            else
            {
                _moveMode = Enumerators.MoveMode.None;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shot();
            }
        }

        public void ToStartingPosShips()
        {
            Vector3 startPosShips = _cameraController.MinPos;
            startPosShips.z = transform.position.z;
            startPosShips.x = _cameraController.Camera.transform.position.x;
            transform.position = startPosShips;
        }
    }
}