using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Transform at) where T : Object;
    }
}