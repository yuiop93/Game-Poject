using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物品顯示 : MonoBehaviour
{
    public item_SO itemSO; // 物品的数据源
    [SerializeField]
    private GameObject 物品prefab; // 物品 prefab
    [SerializeField]
    private Transform 物品父物件; // 物品的父物体
    [SerializeField]
    private GameObject 空物品; // 显示空物品提示的 UI

    // 更新背包界面，传入物品类型作为参数
    public void 更新背包(int 物品類型)
    {
        // 清空当前物品显示
        foreach (Transform child in 物品父物件)
        {
            Destroy(child.gameObject);
        }

        // 遍历所有物品并显示数量大于0的特定类型物品
        for (int i = 0; i < itemSO.物品.Count; i++)
        {
            if (itemSO.物品[i].數量 > 0 && itemSO.物品[i].物品ID / 1000 == 物品類型 + 1)
            {
                GameObject item = Instantiate(物品prefab, 物品父物件);
                item.GetComponent<物品>().物品ID = itemSO.物品[i].物品ID; // 设置物品 ID
                item.GetComponent<物品>().更新數量(); // 更新物品显示数量
            }
        }
        
        // 判断背包是否为空并更新显示
        判斷背包();
    }

    // 判断背包是否为空，并更新 UI
    public void 判斷背包()
    {
        if (gameObject.activeSelf)
        {
            // 如果物品父物体没有子物体，则显示空物品提示
            空物品.SetActive(物品父物件.childCount == 0);
        }
    }
}
