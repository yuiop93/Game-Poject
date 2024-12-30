using UnityEngine;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Aim : MonoBehaviour
{
    [Header("配置组件")]
    [SerializeField] private GameObject[] 瞄準物件;
    [SerializeField] private GameObject targetObject;
    public float fixedDistance;
    [SerializeField] private Rig[] rigs;

    [Header("武器配置")]
    public List<WeaponComponent> weaponComponents;

    private StarterAssetsInputs _input;
    private Animator animator;
    private int currentWeaponIndex;
    private bool _previousAimState;
    private float sp;
    [SerializeField]
    private Transform _GunHandle;
    public Transform[] GunHandle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
        SetObjectsActive(false);
        _previousAimState = _input.aim;
    }
    void Start()
    {
        currentWeaponIndex = 0;
        SwitchToWeaponByIndex(currentWeaponIndex);
    }
    private void OnDisable()
    {
        _input.fire = false;
        foreach (var item in weaponComponents)
        {
            item.Script.enabled = false;
        }
        SetObjectsActive(false);
    }
    private void OnEnable()
    {
        SwitchToWeaponByIndex(currentWeaponIndex);
    }
    private void Update()
    {
        HandleAimState();
        HandleRigAndAnimationWeight();
        HandleWeaponSwitching();
        HandleDistanceAdjustment();
        HandleRaycastTarget();
    }

    private void HandleAimState()
    {
        if (_previousAimState != _input.aim)
        {
            SetObjectsActive(_input.aim);
            _previousAimState = _input.aim;
            if (_previousAimState == true)
            {
                ActivateWeaponByIndex(currentWeaponIndex);
            }
        }
    }
    private void HandleRigAndAnimationWeight()
    {
        // 获取瞄准权重
        float aimWeight = _input.aim ? 1.0f : 0.0f;
        sp = Mathf.Lerp(sp, aimWeight, Time.deltaTime * 10);

        // 清空所有 Rig 的权重
        foreach (var rig in rigs)
        {
            if (rig != null)
                rig.weight = 0;
        }

        // 根据瞄准状态调整动画和 Rig 权重
        if (Mathf.Approximately(aimWeight, 0))
        {
            sp = Mathf.Clamp(sp, 0, 1);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
        }
        else
        {
            WeaponComponent currentWeapon = weaponComponents[currentWeaponIndex];
            switch (currentWeapon.holdStyle)
            {
                case WeaponComponent.HoldStyle.Style1:
                    rigs[1].weight = sp;
                    animator.SetLayerWeight(3, 0);
                    animator.SetLayerWeight(2, sp);
                    break;
                case WeaponComponent.HoldStyle.Style2:
                    rigs[0].weight = sp;
                    rigs[2].weight = sp;
                    animator.SetLayerWeight(2, 0);
                    animator.SetLayerWeight(3, sp);
                    break;
                case WeaponComponent.HoldStyle.Style3:
                    rigs[0].weight = sp;
                    rigs[3].weight = sp;
                    animator.SetLayerWeight(2, sp * 0.5f); // 示例: Style3 动画层的混合效果
                    animator.SetLayerWeight(3, sp * 0.5f);
                    break;
            }
        }
        // 更新基础动画层的权重
        animator.SetLayerWeight(1, sp);
    }

    private void HandleWeaponSwitching()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SwitchToNextWeapon(1);
        }
    }

    private void HandleDistanceAdjustment()
    {
        if (currentWeaponIndex != 0)
        {
            return;
        }
        else
        {
            if (_input.fire)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0 && fixedDistance < 7f)
                    fixedDistance += 0.5f;

                if (Input.GetAxis("Mouse ScrollWheel") < 0 && fixedDistance > 3f)
                    fixedDistance -= 0.5f;
            }
            else
            {
                fixedDistance = 5f;
            }

        }

    }
    void GunHandlePosition()
    {
        if (GunHandle[currentWeaponIndex] != null)
        {
            Debug.Log("Handle");
            _GunHandle.position = GunHandle[currentWeaponIndex].position;
        }
    }

    private void HandleRaycastTarget()
    {
        if (!_input.aim) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.origin + ray.direction.normalized * fixedDistance;
        targetObject.transform.position = targetPosition;
    }

    private void SwitchToNextWeapon(int direction)
    {
        // 禁用當前武器
        foreach (var item in weaponComponents)
        {
            foreach (var gun in item.gunObject)
            {
                gun.SetActive(false);
            }
            item.Script.enabled = false;
        }
        // 計算下一個武器的索引
        currentWeaponIndex += direction;
        if (currentWeaponIndex < 0) currentWeaponIndex = weaponComponents.Count - 1;
        else if (currentWeaponIndex >= weaponComponents.Count) currentWeaponIndex = 0;

        if (currentWeaponIndex < weaponComponents.Count && GameObject.Find("程式/控制").GetComponent<控制>().武器取得狀態[currentWeaponIndex] == true)
        {
            // 啟用新武器
            ActivateWeaponByIndex(currentWeaponIndex);
        }
        else
        {
            // 超出範圍或未取得武器則循環
            SwitchToNextWeapon(direction);
        }
    }

    // 切換到指定索引的武器
    private void SwitchToWeaponByIndex(int index)
    {
        if (index < 0 || index >= weaponComponents.Count)
        {
            Debug.LogWarning("索引超出範圍！");
            return;
        }

        // 禁用當前武器
        foreach (var item in weaponComponents)
        {
            foreach (var gun in item.gunObject)
            {
                gun.SetActive(false);
            }
            item.Script.enabled = false;
        }
        weaponComponents[index].gunObject[1].SetActive(true);
        // 更新索引並啟用對應武器
        currentWeaponIndex = index;
    }

    // 啟用指定索引的武器
    private void ActivateWeaponByIndex(int index)
    {
        var currentWeapon = weaponComponents[index];
        foreach (var gun in currentWeapon.gunObject)
        {
            gun.SetActive(true);
        }
        currentWeapon.Script.enabled = true;
        GunHandlePosition();
    }

    private void SetObjectsActive(bool isActive)
    {
        foreach (var item in weaponComponents)
        {
            item.Script.enabled = false;
        }
        weaponComponents[currentWeaponIndex].Script.enabled = isActive;
        foreach (GameObject item in 瞄準物件)
        {
            if (item != null) item.SetActive(isActive);
        }
        玩家狀態.狀態 = isActive ? 玩家狀態.Statetype.瞄準 : 玩家狀態.Statetype.正常;
    }
}
