using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Transform player; // 玩家角色
    public float lightRange = 20f; // 光源影響距離
    private Light lightSource;
    [SerializeField] private bool DistanceControl = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lightSource = GetComponent<Light>();
        // 訂閱陰影品質變更事件
        GameSettings.OnShadowQualityChanged += UpdateShadowQuality;
        
    }
    void Start()
    {
        if (DistanceControl)
        {
            InvokeRepeating("Light", 0f, 0.5f);
        }
    }
    void Light()
    {
        // 計算玩家與光源的距離
        float distance = Vector3.Distance(transform.position, player.position);
        // 控制光源的啟用
        lightSource.enabled = distance <= lightRange;
    }

    private void UpdateShadowQuality(int quality)
    {
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
