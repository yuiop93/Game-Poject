using UnityEngine;
using StarterAssets; // 引入 StarterAssets

public class 抓取物體 : MonoBehaviour
{
    public Transform attachPoint; // 抓取的連接點
    private 可被抓取 _currentGrabbable;
    private StarterAssetsInputs _input;
    
    void Start()
    {
        // 確保有 StarterAssetsInputs，否則嘗試獲取
        _input = GetComponent<StarterAssetsInputs>();
        if (_input == null)
        {
            Debug.LogError("缺少 StarterAssetsInputs 組件！");
        }
    }

    void Update()
    {
        if (_input.fire) // 判斷是否按下 Fire 按鍵（通常是滑鼠左鍵或觸屏按鈕）
        {
            if (_currentGrabbable == null) // 尚未抓取
            {
                TryGrab();
            }
        }
        else if (_currentGrabbable != null) // 放開 Fire 按鍵時釋放
        {
            Release();
        }
    }

    private void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
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

    private void Release()
    {
        if (_currentGrabbable != null)
        {
            _currentGrabbable.Release();
            _currentGrabbable = null;
        }
    }
}
