namespace Settings
{
    public class Enumerators
    {
        public enum AppState
        {
            Unknown,

            AppStart,
            InGame,
        }
        public enum TargetShot
        {
            Enemy,
            Player
        }
        public enum MoveMode
        {
            None,
            Right,
            Left
        }
        public enum ShipType
        {
            PlayerShip,
            StandartEnemyShip,
            ShootingEnemyShip,
            MotherShip
        }
        public enum LocationSuffix
        {
            Enemy,
            Mothership,
            Player,
            Health,
            Check
        }
    }
}
