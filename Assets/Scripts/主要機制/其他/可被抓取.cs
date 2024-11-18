using UnityEngine;
using UnityEngine.AI;
using System.Collections;
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
    private RigidbodyConstraints _originalConstraints; // 保存原始剛體約束
    private bool _wasGravityEnabled;     // 是否原本開啟了重力
    private bool _isKinematic;           // 是否原本是運動學
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        // 確保物件上有 Rigidbody
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _originalDrag = _rigidbody.drag;
        _originalConstraints = _rigidbody.constraints; // 保存原始約束
        _wasGravityEnabled = _rigidbody.useGravity;
        _isKinematic = _rigidbody.isKinematic;
    }

    /// <summary>
    /// 開始抓取
    /// </summary>
    /// <param name="attachPoint">抓取的連接點</param>
    public void Grab(Rigidbody attachPoint)
    {
        StopCheckingIfStopped(); // 停止檢查是否靜止
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

        // 保存原始阻力值和剛體狀態

        // 設定抓取時的阻力
        _rigidbody.drag = grabDrag;
        _rigidbody.useGravity = false;  // 關閉重力
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 凍結所有旋轉軸
        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = false; // 關閉 NavMeshAgent
        }

        _rigidbody.isKinematic = false;
    }

    /// <summary>
    /// 釋放抓取
    /// </summary>
    private Coroutine _checkCoroutine; // 用來存儲協程引用，方便停止

    public void Release()
    {
        if (_springJoint != null)
        {
            Destroy(_springJoint);
        }

        // 恢復原始阻力
        _rigidbody.drag = _originalDrag;
        // 恢復重力
        _rigidbody.useGravity = _wasGravityEnabled;
        // 恢復剛體的原始約束
        _rigidbody.constraints = _originalConstraints;

        // 開始檢查物體是否靜止
        if (_checkCoroutine == null)
        {
            _checkCoroutine = StartCoroutine(CheckIfStopped());
        }
    }

    private IEnumerator CheckIfStopped()
    {
        float elapsedTime = 0f;
        // 檢查物體是否靜止並計算靜止時間
        while (elapsedTime < 1f)
        {
            // 如果物體移動，重置計時器
            if (_rigidbody.velocity.magnitude > 0.1f)
            {
                elapsedTime = 0f; // 物體移動，重置時間
            }

            elapsedTime += Time.deltaTime; // 累加靜止時間
            yield return null; // 等待下一幀
        }
        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = true;
        }
        _rigidbody.isKinematic = _isKinematic;
        _checkCoroutine = null; // 協程結束後清空引用
    }

    public void StopCheckingIfStopped()
    {
        if (_checkCoroutine != null)
        {
            StopCoroutine(_checkCoroutine); // 停止協程
            _checkCoroutine = null; // 清空引用
        }
    }

}
