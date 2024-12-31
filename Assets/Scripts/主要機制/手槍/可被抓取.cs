using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;
public class 可被抓取 : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onGrabbed; // 抓取時觸發的事件
    [SerializeField]
    private UnityEvent onReleased; // 釋放時觸發的事件
    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;

    [Header("抓取設置")]
    [SerializeField]
    private float grabSpringForce = 20f;   // SpringJoint 的彈力
    private float grabDrag = 5f;          // 抓取時的阻力
    public int 能量消耗 = 10;         // 抓取時消耗的能量
    private float _originalDrag;         // 原始阻力
    private RigidbodyConstraints _originalConstraints; // 保存原始剛體約束
    private bool _wasGravityEnabled;     // 是否原本開啟了重力
    private bool _isKinematic;           // 是否原本是運動學
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private int 傷害 = 10;
    [SerializeField]
    private int 爆炸傷害 = 1000;
    [SerializeField]
    private bool 是否爆炸 = false;
    [SerializeField]
    private GameObject 預置爆炸物件;

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
        onGrabbed.Invoke(); // 觸發抓取事件

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
        float elapsedTime;
        if (_navMeshAgent != null)
        {
            elapsedTime = 0.8f;
        }
        else
        {
            elapsedTime = 0f;
        }
        while (elapsedTime < 1f)
        {
            // 如果物體移動，重置計時器
            if (_rigidbody.velocity.magnitude > 0.1f)
            {
                if (_navMeshAgent != null)
                {
                    elapsedTime = 0.8f;
                }
                else
                {
                    elapsedTime = 0f;
                } // 物體移動，重置時間
            }

            elapsedTime += Time.deltaTime; // 累加靜止時間
            yield return null; // 等待下一幀
        }

        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = true; // 啟用 NavMeshAgent
        }
        _rigidbody.isKinematic = _isKinematic;
        _checkCoroutine = null; // 協程結束後清空引用
        onReleased.Invoke(); // 觸發釋放事件
    }

    public void StopCheckingIfStopped()
    {
        if (_checkCoroutine != null)
        {
            StopCoroutine(_checkCoroutine); // 停止協程
            _checkCoroutine = null; // 清空引用
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Mosters"))
        {
            if (other.relativeVelocity.magnitude > 10f)
            {
                if (other.gameObject.GetComponent<怪物身體部位>() != null)
                    other.gameObject.GetComponent<怪物身體部位>().受傷(傷害, true);
                    if(是否爆炸)
                    {
                        爆炸();
                    }
            }
        }
    }
    void 爆炸()
    {
        if (預置爆炸物件 != null)
        {
            Debug.Log("爆炸");
            GameObject gameObject =Instantiate(預置爆炸物件, transform.position, Quaternion.identity);
            gameObject.GetComponent<爆炸物件>().爆炸傷害 = 爆炸傷害;
        }
        Destroy(gameObject);
    }
}
