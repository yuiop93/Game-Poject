using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "物品", menuName = "Data/物品")]
public class item_SO : ScriptableObject
{
    public List<item> 物品;

    // 添加一个事件
    public event Action<int> OnItemCountChanged;

    // 修改物品数量的方法
    public void ChangeItemCount(int itemID, int newCount)
    {
        var item = 物品.Find(i => i.物品ID == itemID);
        if (item != null)
        {
            item.數量 = newCount; // 更新数量
            OnItemCountChanged?.Invoke(itemID); // 触发事件
        }
    }
}
[System.Serializable]
public class item
{
    public int 物品ID;
    public string 名稱;
    public Sprite 圖片;
    public int 數量;
    [TextArea]
    public string 描述;
    public bool CanUse = false;
    public 消耗品參數 消耗品;
}

[System.Serializable]
public class 消耗品參數
{
    public int 恢復血量;
    public int 恢復體力;
    public int 恢復能量;
}

