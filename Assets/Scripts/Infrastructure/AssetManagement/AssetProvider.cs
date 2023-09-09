using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Transform at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at.position, Quaternion.identity, at);
        }

        public T Instantiate<T>(string path, Vector3 at) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab);
        }

        public T Instantiate<T>(string path, Transform at) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, at.position, Quaternion.identity, at);
        }
    }
}