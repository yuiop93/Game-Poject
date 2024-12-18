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
        Style1 = 1, // 持枪方式1
        Style2 = 2  // 持枪方式2
    }

    public HoldStyle holdStyle = HoldStyle.Style1;
    public string gunName;
    public GameObject BulletPrefab;
    public GameObject gunObject;
    public Sprite gunSprite;
    public int gunDamage;
    public int BulletConsumption = 1;
    public bool 後座力 = false;
    public MonoBehaviour Script;
    public Transform gunMuzzle;
    public AudioClip gunSound;
}

public class Aim : MonoBehaviour
{
    [Header("配置组件")]
    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float fixedDistance = 5f;
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

        if (_input.aim && currentWeaponIndex > 0)
        {
            if (玩家狀態.能量 >= weaponComponents[currentWeaponIndex].BulletConsumption)
            {
                animator.SetBool("Shoot", _input.fire);
            }
            else
            {
                animator.SetBool("Shoot", false);
            }
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
        }
        else
        {
            rig.weight = 1;
            animator.SetLayerWeight(2, sp);
        }

        animator.SetLayerWeight(1, sp);
    }

    private void HandleWeaponSwitching()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SwitchWeapon(1);
        }
    }

    private void HandleDistanceAdjustment()
    {
        if (!_input.fire || currentWeaponIndex != 0)
        {
            fixedDistance = 10f;
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && fixedDistance < 7f)
            fixedDistance += 0.5f;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && fixedDistance > 5f)
            fixedDistance -= 0.5f;
    }

    private void HandleRaycastTarget()
    {
        if (!_input.aim) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.origin + ray.direction.normalized * fixedDistance;
        targetObject.transform.position = targetPosition;
    }

    private void SwitchWeapon(int direction)
    {
        weaponComponents[currentWeaponIndex].gunObject.SetActive(false);
        weaponComponents[currentWeaponIndex].Script.enabled = false;

        currentWeaponIndex += direction;
        if (currentWeaponIndex < 0) currentWeaponIndex = weaponComponents.Count - 1;
        else if (currentWeaponIndex >= weaponComponents.Count) currentWeaponIndex = 0;

        var currentWeapon = weaponComponents[currentWeaponIndex];
        currentWeapon.gunObject.SetActive(true);
        currentWeapon.Script.enabled = true;

        weaponNameText.text = currentWeapon.gunName;
    }

    private void SetObjectsActive(bool isActive)
    {
        foreach (GameObject item in _gameObject)
        {
            if (item != null) item.SetActive(isActive);
        }
        玩家狀態.狀態 = isActive ? 玩家狀態.Statetype.瞄準 : 玩家狀態.Statetype.正常;
    }

    public void Shoot()
    {
        var currentWeapon = weaponComponents[currentWeaponIndex];
        GameObject bullet = Instantiate(currentWeapon.BulletPrefab, currentWeapon.gunMuzzle.position, currentWeapon.gunMuzzle.rotation);
        bullet.GetComponent<Bullet>().Initialize(targetObject.transform.position);

        AudioSource.PlayClipAtPoint(currentWeapon.gunSound, currentWeapon.gunMuzzle.position, 1f);
        玩家狀態.能量 -= currentWeapon.BulletConsumption;
    }
}
