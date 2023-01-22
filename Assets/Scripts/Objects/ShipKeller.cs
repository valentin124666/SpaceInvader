using Managers.Controller;
using Managers.Interfaces;
using Settings;
using Ships.Interfaces;
using UnityEngine;

namespace Objects
{
    public class ShipKeller : Killer
    {
        [SerializeField]
        private Enumerators.TargetShot _targetKeller;

        void Start()
        {
            _target = _targetKeller.ToString();
        }
        protected override void PronouncedDamage(ISpaceShip ship)
        {
            GameClient.Instance.GetService<IGameplayManager>().GetController<LevelController>().GameOver();
            base.PronouncedDamage(ship);
        }
    }
}
