using UnityEngine;

namespace Ships.Interfaces
{
    public interface ISpaceShip
    {
        event System.Action DisposEvent;
        void MoveAcceleration(float acceleration);
        void ShipDamage();
        void Destroy();
    }
}