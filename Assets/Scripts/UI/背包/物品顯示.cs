using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物品顯示 : MonoBehaviour
{
    public item_SO itemSO;
    [SerializeField]
    private GameObject 物品prefab;
    [SerializeField]
    private Transform 物品父物件;
    [SerializeField]
    private GameObject 空物品;
    public void 更新背包(int 物品類型)
    {
        foreach (Transform child in 物品父物件)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < itemSO.物品.Count; i++)
        {
            if (itemSO.物品[i].數量 > 0 && itemSO.物品[i].物品ID / 1000 == 物品類型+1)
            {
                GameObject item = Instantiate(物品prefab, 物品父物件);
                item.GetComponent<物品>().物品ID = itemSO.物品[i].物品ID;
                item.GetComponent<物品>().更新數量();
            }
        }
        判斷背包();
    }
    public void 判斷背包()
    {
        if (gameObject.activeSelf == true)
        {
            if (物品父物件.childCount == 0)
            {
                空物品.SetActive(true);
            }
            else
            {
                空物品.SetActive(false);
            }
        }

    }
}
