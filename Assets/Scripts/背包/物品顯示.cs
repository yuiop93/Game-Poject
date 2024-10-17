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
    void Start()
    {
        for (int i = 0; i < itemSO.物品.Count; i++)
        {
            if (itemSO.物品[i].數量 > 0)
            {
                GameObject item = Instantiate(物品prefab, 物品父物件);
                item.GetComponent<物品>().物品ID = itemSO.物品[i].物品ID;
                item.GetComponent<物品>().名稱.text = itemSO.物品[i].名稱;
                item.GetComponent<物品>().圖片.sprite = itemSO.物品[i].圖片;
                item.GetComponent<物品>().數量.text = itemSO.物品[i].數量.ToString();
            }
        }
    }
}
