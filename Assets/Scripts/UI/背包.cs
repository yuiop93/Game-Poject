using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class 背包 : MonoBehaviour
{
    [SerializeField]
    private GameObject 背包UI;
    private 控制 f1;
    public 分類顯示 狀態;
    private item_SO 物品SO;

    public void 顯示背包()
    {
        背包UI.SetActive(true);
        狀態.開啟();
        f1.CursorUnLock();
    }
    public void 隱藏背包()
    {
        背包UI.SetActive(false);
        f1.CursorLock();
    }
    void Start()
    {
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
        物品SO = Resources.Load<item_SO>("ScriptableObjects/道具/背包");
        背包UI.SetActive(false);
        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            物品SO.物品[i].數量 = 0;
        }
    }

    void Update()
    {
        if (背包UI.activeSelf == true & Input.GetKeyDown(KeyCode.Escape))
        {
            隱藏背包();
        }
        if (背包UI.activeSelf == false & Keyboard.current.bKey.wasPressedThisFrame)
        {
            if (控制.互動中 == false)
            {
                顯示背包();
            }
        }
    }
}
