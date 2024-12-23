using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class 背包 : MonoBehaviour
{
    [SerializeField]
    private GameObject 背包UI;
    public 分類顯示 狀態;
    private item_SO 物品SO;
    public Transform 背包欄位;
    public GameObject 物品資訊;

    public void 顯示背包()
    {
        狀態.開啟();
    }
    void Awake()
    {
        物品SO = Resources.Load<item_SO>("ScriptableObjects/道具/背包");
        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            物品SO.物品[i].數量 = 0;
        }
    }
}
