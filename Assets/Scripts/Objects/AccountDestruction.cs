using Core;
using Cysharp.Threading.Tasks;
using Settings;
using UnityEngine;

namespace Objects
{
    public class AccountDestruction
    {
        private Check _selfObj;

        public AccountDestruction(int number, Vector3 transformDestruction)
        {
            Create(number, transformDestruction).Forget();
        }

        private async UniTask Create(int number, Vector3 transformDestruction)
        {
            _selfObj = await ResourceLoader.Instantiate<Check>(Enumerators.LocationSuffix.Check.ToString(), null);
            _selfObj.transform.position = transformDestruction + Vector3.back;
            _selfObj.Text = number.ToString();
            await UniTask.Delay(500, ignoreTimeScale: false);
            ResourceLoader.ReleaseInstance(_selfObj.gameObject);

        }
    
    }
}
