using UnityEngine;
using StarterAssets; // 引入 StarterAssets
using System.Collections;
public class 雷射 : MonoBehaviour
{
    [SerializeField]
    private float grabDistance = 10f; // 抓取距離

    [SerializeField]
    private float 攻擊速度 = 0.2f; // 攻擊速度
    [SerializeField]
    private LineRenderer laserLineRenderer; // LineRenderer 用於雷射
    [SerializeField]
    private float laserWidth = 0.1f; // 雷射的寬度
    [SerializeField]
    private float laserMaxLength = 100f; // 雷射的最大長度
    [SerializeField]
    private Transform gunMuzzle; // 槍口位置
    private Rigidbody grabbedObject; // 命中物體
    private Vector3 aimWorldPos; // 目標世界位置
    public Transform attachPoint; // 抓取的連接點
    private 可被抓取 _currentGrabbable;
    private StarterAssetsInputs _input; // 輸入
    private 玩家狀態 玩家狀態;

    void Awake()
    {
        玩家狀態 = GetComponent<玩家狀態>();
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
    void OnEnable()
    {
        _input.fire = false;
    }
    void OnDisable()
    {
        Release();
    }
    void Update()
    {
        if (_input.fire) // 判斷是否按下 Fire 按鍵（通常是滑鼠左鍵或觸屏按鈕）
        {
            if (玩家狀態.能量 > 0)
            {
                Fire();
                if (_fireCoroutine == null)
                {
                    _fireCoroutine = StartCoroutine(耗能());
                }
            }
            else
            {
                Release();
            }
        }
        else
        {
            Release();
        }
    }
    public void Fire()
    {
        TryGrab();
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
        if (_currentGrabbable != null)
        {
            _currentGrabbable.Release();
            _currentGrabbable = null;
        }
        if (laserLineRenderer != null)
            laserLineRenderer.enabled = false;
        grabbedObject = null;
        玩家狀態.能量使用中 = false;
        玩家狀態.開始能量回復();
    }

    void TryGrab()
    {
        if (_currentGrabbable == null)
        {
            Grab();
        }
    }
    void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            可被抓取 可被抓取 = hit.collider.GetComponent<可被抓取>();
            if (可被抓取 != null)
            {
                _currentGrabbable = 可被抓取;
                // 確保抓取點有 Rigidbody
                Rigidbody attachRigidbody = attachPoint.gameObject.GetComponent<Rigidbody>();
                if (attachRigidbody == null)
                {
                    attachRigidbody = attachPoint.gameObject.AddComponent<Rigidbody>();
                    attachRigidbody.isKinematic = true; // 抓取點不受物理影響
                }

                _currentGrabbable.Grab(attachRigidbody);
            }
        }
    }
    private Coroutine _fireCoroutine;

    private IEnumerator 耗能()
    {
        玩家狀態.能量使用中 = true;
        while (_input.fire)
        {
            if (_currentGrabbable != null)
            {
                if (玩家狀態.能量 >= _currentGrabbable.能量消耗)
                {
                    玩家狀態.能量使用(_currentGrabbable.能量消耗);
                    yield return new WaitForSeconds(1 / 攻擊速度);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        Release();
        _fireCoroutine = null;
    }
}
