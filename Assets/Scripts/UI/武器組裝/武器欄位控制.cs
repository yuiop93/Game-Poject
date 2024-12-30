using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 武器欄位控制 : MonoBehaviour
{
    [SerializeField] private GameObject[] 武器鏡頭;
    [SerializeField] private List<武器選擇> 武器列表;
    public Transform 組件背包;
    private int _currentWeaponIndex;
    public Button 裝備按鈕;
    public GameObject 資訊欄;
    public GameObject[] 玩家裝備位置;
    public GameObject 當前組件;
    public Sprite 當前組件Image;
    public 組件狀態 當前組件狀態;
    [SerializeField] private Image 組件圖片;
    public GameObject 步槍前端;
    [Header("槍械資訊")]
    public Text 槍械名稱;
    public Text 槍械描述;
    public Slider 傷害;
    public Slider 射程;
    public Slider 能量消耗;

    public void 介面開關(bool 開關)
    {
        if (開關)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void 切換到指定索引的武器(int index)
    {
        if (index < 0 || index >= 武器列表.Count)
        {
            Debug.LogWarning("索引超出範圍！" + index);
            return;
        }
        foreach (var item in 武器列表)
        {
            item.槍械.SetActive(false);
        }
        武器列表[index].槍械.SetActive(true);
        _currentWeaponIndex = index;
        資訊欄.SetActive(false);
        槍械資訊欄更新();
    }
    public void 資訊欄更新()
    {
        if (當前組件 == null)
        {
            資訊欄.SetActive(false);
            return;
        }
        資訊欄.SetActive(true);
        組件圖片.sprite = 當前組件Image;
        判斷組件是否裝備(_currentWeaponIndex);
    }
    void 槍械資訊欄更新()
    {
        if (武器列表[_currentWeaponIndex].槍械描述 != null)
        {
            槍械名稱.text = 武器列表[_currentWeaponIndex].槍械描述.槍械名稱;
            槍械描述.text = 武器列表[_currentWeaponIndex].槍械描述.槍械描述;
            傷害.value = 武器列表[_currentWeaponIndex].槍械描述.傷害;
            射程.value = 武器列表[_currentWeaponIndex].槍械描述.射程;
            能量消耗.value = 武器列表[_currentWeaponIndex].槍械描述.能量消耗;
        }
    }
    public void 裝備()
    {
        if (當前組件狀態.裝備位置 == null)
        {
            裝備組件();
        }
        else
        {
            拆卸組件();
        }
    }

    public void 判斷裝備是否有組件()
    {
        if (_currentWeaponIndex == 1)
        {
            if (武器列表[_currentWeaponIndex].組件.Count == 0)
            {
                步槍前端.SetActive(true);
            }
            else
            {
                步槍前端.SetActive(false);
            }
        }
        if (武器列表[_currentWeaponIndex].組件UI == null)
        {
            return;
        }
        else if (武器列表[_currentWeaponIndex].組件UI != 當前組件狀態.組件UI)
        {
            裝備按鈕.interactable = false;
            裝備按鈕.GetComponentInChildren<Text>().text = "此武器已裝備組件";
        }
    }
    public void 判斷組件是否裝備(int index)
    {
        if (index == 0)
        {
            裝備按鈕.interactable = false;
            裝備按鈕.GetComponentInChildren<Text>().text = "無法使用";
        }
        else if (當前組件狀態.裝備位置 == null)
        {
            裝備按鈕.interactable = true;
            裝備按鈕.GetComponentInChildren<Text>().text = "裝備";
        }
        else if (當前組件狀態.裝備位置 == 武器列表[index].槍械)
        {
            裝備按鈕.interactable = true;
            裝備按鈕.GetComponentInChildren<Text>().text = "拆卸";
        }
        else
        {
            裝備按鈕.interactable = false;
            裝備按鈕.GetComponentInChildren<Text>().text = "已裝備";
        }
        判斷裝備是否有組件();
    }

    private Coroutine currentMoveCoroutine;

    void 裝備組件()
    {
        武器列表[_currentWeaponIndex].組件UI = 當前組件狀態.組件UI;
        當前組件狀態.裝備位置 = 武器列表[_currentWeaponIndex].槍械;
        武器列表[_currentWeaponIndex].組件.Add(Instantiate(當前組件, 玩家裝備位置[_currentWeaponIndex].transform));
        武器列表[_currentWeaponIndex].組件.Add(Instantiate(當前組件, 武器列表[_currentWeaponIndex].槍械.transform));
        武器列表[_currentWeaponIndex].組件[1].transform.localPosition = new Vector3(0, 0, 1);

        // 如果有未完成的协程，先停止
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        // 启动新协程并保存句柄
        currentMoveCoroutine = StartCoroutine(MoveToLocalTargetPosition(武器列表[_currentWeaponIndex].組件[1].transform, new Vector3(0, 0, 0), 0.2f));
        判斷組件是否裝備(_currentWeaponIndex);
    }

    void 拆卸組件()
    {
        // 停止协程
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }

        武器列表[_currentWeaponIndex].組件UI = null;

        foreach (var item in 武器列表[_currentWeaponIndex].組件)
        {
            Destroy(item);
        }
        武器列表[_currentWeaponIndex].組件 = new List<GameObject>();
        當前組件狀態.裝備位置 = null;

        判斷組件是否裝備(_currentWeaponIndex);
    }

    private IEnumerator MoveToLocalTargetPosition(Transform target, Vector3 finalLocalPosition, float duration)
    {
        // 检查当前武器是否有组件
        if (武器列表[_currentWeaponIndex].組件.Count <= 1 || 武器列表[_currentWeaponIndex].組件[1] == null)
        {
            yield break;
        }

        var component = 武器列表[_currentWeaponIndex].組件[1];

        // 如果组件有 Animator，先禁用
        Animator animator = component.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }

        float elapsedTime = 0f;
        Vector3 startLocalPosition = target.localPosition;

        while (elapsedTime < duration)
        {
            // 如果组件或目标已被销毁，则终止
            if (component == null || target == null)
            {
                yield break;
            }

            target.localPosition = Vector3.Lerp(startLocalPosition, finalLocalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 确保最终位置正确
        if (target != null)
        {
            target.localPosition = finalLocalPosition;
        }

        // 恢复 Animator
        if (component != null && animator != null)
        {
            animator.enabled = true;
        }

        // 清除协程句柄
        currentMoveCoroutine = null;
    }


    void Open()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(true);
        }
        for (int i = 0; i < 武器列表.Count; i++)
        {
            武器列表[i].按鈕.interactable = GameObject.Find("程式/控制").GetComponent<控制>().武器取得狀態[i];
        }
        當前組件 = null;
        當前組件Image = null;
        武器列表[0].按鈕.Select();
        切換到指定索引的武器(0);
    }
    void Close()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(false);
        }
    }
}
