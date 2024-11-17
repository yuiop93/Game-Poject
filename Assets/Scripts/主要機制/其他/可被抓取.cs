using UnityEngine;

public class 可被抓取 : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    [Header("抓取設置")]
    [SerializeField] 
    private float grabSpringForce = 20f;   // SpringJoint 的彈力
    [SerializeField]
    private float grabDrag = 5f;          // 抓取時的阻力
    private float _originalDrag;         // 原始阻力
    private bool _wasRotationFrozen;     // 是否原本凍結了旋轉

    private void Awake()
    {
        // 確保物件上有 Rigidbody
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// 開始抓取
    /// </summary>
    /// <param name="attachPoint">抓取的連接點</param>
    public void Grab(Rigidbody attachPoint)
    {
        if (_springJoint == null)
        {
            // 創建 SpringJoint 並配置參數
            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.connectedBody = attachPoint;
            _springJoint.anchor = Vector3.zero;  // 本地連接點
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = Vector3.zero;  // 連接點的本地連接點
            _springJoint.spring = grabSpringForce;
            _springJoint.damper = 0f;
        }

        // 保存原始阻力值
        _originalDrag = _rigidbody.drag;

        // 設定抓取時的阻力
        _rigidbody.drag = grabDrag;

        // 凍結旋轉軸
        _wasRotationFrozen = _rigidbody.freezeRotation;
        _rigidbody.freezeRotation = true;  // 凍結 X, Y, Z 軸旋轉
    }

    /// <summary>
    /// 釋放抓取
    /// </summary>
    public void Release()
    {
        if (_springJoint != null)
        {
            Destroy(_springJoint);
        }

        // 恢復原始阻力
        _rigidbody.drag = _originalDrag;

        // 恢復旋轉凍結狀態
        _rigidbody.freezeRotation = _wasRotationFrozen;
    }
}
