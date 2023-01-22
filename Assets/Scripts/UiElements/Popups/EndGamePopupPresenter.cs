using System;
using Core;
using Managers.Interfaces;

namespace UiElements.Popups
{
    public class EndGamePopupPresenter : SimplePresenter<EndGamePopupPresenter,EndGamePopupPresenterView>,IUIPopup
    {
        private Action _callback;

        public EndGamePopupPresenter(EndGamePopupPresenterView view) : base(view)
        {
        
        }

        public void Show()
        {
            View.SetActive(true);
        }

        public void Show(Action callback)
        {
            _callback = callback;
            View.SetActive(true);
        }

        public void Hide()
        {
            View.SetActive(false);
        }
        public void ReportResult(string message)=> View.SetMessage(message);

        public void Update()
        {
        }

        public void Reset()
        {
        }
    }
}
