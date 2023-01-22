using System;
using Core;
using Managers.Interfaces;
using UnityEngine;

namespace UiElements.Popups
{
    public class PausePopupsPresenter : SimplePresenter<PausePopupsPresenter, PausePopupsPresenterView>, IUIPopup
    {
        private Action _callback;

        private int _currentNumber;
        private float _timerNumberChange;
        private float _delayNumberChange = 0.5f;

        public PausePopupsPresenter(PausePopupsPresenterView view) : base(view)
        {
            _timerNumberChange = _delayNumberChange;
            MainApp.Instance.FixedUpdateEvent += Countdown;

            Refresh();
            Hide();
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

        public void Update()
        {
        }

        private void Countdown()
        {
            if (!View.gameObject.activeSelf)
                return;

            if (_timerNumberChange <= 0)
            {
                _currentNumber--;
                View.SetCounterText(_currentNumber.ToString());

                _timerNumberChange = _delayNumberChange;

                if (_currentNumber <= 0)
                {
                    Refresh();
                    Hide();
                    _callback?.Invoke();
                }
            }
            else
            {
                _timerNumberChange -= Time.deltaTime;
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            MainApp.Instance.FixedUpdateEvent -= Countdown;
        }

        public void Reset()
        {
            _callback = null;
            Refresh();
            Hide();
        }

        private void Refresh()
        {
            _currentNumber = 3;
            View.SetCounterText(_currentNumber.ToString());
        }
    }
}