using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "item", menuName = "Data/item")]
public class item_SO : ScriptableObject
{
    public List<item> itemList ;
}
[System.Serializable]
public class item
{
    [Header("基本資料")]
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
    [Header("物品內容")]
    public int itemAmount;
    public string itemDescription;
}
