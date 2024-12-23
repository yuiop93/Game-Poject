using UnityEngine;
using System.Collections.Generic;

public class ItemIconGenerator : MonoBehaviour
{
    public Camera screenshotCamera; // 專用的拍照攝影機
    public RenderTexture renderTexture; // 用於捕捉道具圖片的 RenderTexture
    public Transform photoPosition; // 拍照位置

    private Dictionary<Item, Sprite> iconCache = new Dictionary<Item, Sprite>(); // 緩存生成的圖標

    /// <summary>
    /// 生成道具圖標並返回 Sprite。
    /// </summary>
    /// <param name="item">目標道具</param>
    /// <returns>生成的 Sprite</returns>
    public Sprite GenerateIcon(Item item)
    {
        if (iconCache.ContainsKey(item))
        {
            // 如果已經生成過，直接返回緩存的圖標
            return iconCache[item];
        }

        // 將 RenderTexture 設為當前目標
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        // 將道具的模型生成到指定位置
        GameObject tempInstance = Instantiate(item.itemPrefab);
        tempInstance.transform.position = photoPosition.position; // 設置到特定拍照位置
        tempInstance.transform.rotation = photoPosition.rotation; // 可選：根據 photoPosition 的方向

        // 渲染到 RenderTexture
        screenshotCamera.Render();

        // 讀取 RenderTexture 的內容
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // 將 Texture2D 轉換為 Sprite
        Sprite icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        iconCache[item] = icon; // 保存到緩存

        // 清理臨時物件
        Destroy(tempInstance);
        RenderTexture.active = currentRT;

        return icon;
    }


    /// <summary>
    /// 清理所有緩存的圖標（釋放內存）。
    /// </summary>
    public void ClearIcons()
    {
        foreach (var sprite in iconCache.Values)
        {
            Destroy(sprite.texture); // 銷毀生成的 Texture2D
            Destroy(sprite); // 銷毀 Sprite
        }
        iconCache.Clear();
    }
}
