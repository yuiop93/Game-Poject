using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI視窗控制 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiTransform;
    private 控制 f1;

    void Start()
    {
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
    }
    void Update()
    {
        for (int i = 0; i < uiTransform.Length; i++)
        {
            if (uiTransform[i].GetComponent<UIScaleEffectWithClose>().isOpen)
            {
                f1.CursorUnLock();
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.B)) // B 键打开第一个 UI
        {
            ToggleUI(0); // 打开或关闭 uiTransform[0]
        }

        if (Input.GetKeyDown(KeyCode.C)) // C 键打开第二个 UI
        {
            ToggleUI(1); // 打开或关闭 uiTransform[1]
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape 键关闭所有打开的 UI
        {
            CloseAllUI();
        }

    }
    void ToggleUI(int index)
    {
        if (控制.互動中 && !uiTransform[index].GetComponent<UIScaleEffectWithClose>().isOpen)
        {
            Debug.Log("互動中");
            return;
        }
        // 切换 UI 状态
        var ui = uiTransform[index].GetComponent<UIScaleEffectWithClose>();
        if (ui.isOpen)
        {
            ui.CloseUI();
        }
        else
        {
            ui.OpenUI();
        }
    }
    // 判断是否有任意 UI 打开
    void CloseAllUI()
    {
        for (int i = 0; i < uiTransform.Length; i++)
        {
            if (uiTransform[i].GetComponent<UIScaleEffectWithClose>().isOpen)
            {
                uiTransform[i].GetComponent<UIScaleEffectWithClose>().CloseUI();
            }
        }
    }
}
