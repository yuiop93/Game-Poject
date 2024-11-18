using UnityEngine;
using StarterAssets; // 引入 StarterAssets
public class 雷射 : MonoBehaviour
{
    private StarterAssetsInputs _input;
    [SerializeField]
    private Transform gunMuzzle; // 槍口位置
    [SerializeField]
    private LineRenderer laserLineRenderer; // LineRenderer 用於雷射
    [SerializeField]
    private float laserWidth = 0.1f; // 雷射的寬度
    [SerializeField]
    private float laserMaxLength = 100f; // 雷射的最大長度
    public Rigidbody grabbedObject; // 命中物體
    private Vector3 aimWorldPos; // 目標世界位置
    void Start()
    {
        // 確保有 StarterAssetsInputs，否則嘗試獲取
        _input = GetComponent<StarterAssetsInputs>();
        if (_input == null)
        {
            Debug.LogError("缺少 StarterAssetsInputs 組件！");
        }
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        laserLineRenderer.enabled = false; // 初始關閉雷射
    }
    void Update()
    {
        if (_input.fire) // 判斷是否按下 Fire 按鍵（通常是滑鼠左鍵或觸屏按鈕）
        {
            Fire();
        }
        else
        {
            Release();
        }
    }
    void Fire()
    {
        laserLineRenderer.enabled = true; // 顯示雷射
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 從鼠標位置發射射線
        if (Physics.Raycast(ray, out RaycastHit hit, laserMaxLength)) // 如果射線碰撞到物體
        {

            if (grabbedObject == null) // 如果還沒有抓取物體
            {
                // 嘗試抓取物體
                if (hit.collider.TryGetComponent(out Rigidbody rb))
                {
                    grabbedObject = rb; // 設置為抓取的物體
                }

            }
        }

        if (grabbedObject != null) // 如果有抓取物體
        {
            laserLineRenderer.SetPositions(new Vector3[] { gunMuzzle.position, grabbedObject.position }); // 設置雷射線的起點和終點
        }
        else
        {
            aimWorldPos = ray.origin + ray.direction * laserMaxLength; // 計算射線的終點
            laserLineRenderer.SetPositions(new Vector3[] { gunMuzzle.position, aimWorldPos }); // 設置雷射線的終點
        }
    }
    void Release()
    {
        laserLineRenderer.enabled = false;
        grabbedObject = null;
    }
}
