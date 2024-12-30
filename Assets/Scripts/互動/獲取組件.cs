using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 獲取組件 : MonoBehaviour
{
    [SerializeField]private Item item;
    private GameObject 組件UIPrefab;
    private Transform 組件背包;
    [HideInInspector]
    public 武器欄位控制 武器欄位控制;
    void Start()
    {
        組件UIPrefab = Resources.Load<GameObject>("Prefab/UI/組件/組件UI");
        武器欄位控制= GameObject.Find("UI控制/彈出UI/武器").GetComponent<武器欄位控制>();
        組件背包 =武器欄位控制.組件背包; 
    }
    public void 撿取()
    {
        if (item == null)
        {
            Debug.LogError("物品未設定！");
            return;
        }
        GameObject 組件UI = Instantiate(組件UIPrefab, 組件背包);
        組件UI.GetComponent<組件UI>().武器欄位控制 = 武器欄位控制;
        組件UI.GetComponent<組件UI>().設定組件(item);
    }
}
