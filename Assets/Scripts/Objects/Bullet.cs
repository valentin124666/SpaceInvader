using Managers.Controller;
using Managers.Interfaces;
using Settings;
using Ships.Interfaces;
using UnityEngine;

namespace Objects
{
    public class Bullet : Killer
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        private CameraController _cameraController;

        [SerializeField]
        private float _speed;
        private void Start()
        {
            _cameraController = GameClient.Get<IGameplayManager>().GetController<CameraController>();

        }
        protected override void PronouncedDamage(ISpaceShip ship)
        {
            base.PronouncedDamage(ship);
            _environmentController.DisposeBullet(this);
        }
        private void FixedUpdate()
        {
            transform.Translate(Vector3.up * _speed);

            if(transform.position.y > _cameraController.MaxPos.y||transform.position.y<_cameraController.MinPos.y)
            {
                _environmentController.DisposeBullet(this);
            }
        }
        public void InitBulletShoting(CharacteristicsShot characteristics)
        {
            _meshRenderer.material = characteristics.material;
            _target = characteristics.target.ToString();
            _speed = characteristics.target == Enumerators.TargetShot.Player ? -characteristics.speedBullet : characteristics.speedBullet;
        }
    }
}

