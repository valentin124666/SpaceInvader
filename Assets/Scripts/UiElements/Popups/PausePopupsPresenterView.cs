using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UiElements.Popups
{
    [PrefabInfo("PausePopups")]

    public class PausePopupsPresenterView : SimplePresenterView<PausePopupsPresenter, PausePopupsPresenterView>
    {
        [SerializeField] private Text _counterText;


        public override void Init()
        {
        }

        public void SetCounterText(string text) => _counterText.text = text;
    }
}