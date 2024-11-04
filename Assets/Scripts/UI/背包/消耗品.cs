using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 消耗品 : MonoBehaviour
{
    [SerializeField]
    private item_SO item_SO;
    [SerializeField]
    private Transform 道具欄;
    [SerializeField]
    private Text 數量UI;
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject 耗盡UI;
    public void 道具顯示(int ID)
    {
        耗盡UI.SetActive(false);
        for (int i = 0; i < item_SO.物品.Count; i++)
        {
            if (item_SO.物品[i].物品ID == ID)
            {
                image.sprite = item_SO.物品[i].圖片;
                數量UI.text = item_SO.物品[i].數量.ToString();
                break;
            }
        }
    }
    public void 使用道具(item item)
    {
        if (item.CanUse)
        {
            玩家狀態.血量 += item.消耗品.恢復HP;
            玩家狀態.體力 += item.消耗品.恢復SP;
            玩家狀態.能量 += item.消耗品.恢復MP;
            item.數量--;
            數量UI.text = item.數量.ToString();
            if (item.數量 <= 0)
            {
                耗盡UI.SetActive(true);
            }
        }
    }
}
