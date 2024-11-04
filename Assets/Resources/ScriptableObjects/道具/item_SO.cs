using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Data/item")]
public class item_SO : ScriptableObject
{
    public List<item> 物品;
}

[System.Serializable]
public class item
{
    public int 物品ID;
    public string 名稱;
    public Sprite 圖片;
    public int 數量;
    public string 描述;
    public bool CanUse = false;
    public 消耗品參數 消耗品;
}

[System.Serializable]
public class 消耗品參數
{
    public int 恢復HP;
    public int 恢復MP;
    public int 恢復SP;
}

