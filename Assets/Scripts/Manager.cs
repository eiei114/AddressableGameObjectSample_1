using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Manager : MonoBehaviour
{
    private string avatarKey = "Assets/Flamers/Avatars/";

    
    public void GetAvatarFromAddressable(string key, Action<GameObject> callback)
    {
        var path = $"{avatarKey}{key}";
        // Addressableから指定されたキーのアセットを非同期でロード
        var handle = Addressables.LoadAssetAsync<GameObject>(path);

        // ロード完了時のコールバックを設定
        handle.Completed += (AsyncOperationHandle<GameObject> opHandle) =>
        {
            if (opHandle.Status == AsyncOperationStatus.Succeeded)
            {
                callback?.Invoke(opHandle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset with key: {key}");
                callback?.Invoke(null);
            }
        };
    }
}
