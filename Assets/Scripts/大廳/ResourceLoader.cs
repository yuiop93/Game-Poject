using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    public static ResourceLoader Instance; // 單例模式
    private Dictionary<string, Object> cachedResources = new Dictionary<string, Object>();

    private void Awake()
    {
        // 確保單例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持常駐
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 預加載資源
    public IEnumerator PreloadResources(string[] resourcePaths)
    {
        foreach (string path in resourcePaths)
        {
            if (!cachedResources.ContainsKey(path))
            {
                Object resource = Resources.Load(path);
                if (resource != null)
                {
                    cachedResources[path] = resource;
                    Debug.Log($"資源已預加載: {path}");
                }
                else
                {
                    Debug.LogWarning($"無法載入資源: {path}");
                }
                yield return null; // 每次加載一個資源後暫停一幀，避免卡頓
            }
        }
    }

    // 獲取已加載的資源
    public T GetResource<T>(string path) where T : Object
    {
        if (cachedResources.ContainsKey(path))
        {
            return cachedResources[path] as T;
        }
        else
        {
            Debug.LogWarning($"資源未找到: {path}");
            return null;
        }
    }
}
