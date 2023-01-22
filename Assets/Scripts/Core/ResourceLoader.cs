using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Core
{
    public static class ResourceLoader
    {
        public static Transform _parentlessPool;

        public static async UniTask Init()
        {
            _parentlessPool = new GameObject().transform;
            _parentlessPool.name = "[ParentlessPool]";

            await Addressables.InitializeAsync();
        }

        private static async UniTask<IList<IResourceLocation>> LoadResourceLocationsAsync(object[] keys)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(keys as IEnumerable, Addressables.MergeMode.Union);
            
            return (locations);
        }

        public static bool ReleaseInstance(GameObject instance)
        {
            return Addressables.ReleaseInstance(instance);
        }
        
        public static string GetLocation<T>(string locationSuffix = null)
        {
            var location = typeof(T).GetCustomAttribute<PrefabInfo>().Location;
            if (!string.IsNullOrEmpty(locationSuffix))
            {
                location += locationSuffix;
            }

            return location;
        }
        
        #region Instantiate

        public static async UniTask<TP> Instantiate<TP, TV>(Transform parent, string locationSuffix, params object[] args)
            where TP : SimplePresenter<TP, TV> where TV : SimplePresenterView<TP, TV>
        {
            var view = await Instantiate<TV>(GetLocation<TV>(locationSuffix), parent);

            if (view != null)
            {
                return (TP)view.Instantiate(args);
            }

            return null;
        }

        public static async UniTask<T> Instantiate<T>(string key, Transform parent) where T : Component
        {
            var locations = await LoadResourceLocationsAsync(new object[] { key });

            if (locations.Count > 0)
            {
                var handle = Addressables.InstantiateAsync(locations[0], parent ? parent : _parentlessPool);
                var prefab = await handle;
                return prefab.GetComponent<T>();
            }

            return null;
        }

        #endregion
    }
}