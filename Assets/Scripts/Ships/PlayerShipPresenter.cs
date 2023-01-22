using UiElements;

namespace Ships
{
    public class PlayerShipPresenter : SpaceShipPresenter<PlayerShipPresenter, PlayerShipPresenterView>
    {
        public PlayerShipPresenter(PlayerShipPresenterView view) : base(view)
        {
            
        }

        public override void ShipDamage()
        {
            base.ShipDamage();
            
            _uIManager.GetPage<GameplayPagePresenter>().RemoveHealth();
            View.ToStartingPosShips();
            if (_healthPlayer > 0 && !_gameplayManager.EndGame)
            {
                _gameplayManager.EnablePause();
            }
        }
    }
}