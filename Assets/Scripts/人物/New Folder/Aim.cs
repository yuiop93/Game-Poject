using UnityEngine;
using StarterAssets;
using UnityEngine.Animations.Rigging;
public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObject; // 存放需要激活/禁用的物件
    [SerializeField] private GameObject targetObject;  // 要移动的物体
    [SerializeField] private float fixedDistance = 5f; // 固定的距离
    [SerializeField] private Rig rig; // 用于获取 Rig 组件
    private StarterAssetsInputs _input; // 用于获取输入
    private bool _previousAimState;     // 用于记录前一次的 aim 状态
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
        SetObjectsActive(false);       // 初始化时禁用物件
        _previousAimState = _input.aim;
    }
    float sp;
    void Update()
    {
        if (_previousAimState != _input.aim) // 检测 aim 状态是否变化
        {
            SetObjectsActive(_input.aim);     // 根据 aim 状态激活或禁用物件
            _previousAimState = _input.aim;   // 更新记录的状态
        }
        if (_input.aim) // 如果 aim 为 true
        {
            ray(); // 调用 ray 方法
            Distance();
        }
        float myFloat = _input.aim ? 1.0f : 0.0f;
        sp = Mathf.Lerp(sp, myFloat, Time.deltaTime * 5);
        if (sp < 0.01f) sp = 0;
        if (sp > 0.99f) sp = 1;
        rig.weight = sp;
        animator.SetLayerWeight(1,sp); // 设置动画层权重

    }
    void Distance()
    {
        if (!_input.fire) { fixedDistance = 5f; }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (fixedDistance < 7f)
                    fixedDistance += 0.5f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (fixedDistance > 3f)
                    fixedDistance -= 0.5f;
            }
        }
    }
    void ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 计算目标位置，沿射线方向移动固定距离
        Vector3 targetPosition = ray.origin + ray.direction.normalized * fixedDistance;

        // 将物体位置设置为计算出来的目标位置
        targetObject.transform.position = targetPosition;
    }

    private void SetObjectsActive(bool isActive)
    {
        foreach (GameObject item in _gameObject)
        {
            if (item != null)
            {
                item.SetActive(isActive);
            }
        }
        
        玩家狀態.狀態=isActive ? 玩家狀態.Statetype.瞄準:玩家狀態.Statetype.正常;
    }
}
