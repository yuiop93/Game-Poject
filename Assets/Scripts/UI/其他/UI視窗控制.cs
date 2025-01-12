using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI視窗控制 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiTransform;
    private 控制 f1;
    public bool isUIOpen = false;

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
        if (Input.GetKeyDown(KeyCode.B)&&控制.背包)
        {
            ToggleUI(0); // 打开或关闭 uiTransform[0]
        }

        if (Input.GetKeyDown(KeyCode.C)&&控制.武器)
        {
            ToggleUI(1); // 打开或关闭 uiTransform[1]
        }

        if (Input.GetKeyDown(KeyCode.G)&&控制.教學) 
        {
            ToggleUI(2); // 打开或关闭 uiTransform[2]
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
            isUIOpen = true;
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
        isUIOpen = false;
    }
}
