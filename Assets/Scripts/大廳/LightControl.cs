using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Transform player; // 玩家角色
    public float lightRange = 20f; // 光源影響距離
    public float shadowDistance = 50f; // 陰影渲染距離

    private Light lightSource;

    void Awake()
    {
        lightRange = Mathf.Max(lightRange, shadowDistance);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lightSource = GetComponent<Light>();

        // 設置全局陰影距離
        QualitySettings.shadowDistance = shadowDistance;

        // 訂閱陰影品質變更事件
        GameSettings.OnShadowQualityChanged += UpdateShadowQuality;
    }

    void Update()
    {
        // 計算玩家與光源的距離
        float distance = Vector3.Distance(transform.position, player.position);

        // 控制光源的啟用
        lightSource.enabled = distance <= lightRange;
    }

    private void UpdateShadowQuality(int quality)
    {Debug.Log($"目前的陰影品質為: {GameSettings.ShadowQuality}");
        switch (quality)
        {
            case 0:
                lightSource.shadows = LightShadows.None;
                break;
            case 1:
                lightSource.shadows = LightShadows.Hard;
                break;
            case 2:
            case 3:
                lightSource.shadows = LightShadows.Soft;
                break;
        }
    }

    void OnDestroy()
    {
        // 確保在物件銷毀時解除訂閱
        GameSettings.OnShadowQualityChanged -= UpdateShadowQuality;
    }
}
