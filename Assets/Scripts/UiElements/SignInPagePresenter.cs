using Core;
using Managers.Interfaces;

namespace UiElements
{
    public class SignInPagePresenter : SimplePresenter<SignInPagePresenter,SignInPagePresenterView>,IUIElement
    {
        public SignInPagePresenter(SignInPagePresenterView view) : base(view)
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

        public void Reset()
        {
        }
    }
}
