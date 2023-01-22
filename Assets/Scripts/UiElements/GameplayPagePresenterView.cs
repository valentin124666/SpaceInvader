using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UiElements
{
    [PrefabInfo("InGameplay")]
    public class GameplayPagePresenterView : SimplePresenterView<GameplayPagePresenter, GameplayPagePresenterView>
    {
        [SerializeField] private Transform _healthGroup;
        private List<GameObject> _healthPrefabs;
        [SerializeField] private Text _scoreText;

        private int _curentHealt;


        public override void Init()
        {
            CreateHealth().Forget();
        }

        public void Reset()
        {
            foreach (var item in _healthPrefabs)
            {
                item.SetActive(true);
            }

            _scoreText.text = 0.ToString();
        }

        public void SetScore(string score) => _scoreText.text = score;

        public void RemoveHealth()
        {
            if (_curentHealt >= 0)
            {
                _healthPrefabs[_curentHealt].SetActive(false);
                _curentHealt--;
            }
        }

        private async UniTask CreateHealth()
        {
            _healthPrefabs = new List<GameObject>();

            for (int i = 0; i < MainApp.Instance.GameData.GetShipsData(Enumerators.ShipType.PlayerShip).health; i++)
            {
                var health = await ResourceLoader.Instantiate<Transform>(Enumerators.LocationSuffix.Health.ToString(), _healthGroup);
                health.name = $"{i}";
                // health.transform.SetParent(_healthGroup, false);
                _healthPrefabs.Add(health.gameObject);
            }

            _curentHealt = _healthPrefabs.Count - 1;
        }
    }
}