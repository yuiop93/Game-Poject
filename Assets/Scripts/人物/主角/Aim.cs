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
        Style1 = 1,
        Style2 = 2

    }

    public HoldStyle holdStyle = HoldStyle.Style1;
    public GameObject[] gunObject;
    public MonoBehaviour Script;
}

public class Aim : MonoBehaviour
{
    [Header("配置组件")]
    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private GameObject targetObject;
    public static float fixedDistance;
    [SerializeField] private Rig rig;
    [SerializeField] private Text weaponNameText;

    [Header("武器配置")]
    public List<WeaponComponent> weaponComponents;

    private StarterAssetsInputs _input;
    private Animator animator;
    private int currentWeaponIndex;
    private bool _previousAimState;
    private float sp;

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
        
        SetObjectsActive(false);
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
        }
    }

    private void HandleRigAndAnimationWeight()
    {
        float aimWeight = _input.aim ? 1.0f : 0.0f;
        sp = Mathf.Lerp(sp, aimWeight, Time.deltaTime * 10);

        if (Mathf.Approximately(aimWeight, 0))
        {
            sp = Mathf.Clamp(sp, 0, 1);
            rig.weight = sp;
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
        }
        else
        {
            rig.weight = 1;
            if (weaponComponents[currentWeaponIndex].holdStyle == WeaponComponent.HoldStyle.Style1)
            {
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(2, sp);
            }
            else if (weaponComponents[currentWeaponIndex].holdStyle == WeaponComponent.HoldStyle.Style2)
            {
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, sp);
            }
        }

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
            fixedDistance = 10f;
            return;
        }
        else
        {
            if (_input.fire)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0 && fixedDistance < 7f)
                    fixedDistance += 0.5f;

                if (Input.GetAxis("Mouse ScrollWheel") < 0 && fixedDistance > 5f)
                    fixedDistance -= 0.5f;
            }
            else
            {
                fixedDistance = 5f;
            }

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

        // 啟用新武器
        ActivateWeaponByIndex(currentWeaponIndex);
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

        // 更新索引並啟用對應武器
        currentWeaponIndex = index;
        ActivateWeaponByIndex(currentWeaponIndex);
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
    }

    private void SetObjectsActive(bool isActive)
    {
        foreach (var item in weaponComponents)
        {
            item.Script.enabled = false;
        }
        weaponComponents[currentWeaponIndex].Script.enabled = isActive;
        foreach (GameObject item in _gameObject)
        {
            if (item != null) item.SetActive(isActive);
        }
        玩家狀態.狀態 = isActive ? 玩家狀態.Statetype.瞄準 : 玩家狀態.Statetype.正常;
    }
}
