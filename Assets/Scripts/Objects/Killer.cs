using Managers.Controller;
using Managers.Interfaces;
using Ships;
using Ships.Interfaces;
using UnityEngine;

namespace Objects
{
    public class Killer : MonoBehaviour
    {
        protected string _target;
        protected EnvironmentController _environmentController;

        private void Awake()
        {
            _environmentController = GameClient.Get<IGameplayManager>().GetController<EnvironmentController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_target))
            {
                var ship = _environmentController.GetShip(other.GetComponent<ISpaceShipView>().Ship);
                if (ship != null)
                {
                    PronouncedDamage(ship);
                }
            }
        }
        protected virtual void PronouncedDamage(ISpaceShip ship)
        {
            ship.ShipDamage();
        }
    }
}
