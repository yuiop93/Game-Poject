using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class 背包 : MonoBehaviour
{
    [SerializeField]
    private GameObject 背包UI;
    [SerializeField]
    private 控制 f1;
    [SerializeField]
    private 物品顯示 物品顯示1;
    [SerializeField]
    private 物品顯示 物品顯示2;
    private item_SO 物品SO;

    public void 顯示背包()
    {
        物品顯示1.更新背包();
        物品顯示2.更新背包();
        背包UI.SetActive(true);
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
            顯示背包();
        }
    }
}
