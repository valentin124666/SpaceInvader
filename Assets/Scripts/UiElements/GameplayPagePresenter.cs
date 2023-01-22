using Core;
using Managers.Interfaces;

namespace UiElements
{
    public class GameplayPagePresenter : SimplePresenter<GameplayPagePresenter, GameplayPagePresenterView>, IUIElement
    {
        private float _curentScore;

        public GameplayPagePresenter(GameplayPagePresenterView view) : base(view)
        {
            Hide();
        }

        public void Show()
        {
            View.SetActive(true);
        }

        public void Hide()
        {
            View.SetActive(false);
        }

        public void Update()
        {
        }

        public void AddScore(float score)
        {
            _curentScore += score;
            View.SetScore(_curentScore.ToString());
        }

        public void RemoveHealth() => View.RemoveHealth();
        
        public void Reset()
        {
            View.Reset();
            _curentScore = 0;
        }
    }
}