using UnityEngine;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.InputSystem;
[System.Serializable]
public class WeaponComponent
{
    public enum HoldStyle
    {
        Style1 = 1, // 持槍方式1
        Style2 = 2  // 持槍方式2
    }

    public HoldStyle holdStyle = HoldStyle.Style1; // 預設持槍方式
    public string gunName; // 槍械名稱
    public GameObject BulletPrefab; // 子彈Prefab
    public GameObject gunObject;// 槍械物件
    public Sprite gunSprite; // 槍械圖片
    public int gunDamage; // 槍械傷害
    public int BulletConsumption = 1; // 子彈消耗
    public bool 後座力 = false; // 後座力
    public MonoBehaviour Script; // 槍械腳本
    public Transform gunMuzzle; // 槍口位置
    public AudioClip gunSound; // 槍械音效
}
public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObject; // 存放需要激活/禁用的物件
    [SerializeField] private GameObject targetObject;  // 要移动的物体
    [SerializeField] private float fixedDistance = 5f; // 固定的距离
    [SerializeField] private Rig rig; // 用于获取 Rig 组件
    private StarterAssetsInputs _input; // 用于获取输入
    private bool _previousAimState;     // 用于记录前一次的 aim 状态
    private Animator animator;
    public List<WeaponComponent> weaponComponents;
    public Text text;
    void Awake()
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
            if(index>0)
            {
                animator.SetBool("Shoot", _input.fire);
            }
        }
        float myFloat = _input.aim ? 1.0f : 0.0f;
        sp = Mathf.Lerp(sp, myFloat, Time.deltaTime * 10);
        if (myFloat == 0)
        {
            if (sp < 0.01f)
            {
                sp = 0;
            }
            rig.weight = sp;
            animator.SetLayerWeight(2, 0); // 设置动画层权重
        }
        else if (myFloat == 1)
        {
            if (sp > 0.99f)
            {
                sp = 1;
            }
            rig.weight = 1;
            animator.SetLayerWeight(2, sp); // 设置动画层权重
        }
        animator.SetLayerWeight(1, sp); // 设置动画层权重
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SwitchWeapon(1); // 向前切換
        }
    }
    int index;
    void SwitchWeapon(int direction)
    {
        for (int i = 0; i < weaponComponents.Count; i++)
        {
            if (weaponComponents[i].gunObject.activeSelf)
            {
                weaponComponents[i].gunObject.SetActive(false);
                weaponComponents[i].Script.enabled = false;
                break;
            }
        }

        index += direction;
        if (index < 0)
        {
            index = weaponComponents.Count - 1;
        }
        else if (index >= weaponComponents.Count)
        {
            index = 0;
        }
        Debug.Log(index);
        weaponComponents[index].gunObject.SetActive(true);
        text.text = weaponComponents[index].gunName;
        weaponComponents[index].Script.enabled = true;
    }
    void Distance()
    {
        if (!_input.fire||index!=0)
        {
            fixedDistance = 10f;
        }
        else
        {
            if (fixedDistance > 7f)
            {
                fixedDistance = 5f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (fixedDistance < 7f)
                    fixedDistance += 0.5f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (fixedDistance > 5f)
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

        玩家狀態.狀態 = isActive ? 玩家狀態.Statetype.瞄準 : 玩家狀態.Statetype.正常;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(weaponComponents[index].BulletPrefab, weaponComponents[index].gunMuzzle.position, weaponComponents[index].gunMuzzle.rotation); // 生成子彈
        bullet.GetComponent<Bullet>().Initialize(targetObject.transform.position); // 初始化子彈
        AudioSource.PlayClipAtPoint(weaponComponents[index].gunSound,weaponComponents[index].gunMuzzle.position , 1f); // 播放槍械音效
        玩家狀態.能量 -= weaponComponents[index].BulletConsumption; // 消耗能量
    }
}
